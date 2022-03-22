/*  GRBL-Plotter. Another GCode sender for GRBL.
    This file is part of the GRBL-Plotter application.
   
    Copyright (C) 2015-2022 Sven Hasemann contact: svenhb@web.de

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
/*
 * 2020-09-13 split file
 * 2020-12-03 Bug fix invoke required in line 59
 * 2020-12-16 line 183 remove lock
 * 2020-12-23 adjust notifier handling: only notify if estimated process time > notifier intervall
 * 2021-03-05 line 118 error handling
 * 2021-07-02 code clean up / code quality
 * 2021-09-03 BtnStreamStart_Click switch from click to mouseUp event
 * 2021-11-23 line 121, 403, 406 add try/catch
 * 2021-11-30 check lineIsInRange(fCTBCodeClickedLineNow)
 * 2021-12-10 line 122 check if (e.CodeLineSent >= fCTBCode.LinesCount); line 340 = fCTBCode.LinesCount - 1;
 * 2022-01-05 bug fix GrblStreaming.pause: show tool change message
*/

using GrblPlotter.GUI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GrblPlotter
{
    public partial class MainForm : Form
    {
        #region Streaming

        TimeSpan elapsed;               //elapsed time 
        DateTime timeInit;              //time start 

        private int signalResume = 0;   // blinking button
        private int signalLock = 0;     // blinking button
        private int signalPlay = 0;     // blinking button
        private int delayedSend = 0;
        private bool isStreaming = false;
        private bool isStreamingPause = false;
        private bool isStreamingCheck = false;
     //   private bool isStreamingRequestStop = false;
        private bool isStreamingOk = true;
        private string lblInfoOkString = "Send G-Code";

        /***** thread save update *****/
        private void UpdateProgressBar(int codeProgress, int buffProgress)
        {
            try {
                int cPrgs = codeProgress;
                if (cPrgs < 0) cPrgs = 0; if (cPrgs > pbFile.Maximum) cPrgs = pbFile.Maximum;
                int bPrgs = buffProgress;
                if (bPrgs < 0) bPrgs = 0; if (bPrgs > pbBuffer.Maximum) bPrgs = pbBuffer.Maximum;

                if (this.pbFile.InvokeRequired)
                { this.pbFile.BeginInvoke((MethodInvoker)delegate () { this.pbFile.Value = cPrgs; }); }
                else
                { this.pbFile.Value = cPrgs; }

                if (this.pbBuffer.InvokeRequired)
                { this.pbBuffer.BeginInvoke((MethodInvoker)delegate () { this.pbBuffer.Value = bPrgs; }); }
                else
                { this.pbBuffer.Value = bPrgs; }

                string txt = string.Format("Progress {0}%", codeProgress);
                if (this.lblFileProgress.InvokeRequired)
                { this.lblFileProgress.BeginInvoke((MethodInvoker)delegate () { this.lblFileProgress.Text = txt; }); }
                else
                { this.lblFileProgress.Text = txt; }
            }
            catch (Exception err) { Logger.Error(err, "UpdateProgressBar "); }
        }

        private static string GetTimeStampString()
        { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }

        /***** Receive streaming status from serial COM *****/
        private bool notifierUpdateMarker = false;
        private bool notifierUpdateMarkerFinish = false;
        private int lastErrorLine = 0;
        private void OnRaiseStreamEvent(object sender, StreamEventArgs e)
        {
            // only notify if estimated process-time > notifier intervall
            bool notifierEnable = ((double)Properties.Settings.Default.notifierMessageProgressInterval < VisuGCode.gcodeMinutes);
            if (isStreaming)
            {
                UpdateProgressBar(e.CodeProgress, e.BuffProgress);
                if (notifierEnable && Properties.Settings.Default.notifierMessageProgressEnable)
                {
                    if ((elapsed.Seconds % (int)(60 * Properties.Settings.Default.notifierMessageProgressInterval)) == 5) // offset 5 sec. to get message at start
                    {
                        if (notifierUpdateMarker)
                        {
                            notifierUpdateMarker = false;
                            string etime = string.Format("{0:00}:{1:00} hrs", elapsed.Hours, elapsed.Minutes);
                            string msg = string.Format("{0}Duration   : {1} \r\nCode line  : {2,6}\r\nProcessed: {3,4:0.0} %\r\nGrbl Buffer: {4,3:0} %\r\nTime stamp: {5}", "", etime, e.CodeLineSent, e.CodeProgress, e.BuffProgress, GetTimeStampString());//Properties.Settings.Default.notifierMessageProgress
                            if (Properties.Settings.Default.notifierMessageProgressTitle)
                                Notifier.SendMessage(msg, string.Format("{0,4:0.0} %", e.CodeProgress));
                            else
                                Notifier.SendMessage(msg);
                        }
                    }
                    else
                        notifierUpdateMarker = true;
                }
            }

            int actualCodeLine = e.CodeLineSent;
            if (actualCodeLine < 0) actualCodeLine = 0;
            if (e.CodeLineSent >= fCTBCode.LinesCount)
                actualCodeLine = fCTBCode.LinesCount - 1;
			try
            {	fCTBCode.Selection = fCTBCode.GetLine(actualCodeLine);}
			catch (Exception err) {	Logger.Error(err,"OnRaiseStreamEvent - fCTBCode.Selection = fCTBCode.GetLine(actualCodeLine)");}

            fCTBCodeClickedLineNow = e.CodeLineSent - 1;// - 1;
            FctbCodeMarkLine();         // set Bookmark and marker in 2D-View
                                        //            fCTBCode.DoCaretVisible();

            try {
                if (this.fCTBCode.InvokeRequired)
                { this.fCTBCode.BeginInvoke((MethodInvoker)delegate () { this.fCTBCode.DoCaretVisible(); }); }
                else
                { this.fCTBCode.DoCaretVisible(); }
            }
            catch (Exception er) {
                Logger.Error(er, "OnRaiseStreamEvent fCTBCode.InvokeRequired "); 
            }


            if (_diyControlPad != null)
                _diyControlPad.SendFeedback("[" + e.Status.ToString() + "]");

        //    if (Properties.Settings.Default.guiProgressShow)
            VisuGCode.ProcessedPath.ProcessedPathLine(e.CodeLineConfirmed);		// in GCodeSimulate.cs

            if (logStreaming) Logger.Trace("### OnRaiseStreamEvent  {0}  line {1} ", e.Status.ToString(), e.CodeLineSent);

            switch (e.Status)
            {
                case GrblStreaming.lasermode:
                    ShowLaserMode();
                    break;

                case GrblStreaming.reset:
                    Logger.Info("### OnRaiseStreamEvent - GrblStreaming.reset");
                    ResetDetected = true;
                    lastErrorLine = 0;
                    SaveStreamingStatus(e.CodeLineSent);
                    StopStreaming(false);
                    if (e.CodeProgress < 0)
                    { SetInfoLabel(_serial_form.lastError, Color.Fuchsia); }
                    else
                    { SetInfoLabel("Vers. " + _serial_form.GrblVers, Color.Lime); }
                    StatusStripClear(1, 2);//, "grblStreaming.reset");
                    toolTip1.SetToolTip(lbInfo, lbInfo.Text);
                    timerUpdateControls = true; timerUpdateControlSource = "grblStreaming.reset";//updateControls();
                    if (_coordSystem_form != null)
                        _coordSystem_form.ShowValues();

                    ControlPowerSaving.EnableStandby();
                    VisuGCode.ProcessedPath.ProcessedPathClear();
                    break;

                case GrblStreaming.error:
                    Logger.Info("streaming error at line {0}", e.CodeLineConfirmed);
                    StatusStripSet(0, Grbl.lastMessage, Color.Fuchsia);
                    pbFile.ForeColor = Color.Red;

                    if (lastErrorLine != e.CodeLineConfirmed)
                    {
                        SaveStreamingStatus(e.CodeLineSent);

                        int errorLine = e.CodeLineConfirmed - 1;
                        if (isStreamingCheck)
                            errorLine = e.CodeLineConfirmed - 2;
                        ErrorLines.Add(errorLine);
                        MarkErrorLine(errorLine);

                        SetInfoLabel(Localization.GetString("mainInfoErrorLine") + errorLine.ToString(), Color.Fuchsia);

                        if (actualCodeLine < fCTBCode.LinesCount)
                            fCTBCode.BookmarkLine(actualCodeLine - 1);
                        fCTBCode.DoSelectionVisible();
                    }
                    lastErrorLine = e.CodeLineConfirmed;

                    if (notifierEnable) Notifier.SendMessage(string.Format("Streaming error at line {0}\r\nTime stamp: {1}", e.CodeLineConfirmed, GetTimeStampString()), "Error");
                    break;

                case GrblStreaming.ok:
                    if (!isStreamingCheck)
                    {
                        if (Grbl.lastErrorNr <= 0)
                        {
                            SetInfoLabel(lblInfoOkString + "(" + (e.CodeLineSent + 1).ToString() + ")", Color.Lime);

                            signalPlay = 0;
                            btnStreamStart.BackColor = SystemColors.Control;
                        }
                    }
                    lastErrorLine = 0;
                    break;

                case GrblStreaming.finish:
                    Logger.Info("streaming finished ok {0}", isStreamingOk);
                    if (isStreamingOk)
                    {
                        if (isStreamingCheck)
                        { SetInfoLabel(Localization.GetString("mainInfoFinishCheck"), Color.Lime); }   // "Finish checking G-Code"; }
                        else
                        { SetInfoLabel(Localization.GetString("mainInfoFinishSend"), Color.Lime); }   // "Finish sending G-Code"; }
                    }
                    MainTimer.Stop();
                    MainTimer.Start();
                    timerUpdateControls = true; timerUpdateControlSource = "grblStreaming.finish";//updateControls();
                    SaveStreamingStatus(0);
                    showPicBoxBgImage = false;                     // don't show background image anymore
                    pictureBox1.BackgroundImage = null;
                    ResetStreaming();

                    if (notifierEnable && !notifierUpdateMarkerFinish)    // just notify once
                    {
                        notifierUpdateMarkerFinish = true;
                        string msg = string.Format("{0}\r\nDuration  : {1} (hh:mm:ss)\r\nCode line : {2}\r\nTime stamp: {3}", Properties.Settings.Default.notifierMessageFinish, elapsed.ToString(@"hh\:mm\:ss"), fCTBCode.LinesCount, GetTimeStampString());
                        if (Properties.Settings.Default.notifierMessageProgressTitle)
                            Notifier.SendMessage(msg, "100 %");
                        else
                            Notifier.SendMessage(msg);
                    }
                    break;

                case GrblStreaming.waitidle:
                    //          timerUpdateControls = true; timerUpdateControlSource = "grblStreaming.waitidle";//updateControls();// true);
                    btnStreamStart.Image = Properties.Resources.btn_play;
                    SetInfoLabel(Localization.GetString("mainInfoWaitIdle") + e.CodeLineSent.ToString() + ")", Color.Yellow);
                    break;

                case GrblStreaming.pause:
                    signalPlay = 1;
                    SetInfoLabel(Localization.GetString("mainInfoPause") + e.CodeLineSent.ToString() + ")", Color.Yellow);
                    btnStreamStart.Image = Properties.Resources.btn_play;
                    isStreamingPause = true;
                    MainTimer.Stop();
                    MainTimer.Start();
                    timerUpdateControls = true; timerUpdateControlSource = "grblStreaming.pause";//updateControls(true);

                    SaveStreamingStatus(e.CodeLineSent);

                    if (Properties.Settings.Default.flowControlEnable) // send extra Pause-Code in MainTimer_Tick from Properties.Settings.Default.flowControlText
                        delayedSend = 2;

                    if (logStreaming) 
					{ 	if (LineIsInRange(fCTBCodeClickedLineNow))
							Logger.Trace("OnRaiseStreamEvent - pause: {0}  in line:{1}", fCTBCode.Lines[fCTBCodeClickedLineNow], fCTBCodeClickedLineNow);
						else
							Logger.Trace("OnRaiseStreamEvent - fCTBCodeClickedLineNow is out of range:{0}  count:{1}", fCTBCodeClickedLineNow, fCTBCode.Lines.Count);
					}

                    for (int tmpLine = (fCTBCodeClickedLineNow - 2); tmpLine <= (fCTBCodeClickedLineNow + 2); tmpLine++)
                    {   // find correct line
                        if (LineIsInRange(tmpLine) && fCTBCode.Lines[tmpLine].Contains("M0") && fCTBCode.Lines[tmpLine].Contains("Tool"))  // keyword set in gcodeRelated 1132
                        { signalShowToolExchangeMessage = true; signalShowToolExchangeLine = tmpLine; if(logStreaming) Logger.Trace("OnRaiseStreamEvent trigger ToolExchangeMessage"); break; }
                    }
                    if (notifierEnable) Notifier.SendMessage("grbl Pause", "Pause"); 
                    break;

                case GrblStreaming.toolchange:
                    timerUpdateControls = true; timerUpdateControlSource = "grblStreaming.toolchange";// updateControls();
                    btnStreamStart.Image = Properties.Resources.btn_play;
                    SetInfoLabel(Localization.GetString("mainInfoToolChange"), Color.Yellow);
                    cBTool.Checked = _serial_form.ToolInSpindle;
                    break;

                case GrblStreaming.stop:
                    timerUpdateControls = true; timerUpdateControlSource = "grblStreaming.stop";// updateControls();
                    SetInfoLabel(Localization.GetString("mainInfoStopStream") + e.CodeLineSent.ToString() + ")", Color.Fuchsia);

                    if (Properties.Settings.Default.flowControlEnable) // send extra Pause-Code in MainTimer_Tick from Properties.Settings.Default.flowControlText
                        delayedSend = 2;
                    break;

                default:
                    break;
            }

            lastLabelInfoText = lbInfo.Text;
            //          lbInfo.Text += overrideMessage;
            if (this.lbInfo.InvokeRequired)
            { this.lbInfo.BeginInvoke((MethodInvoker)delegate () { this.lbInfo.Text += overrideMessage; }); }
            else
            { this.lbInfo.Text += overrideMessage; }
        }
        internal delegate void Del();

        bool signalShowToolExchangeMessage = false;
        int signalShowToolExchangeLine = 0;
        private void ShowToolChangeMessage()		// triggert by signalShowToolExchangeMessage at GrblStreaming.pause
        {
            if (logStreaming) Logger.Trace("showToolChangeMessage");
            Console.Beep();
            using (MessageForm f = new MessageForm())
            {
                string tool = "unknown";
				if (LineIsInRange(signalShowToolExchangeLine))
					tool = fCTBCode.Lines[signalShowToolExchangeLine];
                int c1 = tool.IndexOf('(');
                if (c1 > 0)
                {
                    tool = tool.Substring(c1 + 1);
                    tool = tool.Substring(0, tool.Length - 1);
                    tool = tool.Replace("Color", "\r  Color");
                }
                string msg = Localization.GetString("mainToolChange1") + "  " + tool + "\r" + Localization.GetString("mainToolChange2");
                Notifier.SendMessage(msg, "Tool change");
                f.ShowMessage(Localization.GetString("mainToolChange"), msg, 1);
                var result = f.ShowDialog(this);
                if (result == DialogResult.Yes)
                { StartStreaming(0, fCTBCode.LinesCount - 1); }
            }
        }
        //     private void BtnStreamStart_Click(object sender, EventArgs e)
        private void BtnStreamStart_Click(object sender, MouseEventArgs e)
        {
            UpdateLogging();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            { StreamSection(); }
            else
            { StartStreaming(0, fCTBCode.LinesCount - 1); }
        }

        private void StreamSection()
        {
            int lineStart = fCTBCodeClickedLineNow;
            int lineEnd = fCTBCode.LinesCount - 1;
            int selStart = fCTBCode.Selection.FromLine;
            int selEnd = fCTBCode.Selection.ToLine;
            if (selStart < selEnd)
            {
                lineStart = selStart;
                lineEnd = selEnd;
            }

            using (MainFormStreamSection f = new MainFormStreamSection())
            {
                f.LineMax = fCTBCode.LinesCount - 1;
                f.LineStart = lineStart + 1;    // Editor starts at 1
                f.LineEnd = lineEnd + 1;        // Editor starts at 1
                f.Parser = VisuGCode.GetParserState(lineStart);
                f.Position = VisuGCode.GetActualPosition(f.LineStart);
                var result = f.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    fCTBCodeClickedLineNow = f.LineStart;
                    string parserState = VisuGCode.GetParserState(f.LineStart - 1);
                    XyzPoint tmpPos = VisuGCode.GetActualPosition(f.LineStart - 1); ;
                    Logger.Info("StreamSection start:{0} stop:{1}  state:{2} posX:{0:0.000} posY:{0:0.000} posZ:{0:0.000}", f.LineStart, f.LineEnd, parserState, tmpPos.X, tmpPos.Y, tmpPos.Z);
                    FctbCodeMarkLine();
                    _serial_form.parserStateGC = parserState;   //   <Parser State="G1 G54 G17 G21 G90 G94 M3 M9 T0 F1000 S10000" />
                    _serial_form.posPause = tmpPos;
                    StartStreaming(f.LineStart, f.LineEnd);
                }
            }
        }

        // if startline > 0 start with pause
        private void StartStreaming(int startLine, int endLine)
        {
            Logger.Trace("startStreaming serialPortOpen:{0} ", _serial_form.SerialPortOpen);
         //   isStreamingRequestStop = false;
            lblInfoOkString = Localization.GetString("mainInfoSendCode");
            notifierUpdateMarker = false;
            notifierUpdateMarkerFinish = false;
            if (fCTBCode.LinesCount > 1)
            {
                if (!isStreaming)
                {
                    ClearErrorLines();
                    Logger.Info("►►►►  Start streaming at line:{0} to line:{1} showProgress:{2}  backgroundImage:{3}", startLine, endLine, Properties.Settings.Default.guiProgressShow, Properties.Settings.Default.guiBackgroundImageEnable);
                    ExpandCodeBlocksToolStripMenuItem_Click(null, null);
                    VisuGCode.ProcessedPath.ProcessedPathClear();
                    MainTimer.Stop();
                    MainTimer.Start();

                    isStreaming = true;
                    isStreamingPause = false;
                    isStreamingCheck = false;
                    isStreamingOk = true;
					pbFile.Maximum = 100;
					
                    VisuGCode.MarkSelectedFigure(0);
                    if (startLine > 0)
                    {
                        btnStreamStart.Image = Properties.Resources.btn_pause;
                        //              isStreamingPause = true;
                    }

                    if (!Grbl.isVersion_0)		// show override buttons below start-button
                    {
                        gBoxOverride.Height = 175;
                        gBoxOverrideBig = true;
                    }

                    timerUpdateControlSource = "startStreaming";
                    UpdateControlEnables();
                    timeInit = DateTime.UtcNow;
                    elapsed = TimeSpan.Zero;
                    SetInfoLabel(Localization.GetString("mainInfoSendCode"), Color.Lime);
                    for (int i = 0; i < fCTBCode.LinesCount; i++)
                        fCTBCode.UnbookmarkLine(i);

                    //save gcode
                    string fileName = Datapath.AppDataFolder + "\\" + fileLastProcessed;
                    string txt = fCTBCode.Text;

                    try { 
						File.WriteAllText(fileName + ".nc", txt);		// save current GCode
						if (File.Exists(fileName + ".xml")) 			// remove old process-state
                            File.Delete(fileName + ".xml"); 
					    SaveRecentFile(fileLastProcessed + ".nc");		// update last processed file
					}
					catch (IOException err) { 
						Properties.Settings.Default.guiLastEndReason += "StartStreaming: "+Datapath.AppDataFolder+" ";
						Datapath.AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        Logger.Error(err, "StartStreaming fileName: {0}, new Datapath.AppDataFolder: {1} ", fileName, Datapath.AppDataFolder);
                        MessageBox.Show("Path does not exist and could not be created: "+fileName+"\r\nPath will be modified to "+ Datapath.AppDataFolder, "Error");
						fileName = Datapath.AppDataFolder + "\\" + fileLastProcessed;
					}
					catch (Exception err) {
                        Properties.Settings.Default.guiLastEndReason += "StartStreaming: " + err.Message + "  " + fileName+" ";
                        Logger.Error(err, "StartStreaming fileName: {0}, new Datapath.AppDataFolder: {1} ", fileName, Datapath.AppDataFolder);
                        MessageBox.Show("'last processed' file could not be created: " + fileName + "\r\nError: " + err.Message, "Error");
                        //  throw;		// unknown exception...  access denied 
					}

                    bool removeFiducials = (Properties.Settings.Default.importFiducialSkipCode && (VisuGCode.fiducialsCenter.Count > 0));
                    if (removeFiducials)	// copy code
					{
						UnDo.SetCode(fCTBCode.Text, "remove fiducials", this);
                        string fiducialLabel = Properties.Settings.Default.importFiducialLabel;
                        fCTBCode.TextChanged -= FctbCode_TextChanged;       // disable textChanged events
                        foreach (XmlMarker.BlockData tmp in XmlMarker.listFigures)
						{
                            if (tmp.Layer.IndexOf(fiducialLabel) >= 0)
                            {
                                Logger.Info("StartStreaming fiducials: exclude line:{0} to:{1}", tmp.LineStart, tmp.LineEnd);
                                for (int lnr=tmp.LineStart; lnr <= tmp.LineEnd; lnr++)								
								{
                                    Logger.Trace(" - {0}", fCTBCode.GetLineText(lnr));
                                    fCTBCode.Selection = fCTBCode.GetLine(lnr);
                                    fCTBCode.SelectedText = "(" + fCTBCode.GetLineText(lnr) + ")";  // remove fiducial code
                                }
							}							
						}
                        fCTBCode.TextChanged += FctbCode_TextChanged;       // enable textChanged events
                    }	

                    lblElapsed.Text = "Time " + elapsed.ToString(@"hh\:mm\:ss");
                    _serial_form.StartStreaming(fCTBCode.Lines, startLine, endLine, false);  // no check
					
					if (removeFiducials)	// restore original code with fiducials
					{
						fCTBCode.Text = UnDo.GetCode();
					}
					
                    btnStreamStart.Image = Properties.Resources.btn_pause;
                    btnStreamCheck.Enabled = false;
                    OnPaint_setBackground();                // Generante a background-image for pictureBox to avoid frequent drawing of pen-up/down paths
                    VisuGCode.SetPathAsLandMark(false);//clear = false
                    ControlPowerSaving.SuppressStandby();
                }
                else
                {
                    if (!isStreamingPause)
                    {
                        Logger.Info("⏸⏸  Pause streaming - pause stream");
                        btnStreamStart.Image = Properties.Resources.btn_play;
                        _serial_form.PauseStreaming();
                        //            isStreamingPause = true;
                        StatusStripSet(0, Localization.GetString("statusStripeStreamingStatusSaved"), Color.LightGreen);
                    }
                    else
                    {
                        Logger.Info("⏸⏸  Pause streaming - continue stream");
                        btnStreamStart.Image = Properties.Resources.btn_pause;
                        _serial_form.PauseStreaming();
                        isStreamingPause = false;
                        StatusStripClear(0);
                    }
                }
            }
        }
        private void BtnStreamCheck_Click(object sender, EventArgs e)
        {
            if ((fCTBCode.LinesCount > 1) && (!isStreaming))
            {
                ClearErrorLines();
                Logger.Info("check code");
                isStreaming = true;
                isStreamingCheck = true;
                isStreamingOk = true;
                timerUpdateControlSource = "btnStreamCheck_Click";
                UpdateControlEnables();
                timeInit = DateTime.UtcNow;
                elapsed = TimeSpan.Zero;
                SetInfoLabel(Localization.GetString("mainInfoCheckCode"), SystemColors.Control);
                for (int i = 0; i < fCTBCode.LinesCount; i++)
                    fCTBCode.UnbookmarkLine(i);
                _serial_form.StartStreaming(fCTBCode.Lines, 0, fCTBCode.LinesCount -1, true);
                btnStreamStart.Enabled = false;
                OnPaint_setBackground();
            }
        }
        private void BtnStreamStop_Click(object sender, EventArgs e)
        { UpdateLogging(); StopStreaming(true); }
        private void StopStreaming(bool showMessage)
        {
            Logger.Info("⏹⏹  Stop streaming at line {0}", (fCTBCodeClickedLineNow + 1));
            showPicBoxBgImage = false;                 // don't show background image anymore
                                                       //            pictureBox1.BackgroundImage = null;
            signalPlay = 0;
        //    isStreamingRequestStop = true;

            _serial_form.StopStreaming(showMessage);

            if (isStreaming || isStreamingCheck)
            {
                SetInfoLabel(Localization.GetString("mainInfoStopStream2") + (fCTBCodeClickedLineNow + 1).ToString() + " )", Color.Fuchsia);
            }

            ResetStreaming();
        }
        private void ResetStreaming(bool updateCtrls = true)
        {
            isStreaming = false;
            isStreamingCheck = false;
            pbFile.Value = 0;
            pbFile.Maximum = 100;
            pbBuffer.Value = 0;

            signalPlay = 0;
            VisuGCode.ProcessedPath.ProcessedPathClear();
            btnStreamStart.Image = Properties.Resources.btn_play;
            btnStreamStart.BackColor = SystemColors.Control;
            btnStreamStart.Enabled = true;
            btnStreamCheck.Enabled = true;
            timerUpdateControlSource = "resetStreaming";
            if (updateCtrls)
            {
                UpdateControlEnables();
                pictureBox1.Invalidate();
            }
            ControlPowerSaving.EnableStandby();
        }
        private void BtnStreamPause_Click(object sender, EventArgs e)
        { UpdateLogging(); _serial_form.PauseStreaming(); }

        #endregion
    }
}