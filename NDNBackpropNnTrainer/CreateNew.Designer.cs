namespace BackPropNnTrainer
{
    partial class formCreateNew
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
            this.buttonChooseSaveFolder = new System.Windows.Forms.Button();
            this.textSaveFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textProjectName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textTrainingSet = new System.Windows.Forms.TextBox();
            this.buttonChooseTrainingSet = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textCvSet = new System.Windows.Forms.TextBox();
            this.buttonChooseCrossValidationSet = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textNeuronCount1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboActFunction1 = new System.Windows.Forms.ComboBox();
            this.comboActFunction2 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textNeuronCount2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textMomentum = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textStartLearningRate = new System.Windows.Forms.TextBox();
            this.lblLearningRate = new System.Windows.Forms.Label();
            this.textTrainingCycles = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textFinalLearningRate = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboLRFunction = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.comboOutputFunction = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textInputCount = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textOutputCount = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonChooseSaveFolder
            // 
            this.buttonChooseSaveFolder.Location = new System.Drawing.Point(466, 31);
            this.buttonChooseSaveFolder.Name = "buttonChooseSaveFolder";
            this.buttonChooseSaveFolder.Size = new System.Drawing.Size(45, 23);
            this.buttonChooseSaveFolder.TabIndex = 4;
            this.buttonChooseSaveFolder.Text = "...";
            this.buttonChooseSaveFolder.UseVisualStyleBackColor = true;
            this.buttonChooseSaveFolder.Click += new System.EventHandler(this.buttonChooseSaveFolder_Click);
            // 
            // textSaveFolder
            // 
            this.textSaveFolder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textSaveFolder.Location = new System.Drawing.Point(105, 34);
            this.textSaveFolder.Name = "textSaveFolder";
            this.textSaveFolder.ReadOnly = true;
            this.textSaveFolder.Size = new System.Drawing.Size(355, 21);
            this.textSaveFolder.TabIndex = 3;
            this.textSaveFolder.Text = global::NDNBackpropNnTrainer.Properties.Settings.Default.NewNetworkSaveFolder;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "保存路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "工程名称";
            // 
            // textProjectName
            // 
            this.textProjectName.Location = new System.Drawing.Point(105, 8);
            this.textProjectName.Name = "textProjectName";
            this.textProjectName.Size = new System.Drawing.Size(222, 21);
            this.textProjectName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(22, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(500, 2);
            this.label3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "训练集";
            // 
            // textTrainingSet
            // 
            this.textTrainingSet.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textTrainingSet.Location = new System.Drawing.Point(105, 138);
            this.textTrainingSet.Name = "textTrainingSet";
            this.textTrainingSet.ReadOnly = true;
            this.textTrainingSet.Size = new System.Drawing.Size(355, 21);
            this.textTrainingSet.TabIndex = 7;
            // 
            // buttonChooseTrainingSet
            // 
            this.buttonChooseTrainingSet.Location = new System.Drawing.Point(466, 135);
            this.buttonChooseTrainingSet.Name = "buttonChooseTrainingSet";
            this.buttonChooseTrainingSet.Size = new System.Drawing.Size(45, 23);
            this.buttonChooseTrainingSet.TabIndex = 8;
            this.buttonChooseTrainingSet.Text = "...";
            this.buttonChooseTrainingSet.UseVisualStyleBackColor = true;
            this.buttonChooseTrainingSet.Click += new System.EventHandler(this.buttonChooseTrainingSet_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "交叉验证集";
            // 
            // textCvSet
            // 
            this.textCvSet.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textCvSet.Location = new System.Drawing.Point(136, 165);
            this.textCvSet.Name = "textCvSet";
            this.textCvSet.ReadOnly = true;
            this.textCvSet.Size = new System.Drawing.Size(324, 21);
            this.textCvSet.TabIndex = 10;
            // 
            // buttonChooseCrossValidationSet
            // 
            this.buttonChooseCrossValidationSet.Location = new System.Drawing.Point(466, 162);
            this.buttonChooseCrossValidationSet.Name = "buttonChooseCrossValidationSet";
            this.buttonChooseCrossValidationSet.Size = new System.Drawing.Size(45, 23);
            this.buttonChooseCrossValidationSet.TabIndex = 11;
            this.buttonChooseCrossValidationSet.Text = "...";
            this.buttonChooseCrossValidationSet.UseVisualStyleBackColor = true;
            this.buttonChooseCrossValidationSet.Click += new System.EventHandler(this.buttonChooseCrossValidationSet_Click);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(22, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(500, 2);
            this.label6.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "隐层 1：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(133, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "神经元计数";
            // 
            // textNeuronCount1
            // 
            this.textNeuronCount1.Location = new System.Drawing.Point(212, 213);
            this.textNeuronCount1.Name = "textNeuronCount1";
            this.textNeuronCount1.Size = new System.Drawing.Size(51, 21);
            this.textNeuronCount1.TabIndex = 15;
            this.textNeuronCount1.Text = "10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(289, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "激活函数";
            // 
            // comboActFunction1
            // 
            this.comboActFunction1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboActFunction1.FormattingEnabled = true;
            this.comboActFunction1.Location = new System.Drawing.Point(393, 212);
            this.comboActFunction1.Name = "comboActFunction1";
            this.comboActFunction1.Size = new System.Drawing.Size(118, 20);
            this.comboActFunction1.TabIndex = 17;
            // 
            // comboActFunction2
            // 
            this.comboActFunction2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboActFunction2.FormattingEnabled = true;
            this.comboActFunction2.Location = new System.Drawing.Point(393, 239);
            this.comboActFunction2.Name = "comboActFunction2";
            this.comboActFunction2.Size = new System.Drawing.Size(118, 20);
            this.comboActFunction2.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(289, 242);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "激活函数";
            // 
            // textNeuronCount2
            // 
            this.textNeuronCount2.Location = new System.Drawing.Point(212, 239);
            this.textNeuronCount2.Name = "textNeuronCount2";
            this.textNeuronCount2.Size = new System.Drawing.Size(51, 21);
            this.textNeuronCount2.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(133, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "神经元计数";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(36, 242);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 18;
            this.label12.Text = "隐层 2：";
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Location = new System.Drawing.Point(22, 299);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(500, 2);
            this.label13.TabIndex = 23;
            // 
            // textMomentum
            // 
            this.textMomentum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMomentum.Location = new System.Drawing.Point(88, 342);
            this.textMomentum.Name = "textMomentum";
            this.textMomentum.Size = new System.Drawing.Size(51, 20);
            this.textMomentum.TabIndex = 31;
            this.textMomentum.Text = "0.15";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(26, 344);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "动量";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textStartLearningRate
            // 
            this.textStartLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStartLearningRate.Location = new System.Drawing.Point(127, 313);
            this.textStartLearningRate.Name = "textStartLearningRate";
            this.textStartLearningRate.Size = new System.Drawing.Size(51, 20);
            this.textStartLearningRate.TabIndex = 25;
            this.textStartLearningRate.Text = "0.3";
            // 
            // lblLearningRate
            // 
            this.lblLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLearningRate.Location = new System.Drawing.Point(27, 315);
            this.lblLearningRate.Name = "lblLearningRate";
            this.lblLearningRate.Size = new System.Drawing.Size(103, 13);
            this.lblLearningRate.TabIndex = 24;
            this.lblLearningRate.Text = "初始学习率";
            this.lblLearningRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textTrainingCycles
            // 
            this.textTrainingCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTrainingCycles.Location = new System.Drawing.Point(250, 342);
            this.textTrainingCycles.Name = "textTrainingCycles";
            this.textTrainingCycles.Size = new System.Drawing.Size(51, 20);
            this.textTrainingCycles.TabIndex = 33;
            this.textTrainingCycles.Text = "5000";
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(149, 344);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 13);
            this.label16.TabIndex = 32;
            this.label16.Text = "最大训练周期";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(20, 390);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(83, 22);
            this.buttonOK.TabIndex = 37;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label15.Location = new System.Drawing.Point(22, 377);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(500, 2);
            this.label15.TabIndex = 36;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(112, 390);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(83, 22);
            this.buttonCancel.TabIndex = 38;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textFinalLearningRate
            // 
            this.textFinalLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFinalLearningRate.Location = new System.Drawing.Point(282, 313);
            this.textFinalLearningRate.Name = "textFinalLearningRate";
            this.textFinalLearningRate.Size = new System.Drawing.Size(51, 20);
            this.textFinalLearningRate.TabIndex = 27;
            this.textFinalLearningRate.Text = "0.1";
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(184, 315);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(103, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "最终学习率";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboLRFunction
            // 
            this.comboLRFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLRFunction.FormattingEnabled = true;
            this.comboLRFunction.Location = new System.Drawing.Point(402, 312);
            this.comboLRFunction.Name = "comboLRFunction";
            this.comboLRFunction.Size = new System.Drawing.Size(107, 20);
            this.comboLRFunction.TabIndex = 29;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(345, 316);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 28;
            this.label18.Text = "LR函数";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(29, 78);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(371, 12);
            this.label19.TabIndex = 39;
            this.label19.Text = "注意：数据文件列必须以逗号分隔。 输入计数默认为<总列数> - 1。";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(29, 92);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(413, 12);
            this.label20.TabIndex = 40;
            this.label20.Text = "输出计数默认为1（最后一列）。 如果两者都指定，任何额外的列将被忽略。";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(36, 270);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 12);
            this.label22.TabIndex = 43;
            this.label22.Text = "输出层：";
            // 
            // comboOutputFunction
            // 
            this.comboOutputFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOutputFunction.FormattingEnabled = true;
            this.comboOutputFunction.Location = new System.Drawing.Point(239, 267);
            this.comboOutputFunction.Name = "comboOutputFunction";
            this.comboOutputFunction.Size = new System.Drawing.Size(118, 20);
            this.comboOutputFunction.TabIndex = 45;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(135, 270);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 44;
            this.label23.Text = "激活函数";
            // 
            // textInputCount
            // 
            this.textInputCount.Location = new System.Drawing.Point(105, 112);
            this.textInputCount.Name = "textInputCount";
            this.textInputCount.Size = new System.Drawing.Size(51, 21);
            this.textInputCount.TabIndex = 47;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(29, 114);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 46;
            this.label24.Text = "输入计数";
            // 
            // textOutputCount
            // 
            this.textOutputCount.Location = new System.Drawing.Point(250, 112);
            this.textOutputCount.Name = "textOutputCount";
            this.textOutputCount.Size = new System.Drawing.Size(51, 21);
            this.textOutputCount.TabIndex = 49;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(174, 114);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 12);
            this.label25.TabIndex = 48;
            this.label25.Text = "输出计数";
            // 
            // formCreateNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(545, 436);
            this.Controls.Add(this.textOutputCount);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.textInputCount);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.comboOutputFunction);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.comboLRFunction);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textFinalLearningRate);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textTrainingCycles);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textMomentum);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textStartLearningRate);
            this.Controls.Add(this.lblLearningRate);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboActFunction2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textNeuronCount2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboActFunction1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textNeuronCount1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textCvSet);
            this.Controls.Add(this.buttonChooseCrossValidationSet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textTrainingSet);
            this.Controls.Add(this.buttonChooseTrainingSet);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textProjectName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textSaveFolder);
            this.Controls.Add(this.buttonChooseSaveFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formCreateNew";
            this.ShowInTaskbar = false;
            this.Text = "新建神经网络";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonChooseSaveFolder;
        private System.Windows.Forms.TextBox textSaveFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textProjectName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textTrainingSet;
        private System.Windows.Forms.Button buttonChooseTrainingSet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textCvSet;
        private System.Windows.Forms.Button buttonChooseCrossValidationSet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textNeuronCount1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboActFunction1;
        private System.Windows.Forms.ComboBox comboActFunction2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textNeuronCount2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textMomentum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textStartLearningRate;
        private System.Windows.Forms.Label lblLearningRate;
        private System.Windows.Forms.TextBox textTrainingCycles;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textFinalLearningRate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboLRFunction;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox comboOutputFunction;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textInputCount;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textOutputCount;
        private System.Windows.Forms.Label label25;
    }
}