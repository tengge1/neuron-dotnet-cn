/***********************************************************************************************
 COPYRIGHT 2008 Vijeth D

 This file is part of NeuronDotNet XOR Sample.
 (Project Website : http://neurondotnet.freehostia.com)

 NeuronDotNet is a free software. You can redistribute it and/or modify it under the terms of
 the GNU General Public License as published by the Free Software Foundation, either version 3
 of the License, or (at your option) any later version.

 NeuronDotNet is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 See the GNU General Public License for more details.

 You should have received a copy of the GNU General Public License along with NeuronDotNet.
 If not, see <http://www.gnu.org/licenses/>.

 ***********************************************************************************************/

namespace BackPropNnTrainer
{
	partial class MainForm
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
            this.lblSSE = new System.Windows.Forms.Label();
            this.lblLearningRate = new System.Windows.Forms.Label();
            this.lblCycles = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorGraph = new ZedGraph.ZedGraphControl();
            this.lblTrainErrorVal = new System.Windows.Forms.Label();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.lblSumSqErrorCV = new System.Windows.Forms.Label();
            this.lblCvPercError = new System.Windows.Forms.Label();
            this.lblCVErrLabel = new System.Windows.Forms.Label();
            this.textParam2 = new System.Windows.Forms.TextBox();
            this.labelParam2 = new System.Windows.Forms.Label();
            this.textParam1 = new System.Windows.Forms.TextBox();
            this.labelParam1 = new System.Windows.Forms.Label();
            this.comboInitializeFunction = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonInitializeNet = new System.Windows.Forms.Button();
            this.comboLRFunction = new System.Windows.Forms.ComboBox();
            this.textMomentum = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFinalLearningRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnTrain = new System.Windows.Forms.Button();
            this.txtInitialLearningRate = new System.Windows.Forms.TextBox();
            this.txtCycles = new System.Windows.Forms.TextBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTrainingSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCVSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.networkDescription = new System.Windows.Forms.TextBox();
            this.grpSettings.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSSE
            // 
            this.lblSSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSSE.Location = new System.Drawing.Point(8, 220);
            this.lblSSE.Name = "lblSSE";
            this.lblSSE.Size = new System.Drawing.Size(102, 18);
            this.lblSSE.TabIndex = 13;
            this.lblSSE.Text = "误差平方和";
            this.lblSSE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLearningRate
            // 
            this.lblLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLearningRate.Location = new System.Drawing.Point(2, 48);
            this.lblLearningRate.Name = "lblLearningRate";
            this.lblLearningRate.Size = new System.Drawing.Size(116, 13);
            this.lblLearningRate.TabIndex = 2;
            this.lblLearningRate.Text = "初始学习率";
            this.lblLearningRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCycles
            // 
            this.lblCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCycles.Location = new System.Drawing.Point(27, 24);
            this.lblCycles.Name = "lblCycles";
            this.lblCycles.Size = new System.Drawing.Size(91, 13);
            this.lblCycles.TabIndex = 0;
            this.lblCycles.Text = "训练周期";
            this.lblCycles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(4, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 2);
            this.label1.TabIndex = 10;
            // 
            // errorGraph
            // 
            this.errorGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorGraph.AutoSize = true;
            this.errorGraph.Location = new System.Drawing.Point(236, 25);
            this.errorGraph.Name = "errorGraph";
            this.errorGraph.ScrollGrace = 0D;
            this.errorGraph.ScrollMaxX = 0D;
            this.errorGraph.ScrollMaxY = 0D;
            this.errorGraph.ScrollMaxY2 = 0D;
            this.errorGraph.ScrollMinX = 0D;
            this.errorGraph.ScrollMinY = 0D;
            this.errorGraph.ScrollMinY2 = 0D;
            this.errorGraph.Size = new System.Drawing.Size(662, 464);
            this.errorGraph.TabIndex = 4;
            // 
            // lblTrainErrorVal
            // 
            this.lblTrainErrorVal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTrainErrorVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrainErrorVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrainErrorVal.Location = new System.Drawing.Point(110, 219);
            this.lblTrainErrorVal.Name = "lblTrainErrorVal";
            this.lblTrainErrorVal.Size = new System.Drawing.Size(54, 19);
            this.lblTrainErrorVal.TabIndex = 14;
            this.lblTrainErrorVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpSettings
            // 
            this.grpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpSettings.Controls.Add(this.lblSumSqErrorCV);
            this.grpSettings.Controls.Add(this.lblCvPercError);
            this.grpSettings.Controls.Add(this.lblCVErrLabel);
            this.grpSettings.Controls.Add(this.textParam2);
            this.grpSettings.Controls.Add(this.labelParam2);
            this.grpSettings.Controls.Add(this.textParam1);
            this.grpSettings.Controls.Add(this.labelParam1);
            this.grpSettings.Controls.Add(this.comboInitializeFunction);
            this.grpSettings.Controls.Add(this.label4);
            this.grpSettings.Controls.Add(this.buttonInitializeNet);
            this.grpSettings.Controls.Add(this.comboLRFunction);
            this.grpSettings.Controls.Add(this.textMomentum);
            this.grpSettings.Controls.Add(this.label14);
            this.grpSettings.Controls.Add(this.label3);
            this.grpSettings.Controls.Add(this.txtFinalLearningRate);
            this.grpSettings.Controls.Add(this.label2);
            this.grpSettings.Controls.Add(this.progressBar);
            this.grpSettings.Controls.Add(this.btnTrain);
            this.grpSettings.Controls.Add(this.lblTrainErrorVal);
            this.grpSettings.Controls.Add(this.lblSSE);
            this.grpSettings.Controls.Add(this.txtInitialLearningRate);
            this.grpSettings.Controls.Add(this.lblLearningRate);
            this.grpSettings.Controls.Add(this.label1);
            this.grpSettings.Controls.Add(this.txtCycles);
            this.grpSettings.Controls.Add(this.lblCycles);
            this.grpSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSettings.Location = new System.Drawing.Point(12, 123);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(218, 336);
            this.grpSettings.TabIndex = 2;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "训练阶段";
            // 
            // lblSumSqErrorCV
            // 
            this.lblSumSqErrorCV.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblSumSqErrorCV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSumSqErrorCV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumSqErrorCV.Location = new System.Drawing.Point(110, 240);
            this.lblSumSqErrorCV.Name = "lblSumSqErrorCV";
            this.lblSumSqErrorCV.Size = new System.Drawing.Size(54, 19);
            this.lblSumSqErrorCV.TabIndex = 23;
            this.lblSumSqErrorCV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCvPercError
            // 
            this.lblCvPercError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCvPercError.Location = new System.Drawing.Point(162, 240);
            this.lblCvPercError.Name = "lblCvPercError";
            this.lblCvPercError.Size = new System.Drawing.Size(46, 18);
            this.lblCvPercError.TabIndex = 24;
            this.lblCvPercError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCVErrLabel
            // 
            this.lblCVErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCVErrLabel.Location = new System.Drawing.Point(18, 240);
            this.lblCVErrLabel.Name = "lblCVErrLabel";
            this.lblCVErrLabel.Size = new System.Drawing.Size(90, 18);
            this.lblCVErrLabel.TabIndex = 22;
            this.lblCVErrLabel.Text = "误差平方和CV";
            this.lblCVErrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textParam2
            // 
            this.textParam2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textParam2.Location = new System.Drawing.Point(157, 308);
            this.textParam2.Name = "textParam2";
            this.textParam2.Size = new System.Drawing.Size(51, 20);
            this.textParam2.TabIndex = 21;
            // 
            // labelParam2
            // 
            this.labelParam2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelParam2.Location = new System.Drawing.Point(112, 310);
            this.labelParam2.Name = "labelParam2";
            this.labelParam2.Size = new System.Drawing.Size(51, 13);
            this.labelParam2.TabIndex = 20;
            this.labelParam2.Text = "参数二";
            this.labelParam2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textParam1
            // 
            this.textParam1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textParam1.Location = new System.Drawing.Point(55, 308);
            this.textParam1.Name = "textParam1";
            this.textParam1.Size = new System.Drawing.Size(51, 20);
            this.textParam1.TabIndex = 19;
            // 
            // labelParam1
            // 
            this.labelParam1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelParam1.Location = new System.Drawing.Point(8, 310);
            this.labelParam1.Name = "labelParam1";
            this.labelParam1.Size = new System.Drawing.Size(51, 13);
            this.labelParam1.TabIndex = 18;
            this.labelParam1.Text = "参数一";
            this.labelParam1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboInitializeFunction
            // 
            this.comboInitializeFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboInitializeFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboInitializeFunction.FormattingEnabled = true;
            this.comboInitializeFunction.Location = new System.Drawing.Point(101, 278);
            this.comboInitializeFunction.Name = "comboInitializeFunction";
            this.comboInitializeFunction.Size = new System.Drawing.Size(111, 21);
            this.comboInitializeFunction.TabIndex = 17;
            this.comboInitializeFunction.SelectedIndexChanged += new System.EventHandler(this.comboInitializeFunction_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(5, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 2);
            this.label4.TabIndex = 15;
            // 
            // buttonInitializeNet
            // 
            this.buttonInitializeNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInitializeNet.Location = new System.Drawing.Point(10, 274);
            this.buttonInitializeNet.Name = "buttonInitializeNet";
            this.buttonInitializeNet.Size = new System.Drawing.Size(82, 25);
            this.buttonInitializeNet.TabIndex = 16;
            this.buttonInitializeNet.Text = "初始化网络";
            this.buttonInitializeNet.UseVisualStyleBackColor = true;
            this.buttonInitializeNet.Click += new System.EventHandler(this.buttonInitializeNet_Click);
            // 
            // comboLRFunction
            // 
            this.comboLRFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLRFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboLRFunction.FormattingEnabled = true;
            this.comboLRFunction.Location = new System.Drawing.Point(124, 94);
            this.comboLRFunction.Name = "comboLRFunction";
            this.comboLRFunction.Size = new System.Drawing.Size(86, 21);
            this.comboLRFunction.TabIndex = 7;
            // 
            // textMomentum
            // 
            this.textMomentum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMomentum.Location = new System.Drawing.Point(124, 119);
            this.textMomentum.Name = "textMomentum";
            this.textMomentum.Size = new System.Drawing.Size(51, 20);
            this.textMomentum.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(54, 121);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "动量";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "学习率函数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFinalLearningRate
            // 
            this.txtFinalLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinalLearningRate.Location = new System.Drawing.Point(124, 70);
            this.txtFinalLearningRate.Name = "txtFinalLearningRate";
            this.txtFinalLearningRate.Size = new System.Drawing.Size(51, 20);
            this.txtFinalLearningRate.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "最终学习率";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(11, 195);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(199, 18);
            this.progressBar.TabIndex = 12;
            // 
            // btnTrain
            // 
            this.btnTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrain.Location = new System.Drawing.Point(10, 164);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(200, 25);
            this.btnTrain.TabIndex = 11;
            this.btnTrain.Text = "训练";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.Train);
            // 
            // txtInitialLearningRate
            // 
            this.txtInitialLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInitialLearningRate.Location = new System.Drawing.Point(124, 46);
            this.txtInitialLearningRate.Name = "txtInitialLearningRate";
            this.txtInitialLearningRate.Size = new System.Drawing.Size(51, 20);
            this.txtInitialLearningRate.TabIndex = 3;
            // 
            // txtCycles
            // 
            this.txtCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycles.Location = new System.Drawing.Point(124, 22);
            this.txtCycles.Name = "txtCycles";
            this.txtCycles.Size = new System.Drawing.Size(51, 20);
            this.txtCycles.TabIndex = 1;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(22, 464);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(200, 25);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(906, 25);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.loadToolStripMenuItem.Text = "载入";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "保存";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveAsToolStripMenuItem.Text = "另存为";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.newToolStripMenuItem.Text = "新建";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "退出";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTrainingSetToolStripMenuItem,
            this.saveCVSetToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.optionsToolStripMenuItem.Text = "选项";
            // 
            // saveTrainingSetToolStripMenuItem
            // 
            this.saveTrainingSetToolStripMenuItem.Name = "saveTrainingSetToolStripMenuItem";
            this.saveTrainingSetToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveTrainingSetToolStripMenuItem.Text = "保存训练集";
            this.saveTrainingSetToolStripMenuItem.Click += new System.EventHandler(this.saveTrainingSetToolStripMenuItem_Click);
            // 
            // saveCVSetToolStripMenuItem
            // 
            this.saveCVSetToolStripMenuItem.Name = "saveCVSetToolStripMenuItem";
            this.saveCVSetToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveCVSetToolStripMenuItem.Text = "保存CVSet";
            this.saveCVSetToolStripMenuItem.Click += new System.EventHandler(this.saveCVSetToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.networkDescription);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 95);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "网络";
            // 
            // networkDescription
            // 
            this.networkDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.networkDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.networkDescription.Location = new System.Drawing.Point(19, 16);
            this.networkDescription.Multiline = true;
            this.networkDescription.Name = "networkDescription";
            this.networkDescription.ReadOnly = true;
            this.networkDescription.Size = new System.Drawing.Size(191, 74);
            this.networkDescription.TabIndex = 0;
            this.networkDescription.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 496);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.errorGraph);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "NDN Backprop NeuralNet Trainer";
            this.Load += new System.EventHandler(this.LoadForm);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private ZedGraph.ZedGraphControl errorGraph;
		private System.Windows.Forms.Label lblTrainErrorVal;
		private System.Windows.Forms.GroupBox grpSettings;
		private System.Windows.Forms.Button btnTrain;
		private System.Windows.Forms.TextBox txtInitialLearningRate;
		private System.Windows.Forms.TextBox txtCycles;
		private System.Windows.Forms.Label lblSSE;
		private System.Windows.Forms.Label lblLearningRate;
		private System.Windows.Forms.Label lblCycles;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.TextBox txtFinalLearningRate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textMomentum;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ComboBox comboLRFunction;
		private System.Windows.Forms.Button buttonInitializeNet;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox networkDescription;
		private System.Windows.Forms.ComboBox comboInitializeFunction;
		private System.Windows.Forms.TextBox textParam2;
		private System.Windows.Forms.Label labelParam2;
		private System.Windows.Forms.TextBox textParam1;
		private System.Windows.Forms.Label labelParam1;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveTrainingSetToolStripMenuItem;
		private System.Windows.Forms.Label lblSumSqErrorCV;
		private System.Windows.Forms.Label lblCVErrLabel;
		private System.Windows.Forms.Label lblCvPercError;
		private System.Windows.Forms.ToolStripMenuItem saveCVSetToolStripMenuItem;
	}
}

