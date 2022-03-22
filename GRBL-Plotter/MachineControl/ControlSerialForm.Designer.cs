﻿/*  GRBL-Plotter. Another GCode sender for GRBL.
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
namespace GrblPlotter
{
    partial class ControlSerialForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
				timerSerial.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlSerialForm));
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.cbBaud = new System.Windows.Forms.ComboBox();
            this.BtnOpenPortSerial = new System.Windows.Forms.Button();
            this.btnScanPort = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteCodeFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectionToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnGRBLCommand0 = new System.Windows.Forms.Button();
            this.btnGRBLCommand1 = new System.Windows.Forms.Button();
            this.btnGRBLCommand2 = new System.Windows.Forms.Button();
            this.btnGRBLCommand3 = new System.Windows.Forms.Button();
            this.toolTipSerial = new System.Windows.Forms.ToolTip(this.components);
            this.btnGRBLCommand4 = new System.Windows.Forms.Button();
            this.lblSrPos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSrBf = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSrFS = new System.Windows.Forms.Label();
            this.lblSrPn = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSrOv = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSrA = new System.Windows.Forms.Label();
            this.LblAccessoryState = new System.Windows.Forms.Label();
            this.lblSrLn = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCheckGRBL = new System.Windows.Forms.Button();
            this.btnGRBLReset = new System.Windows.Forms.Button();
            this.btnGRBLHardReset = new System.Windows.Forms.Button();
            this.btnGRBLCmndParser = new System.Windows.Forms.Button();
            this.btnGRBLCmndBuild = new System.Windows.Forms.Button();
            this.cBStatus = new System.Windows.Forms.CheckBox();
            this.cBStatus1 = new System.Windows.Forms.CheckBox();
            this.lblSrState = new System.Windows.Forms.Label();
            this.cBCommand = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheckGRBLResult = new System.Windows.Forms.Button();
            this.TbEthernetIP = new System.Windows.Forms.TextBox();
            this.CbEthernetUse = new System.Windows.Forms.CheckBox();
            this.TbEthernetPort = new System.Windows.Forms.TextBox();
            this.LblEthernetIP = new System.Windows.Forms.Label();
            this.LblEthernetPort = new System.Windows.Forms.Label();
            this.BtnOpenPortEthernet = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            resources.ApplyResources(this.cbPort, "cbPort");
            this.cbPort.Name = "cbPort";
            this.toolTipSerial.SetToolTip(this.cbPort, resources.GetString("cbPort.ToolTip"));
            // 
            // cbBaud
            // 
            this.cbBaud.FormattingEnabled = true;
            this.cbBaud.Items.AddRange(new object[] {
            resources.GetString("cbBaud.Items"),
            resources.GetString("cbBaud.Items1"),
            resources.GetString("cbBaud.Items2"),
            resources.GetString("cbBaud.Items3"),
            resources.GetString("cbBaud.Items4"),
            resources.GetString("cbBaud.Items5")});
            resources.ApplyResources(this.cbBaud, "cbBaud");
            this.cbBaud.Name = "cbBaud";
            this.toolTipSerial.SetToolTip(this.cbBaud, resources.GetString("cbBaud.ToolTip"));
            // 
            // BtnOpenPortSerial
            // 
            resources.ApplyResources(this.BtnOpenPortSerial, "BtnOpenPortSerial");
            this.BtnOpenPortSerial.Name = "BtnOpenPortSerial";
            this.toolTipSerial.SetToolTip(this.BtnOpenPortSerial, resources.GetString("BtnOpenPortSerial.ToolTip"));
            this.BtnOpenPortSerial.UseVisualStyleBackColor = true;
            this.BtnOpenPortSerial.Click += new System.EventHandler(this.BtnOpenPortSerial_Click);
            // 
            // btnScanPort
            // 
            resources.ApplyResources(this.btnScanPort, "btnScanPort");
            this.btnScanPort.Name = "btnScanPort";
            this.toolTipSerial.SetToolTip(this.btnScanPort, resources.GetString("btnScanPort.ToolTip"));
            this.btnScanPort.UseVisualStyleBackColor = true;
            this.btnScanPort.Click += new System.EventHandler(this.BtnScanPort_Click);
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 115200;
            this.serialPort.ReadBufferSize = 2048;
            this.serialPort.ReadTimeout = 3000;
            this.serialPort.WriteTimeout = 3000;
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataReceived);
            // 
            // rtbLog
            // 
            this.rtbLog.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.rtbLog, "rtbLog");
            this.rtbLog.Name = "rtbLog";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteCodeFromClipboardToolStripMenuItem,
            this.copySelectionToClipboardToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // pasteCodeFromClipboardToolStripMenuItem
            // 
            this.pasteCodeFromClipboardToolStripMenuItem.Name = "pasteCodeFromClipboardToolStripMenuItem";
            resources.ApplyResources(this.pasteCodeFromClipboardToolStripMenuItem, "pasteCodeFromClipboardToolStripMenuItem");
            this.pasteCodeFromClipboardToolStripMenuItem.Click += new System.EventHandler(this.PasteCodeFromClipboardToolStripMenuItem_Click);
            // 
            // copySelectionToClipboardToolStripMenuItem
            // 
            this.copySelectionToClipboardToolStripMenuItem.Name = "copySelectionToClipboardToolStripMenuItem";
            resources.ApplyResources(this.copySelectionToClipboardToolStripMenuItem, "copySelectionToClipboardToolStripMenuItem");
            this.copySelectionToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopySelectionToClipboardToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.toolTipSerial.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.Name = "btnSend";
            this.toolTipSerial.SetToolTip(this.btnSend, resources.GetString("btnSend.ToolTip"));
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // btnGRBLCommand0
            // 
            resources.ApplyResources(this.btnGRBLCommand0, "btnGRBLCommand0");
            this.btnGRBLCommand0.Name = "btnGRBLCommand0";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand0, resources.GetString("btnGRBLCommand0.ToolTip"));
            this.btnGRBLCommand0.UseVisualStyleBackColor = true;
            this.btnGRBLCommand0.Click += new System.EventHandler(this.BtnGRBLCommand0_Click);
            // 
            // btnGRBLCommand1
            // 
            resources.ApplyResources(this.btnGRBLCommand1, "btnGRBLCommand1");
            this.btnGRBLCommand1.Name = "btnGRBLCommand1";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand1, resources.GetString("btnGRBLCommand1.ToolTip"));
            this.btnGRBLCommand1.UseVisualStyleBackColor = true;
            this.btnGRBLCommand1.Click += new System.EventHandler(this.BtnGRBLCommand1_Click);
            // 
            // btnGRBLCommand2
            // 
            resources.ApplyResources(this.btnGRBLCommand2, "btnGRBLCommand2");
            this.btnGRBLCommand2.Name = "btnGRBLCommand2";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand2, resources.GetString("btnGRBLCommand2.ToolTip"));
            this.btnGRBLCommand2.UseVisualStyleBackColor = true;
            this.btnGRBLCommand2.Click += new System.EventHandler(this.BtnGRBLCommand2_Click);
            // 
            // btnGRBLCommand3
            // 
            resources.ApplyResources(this.btnGRBLCommand3, "btnGRBLCommand3");
            this.btnGRBLCommand3.Name = "btnGRBLCommand3";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand3, resources.GetString("btnGRBLCommand3.ToolTip"));
            this.btnGRBLCommand3.UseVisualStyleBackColor = true;
            this.btnGRBLCommand3.Click += new System.EventHandler(this.BtnGRBLCommand3_Click);
            // 
            // btnGRBLCommand4
            // 
            resources.ApplyResources(this.btnGRBLCommand4, "btnGRBLCommand4");
            this.btnGRBLCommand4.Name = "btnGRBLCommand4";
            this.toolTipSerial.SetToolTip(this.btnGRBLCommand4, resources.GetString("btnGRBLCommand4.ToolTip"));
            this.btnGRBLCommand4.UseVisualStyleBackColor = true;
            this.btnGRBLCommand4.Click += new System.EventHandler(this.BtnGRBLCommand4_Click);
            // 
            // lblSrPos
            // 
            resources.ApplyResources(this.lblSrPos, "lblSrPos");
            this.lblSrPos.Name = "lblSrPos";
            this.toolTipSerial.SetToolTip(this.lblSrPos, resources.GetString("lblSrPos.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTipSerial.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // lblSrBf
            // 
            resources.ApplyResources(this.lblSrBf, "lblSrBf");
            this.lblSrBf.Name = "lblSrBf";
            this.toolTipSerial.SetToolTip(this.lblSrBf, resources.GetString("lblSrBf.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTipSerial.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // lblSrFS
            // 
            resources.ApplyResources(this.lblSrFS, "lblSrFS");
            this.lblSrFS.Name = "lblSrFS";
            this.toolTipSerial.SetToolTip(this.lblSrFS, resources.GetString("lblSrFS.ToolTip"));
            // 
            // lblSrPn
            // 
            resources.ApplyResources(this.lblSrPn, "lblSrPn");
            this.lblSrPn.Name = "lblSrPn";
            this.toolTipSerial.SetToolTip(this.lblSrPn, resources.GetString("lblSrPn.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTipSerial.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // lblSrOv
            // 
            resources.ApplyResources(this.lblSrOv, "lblSrOv");
            this.lblSrOv.Name = "lblSrOv";
            this.toolTipSerial.SetToolTip(this.lblSrOv, resources.GetString("lblSrOv.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTipSerial.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // lblSrA
            // 
            resources.ApplyResources(this.lblSrA, "lblSrA");
            this.lblSrA.Name = "lblSrA";
            this.toolTipSerial.SetToolTip(this.lblSrA, resources.GetString("lblSrA.ToolTip"));
            // 
            // LblAccessoryState
            // 
            resources.ApplyResources(this.LblAccessoryState, "LblAccessoryState");
            this.LblAccessoryState.BackColor = System.Drawing.Color.Yellow;
            this.LblAccessoryState.Name = "LblAccessoryState";
            this.toolTipSerial.SetToolTip(this.LblAccessoryState, resources.GetString("LblAccessoryState.ToolTip"));
            this.LblAccessoryState.Click += new System.EventHandler(this.LblAccessoryState_Click);
            // 
            // lblSrLn
            // 
            resources.ApplyResources(this.lblSrLn, "lblSrLn");
            this.lblSrLn.Name = "lblSrLn";
            this.toolTipSerial.SetToolTip(this.lblSrLn, resources.GetString("lblSrLn.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTipSerial.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // btnCheckGRBL
            // 
            resources.ApplyResources(this.btnCheckGRBL, "btnCheckGRBL");
            this.btnCheckGRBL.Name = "btnCheckGRBL";
            this.toolTipSerial.SetToolTip(this.btnCheckGRBL, resources.GetString("btnCheckGRBL.ToolTip"));
            this.btnCheckGRBL.UseVisualStyleBackColor = true;
            this.btnCheckGRBL.Click += new System.EventHandler(this.BtnCheckGRBL_Click);
            // 
            // btnGRBLReset
            // 
            resources.ApplyResources(this.btnGRBLReset, "btnGRBLReset");
            this.btnGRBLReset.Name = "btnGRBLReset";
            this.toolTipSerial.SetToolTip(this.btnGRBLReset, resources.GetString("btnGRBLReset.ToolTip"));
            this.btnGRBLReset.UseVisualStyleBackColor = true;
            this.btnGRBLReset.Click += new System.EventHandler(this.BtnGRBLReset_Click);
            // 
            // btnGRBLHardReset
            // 
            resources.ApplyResources(this.btnGRBLHardReset, "btnGRBLHardReset");
            this.btnGRBLHardReset.Name = "btnGRBLHardReset";
            this.toolTipSerial.SetToolTip(this.btnGRBLHardReset, resources.GetString("btnGRBLHardReset.ToolTip"));
            this.btnGRBLHardReset.UseVisualStyleBackColor = true;
            this.btnGRBLHardReset.Click += new System.EventHandler(this.BtnGRBLHardReset_Click);
            // 
            // btnGRBLCmndParser
            // 
            resources.ApplyResources(this.btnGRBLCmndParser, "btnGRBLCmndParser");
            this.btnGRBLCmndParser.Name = "btnGRBLCmndParser";
            this.toolTipSerial.SetToolTip(this.btnGRBLCmndParser, resources.GetString("btnGRBLCmndParser.ToolTip"));
            this.btnGRBLCmndParser.UseVisualStyleBackColor = true;
            this.btnGRBLCmndParser.Click += new System.EventHandler(this.BtnGRBLCmndParser_Click);
            // 
            // btnGRBLCmndBuild
            // 
            resources.ApplyResources(this.btnGRBLCmndBuild, "btnGRBLCmndBuild");
            this.btnGRBLCmndBuild.Name = "btnGRBLCmndBuild";
            this.toolTipSerial.SetToolTip(this.btnGRBLCmndBuild, resources.GetString("btnGRBLCmndBuild.ToolTip"));
            this.btnGRBLCmndBuild.UseVisualStyleBackColor = true;
            this.btnGRBLCmndBuild.Click += new System.EventHandler(this.BtnGRBLCmndBuild_Click);
            // 
            // cBStatus
            // 
            resources.ApplyResources(this.cBStatus, "cBStatus");
            this.cBStatus.Name = "cBStatus";
            this.toolTipSerial.SetToolTip(this.cBStatus, resources.GetString("cBStatus.ToolTip"));
            this.cBStatus.UseVisualStyleBackColor = true;
            // 
            // cBStatus1
            // 
            resources.ApplyResources(this.cBStatus1, "cBStatus1");
            this.cBStatus1.Name = "cBStatus1";
            this.toolTipSerial.SetToolTip(this.cBStatus1, resources.GetString("cBStatus1.ToolTip"));
            this.cBStatus1.UseVisualStyleBackColor = true;
            this.cBStatus1.CheckedChanged += new System.EventHandler(this.CbStatus1_CheckedChanged);
            // 
            // lblSrState
            // 
            resources.ApplyResources(this.lblSrState, "lblSrState");
            this.lblSrState.Name = "lblSrState";
            // 
            // cBCommand
            // 
            this.cBCommand.FormattingEnabled = true;
            this.cBCommand.Items.AddRange(new object[] {
            resources.GetString("cBCommand.Items"),
            resources.GetString("cBCommand.Items1"),
            resources.GetString("cBCommand.Items2"),
            resources.GetString("cBCommand.Items3"),
            resources.GetString("cBCommand.Items4")});
            resources.ApplyResources(this.cBCommand, "cBCommand");
            this.cBCommand.Name = "cBCommand";
            this.cBCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbCommand_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBStatus1);
            this.groupBox1.Controls.Add(this.btnCheckGRBLResult);
            this.groupBox1.Controls.Add(this.btnCheckGRBL);
            this.groupBox1.Controls.Add(this.cBStatus);
            this.groupBox1.Controls.Add(this.lblSrA);
            this.groupBox1.Controls.Add(this.lblSrLn);
            this.groupBox1.Controls.Add(this.LblAccessoryState);
            this.groupBox1.Controls.Add(this.lblSrState);
            this.groupBox1.Controls.Add(this.lblSrOv);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblSrPn);
            this.groupBox1.Controls.Add(this.lblSrPos);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblSrFS);
            this.groupBox1.Controls.Add(this.lblSrBf);
            this.groupBox1.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnCheckGRBLResult
            // 
            resources.ApplyResources(this.btnCheckGRBLResult, "btnCheckGRBLResult");
            this.btnCheckGRBLResult.Name = "btnCheckGRBLResult";
            this.btnCheckGRBLResult.UseVisualStyleBackColor = true;
            this.btnCheckGRBLResult.Click += new System.EventHandler(this.BtnCheckGRBLResult_Click);
            // 
            // TbEthernetIP
            // 
            resources.ApplyResources(this.TbEthernetIP, "TbEthernetIP");
            this.TbEthernetIP.Name = "TbEthernetIP";
            // 
            // CbEthernetUse
            // 
            resources.ApplyResources(this.CbEthernetUse, "CbEthernetUse");
            this.CbEthernetUse.Name = "CbEthernetUse";
            this.CbEthernetUse.CheckedChanged += new System.EventHandler(this.CbEthernetUse_CheckedChanged);
            // 
            // TbEthernetPort
            // 
            resources.ApplyResources(this.TbEthernetPort, "TbEthernetPort");
            this.TbEthernetPort.Name = "TbEthernetPort";
            // 
            // LblEthernetIP
            // 
            resources.ApplyResources(this.LblEthernetIP, "LblEthernetIP");
            this.LblEthernetIP.Name = "LblEthernetIP";
            // 
            // LblEthernetPort
            // 
            resources.ApplyResources(this.LblEthernetPort, "LblEthernetPort");
            this.LblEthernetPort.Name = "LblEthernetPort";
            // 
            // BtnOpenPortEthernet
            // 
            resources.ApplyResources(this.BtnOpenPortEthernet, "BtnOpenPortEthernet");
            this.BtnOpenPortEthernet.Name = "BtnOpenPortEthernet";
            this.BtnOpenPortEthernet.UseVisualStyleBackColor = true;
            this.BtnOpenPortEthernet.Click += new System.EventHandler(this.BtnOpenPortEthernet_Click);
            // 
            // ControlSerialForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.TbEthernetPort);
            this.Controls.Add(this.BtnOpenPortEthernet);
            this.Controls.Add(this.LblEthernetPort);
            this.Controls.Add(this.LblEthernetIP);
            this.Controls.Add(this.TbEthernetIP);
            this.Controls.Add(this.btnScanPort);
            this.Controls.Add(this.CbEthernetUse);
            this.Controls.Add(this.btnGRBLCmndBuild);
            this.Controls.Add(this.btnGRBLCmndParser);
            this.Controls.Add(this.btnGRBLHardReset);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cBCommand);
            this.Controls.Add(this.btnGRBLReset);
            this.Controls.Add(this.btnGRBLCommand4);
            this.Controls.Add(this.btnGRBLCommand3);
            this.Controls.Add(this.btnGRBLCommand2);
            this.Controls.Add(this.btnGRBLCommand1);
            this.Controls.Add(this.btnGRBLCommand0);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.BtnOpenPortSerial);
            this.Controls.Add(this.cbBaud);
            this.Controls.Add(this.cbPort);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ControlSerialForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialForm_FormClosing);
            this.Load += new System.EventHandler(this.SerialForm_Load);
            this.Resize += new System.EventHandler(this.SerialForm_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ComboBox cbBaud;
        private System.Windows.Forms.Button BtnOpenPortSerial;
        private System.Windows.Forms.Button btnScanPort;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnGRBLCommand0;
        private System.Windows.Forms.Button btnGRBLCommand1;
        private System.Windows.Forms.ToolTip toolTipSerial;
        private System.Windows.Forms.Button btnGRBLCommand2;
        private System.Windows.Forms.Button btnGRBLCommand3;
//        private System.Windows.Forms.Timer timerSerial;
        private System.Windows.Forms.Button btnGRBLCommand4;
        private System.Windows.Forms.Button btnGRBLReset;
        private System.Windows.Forms.Label lblSrState;
        private System.Windows.Forms.Label lblSrPos;
        private System.Windows.Forms.ComboBox cBCommand;
        private System.Windows.Forms.CheckBox cBStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSrBf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSrFS;
        private System.Windows.Forms.Label lblSrPn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSrOv;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSrA;
        private System.Windows.Forms.Label LblAccessoryState;
        private System.Windows.Forms.Label lblSrLn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCheckGRBL;
        private System.Windows.Forms.Button btnCheckGRBLResult;
        private System.Windows.Forms.Button btnGRBLHardReset;
        private System.Windows.Forms.Button btnGRBLCmndParser;
        private System.Windows.Forms.Button btnGRBLCmndBuild;
        private System.Windows.Forms.CheckBox CbEthernetUse;
        private System.Windows.Forms.TextBox TbEthernetIP;
        private System.Windows.Forms.CheckBox cBStatus1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pasteCodeFromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectionToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.TextBox TbEthernetPort;
        private System.Windows.Forms.Label LblEthernetIP;
        private System.Windows.Forms.Label LblEthernetPort;
        private System.Windows.Forms.Button BtnOpenPortEthernet;
    }
}