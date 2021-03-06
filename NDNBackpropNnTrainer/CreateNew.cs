﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.LearningRateFunctions;

namespace BackPropNnTrainer
{
    public partial class formCreateNew : Form
    {
        public NnProject NewProject;

        public formCreateNew()
        {
            InitializeComponent();

            // 填充层组合框
            foreach (HiddenLayerType hlt in Enum.GetValues(typeof(HiddenLayerType)))
            {
                comboActFunction1.Items.Add(hlt);
                comboActFunction2.Items.Add(hlt);
                comboOutputFunction.Items.Add(hlt);
            }
            comboActFunction1.SelectedIndex = 2;
            comboOutputFunction.SelectedIndex = 2;

            // 填充学习函数组合框
            comboLRFunction.Items.Add("(none)");
            foreach (LearningRateFunction lrf in Enum.GetValues(typeof(LearningRateFunction)))
            {
                comboLRFunction.Items.Add(lrf);
            }
            comboLRFunction.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //
                // 解析数据文件
                //
                int inputCount = 0;
                int outputCount = 1;
                if (textInputCount.Text.Length > 0) inputCount = int.Parse(textInputCount.Text);
                if (textOutputCount.Text.Length > 0) outputCount = int.Parse(textOutputCount.Text);
                TrainingSet trainingSet = DataFile.CsvFileToTrainingSet(textTrainingSet.Text, ref inputCount, ref outputCount);
                TrainingSet crossvalidSet = null;
                if (textCvSet.Text.Length > 0) crossvalidSet = DataFile.CsvFileToTrainingSet(textCvSet.Text, ref inputCount, ref outputCount);


                //
                // 创建新网络
                //

                // 输入层始终与输入计数是线性关系
                LinearLayer inputLayer = new LinearLayer(inputCount);

                // 创建隐层
                ActivationLayer hiddenLayer1 = null;
                ActivationLayer hiddenLayer2 = null;
                ActivationLayer outputLayer = null;
                if (comboActFunction1.SelectedIndex < 0) { MessageBox.Show("请选择激活函数！"); return; }
                switch ((HiddenLayerType)comboActFunction1.SelectedItem)
                {
                    case HiddenLayerType.Linear: hiddenLayer1 = new LinearLayer(int.Parse(textNeuronCount1.Text)); break;
                    case HiddenLayerType.Logarithmic: hiddenLayer1 = new LogarithmLayer(int.Parse(textNeuronCount1.Text)); break;
                    case HiddenLayerType.Sigmoid: hiddenLayer1 = new SigmoidLayer(int.Parse(textNeuronCount1.Text)); break;
                    case HiddenLayerType.Sine: hiddenLayer1 = new SineLayer(int.Parse(textNeuronCount1.Text)); break;
                    case HiddenLayerType.Tanh: hiddenLayer1 = new TanhLayer(int.Parse(textNeuronCount1.Text)); break;
                }
                if (textNeuronCount2.Text.Length > 0 && int.Parse(textNeuronCount2.Text) > 0)
                {
                    switch ((HiddenLayerType)comboActFunction2.SelectedItem)
                    {
                        case HiddenLayerType.Linear: hiddenLayer2 = new LinearLayer(int.Parse(textNeuronCount2.Text)); break;
                        case HiddenLayerType.Logarithmic: hiddenLayer2 = new LogarithmLayer(int.Parse(textNeuronCount2.Text)); break;
                        case HiddenLayerType.Sigmoid: hiddenLayer2 = new SigmoidLayer(int.Parse(textNeuronCount2.Text)); break;
                        case HiddenLayerType.Sine: hiddenLayer2 = new SineLayer(int.Parse(textNeuronCount2.Text)); break;
                        case HiddenLayerType.Tanh: hiddenLayer2 = new TanhLayer(int.Parse(textNeuronCount2.Text)); break;
                    }
                }

                if (comboOutputFunction.SelectedIndex < 0) { MessageBox.Show("请选择输出函数！"); return; }
                switch ((HiddenLayerType)comboOutputFunction.SelectedItem)
                {
                    case HiddenLayerType.Linear: outputLayer = new LinearLayer(outputCount); break;
                    case HiddenLayerType.Logarithmic: outputLayer = new LogarithmLayer(outputCount); break;
                    case HiddenLayerType.Sigmoid: outputLayer = new SigmoidLayer(outputCount); break;
                    case HiddenLayerType.Sine: outputLayer = new SineLayer(outputCount); break;
                    case HiddenLayerType.Tanh: outputLayer = new TanhLayer(outputCount); break;
                }

                // 连接层, hidden2是可选的
                new BackpropagationConnector(inputLayer, hiddenLayer1);
                if (hiddenLayer2 != null)
                {
                    new BackpropagationConnector(hiddenLayer1, hiddenLayer2);
                    new BackpropagationConnector(hiddenLayer2, outputLayer);
                }
                else
                {
                    new BackpropagationConnector(hiddenLayer1, outputLayer);
                }
                BackpropagationNetwork backpropNetwork = new BackpropagationNetwork(inputLayer, outputLayer);


                //
                // 设置学习和退出参数
                //

                double startLearningRate = double.Parse(textStartLearningRate.Text);
                double? finalLearningRate = null;
                if (textFinalLearningRate.Text.Length > 0) finalLearningRate = double.Parse(textFinalLearningRate.Text);

                // 如果选择了学习率函数则使用
                LearningRateFunction? lrf = null;
                if (comboLRFunction.SelectedIndex > 0)
                {
                    lrf = (LearningRateFunction)comboLRFunction.SelectedItem;
                    backpropNetwork.SetLearningRate(
                        LearningRateFactory.GetLearningRateFunction(lrf.Value, startLearningRate, finalLearningRate.Value));
                }
                else
                {
                    // 否则使用普通学习率，也许有起点和终点
                    if (finalLearningRate.HasValue)
                        backpropNetwork.SetLearningRate(startLearningRate, finalLearningRate.Value);
                    else
                        backpropNetwork.SetLearningRate(startLearningRate);
                }

                // 如果给定，在连接器中设置动量
                double? momentum = null;
                if (textMomentum.Text.Length > 0)
                {
                    momentum = double.Parse(textMomentum.Text);
                    foreach (ILayer layer in backpropNetwork.Layers)
                    {
                        foreach (BackpropagationConnector conn in layer.SourceConnectors)
                            conn.Momentum = momentum.Value;
                        foreach (BackpropagationConnector conn in layer.TargetConnectors)
                            conn.Momentum = momentum.Value;
                    }
                }


                //
                // 新建工程和保存工程
                // 
                int tmpInt;
                NewProject = new NnProject();
                NewProject.Network = backpropNetwork;
                // 确保为新网络初始化权重，默认情况下新的训练周期将不会初始化
                NewProject.Network.Initialize();
                NewProject.ProjectName = textProjectName.Text.Trim();
                NewProject.SaveFolder = textSaveFolder.Text;
                NewProject.TrainingSet = trainingSet;
                NewProject.CrossValidationSet = crossvalidSet;
                NewProject.LearningParameters = new NnProject.NnLearningParameters();
                NewProject.LearningParameters.InitialLearningRate = startLearningRate;
                NewProject.LearningParameters.FinalLearningRate = finalLearningRate;
                NewProject.LearningParameters.LearningRateFunction = lrf;
                NewProject.LearningParameters.Momentum = momentum;
                if (int.TryParse(textTrainingCycles.Text, out tmpInt)) NewProject.LearningParameters.MaxTrainingCycles = tmpInt;

                NnProject.Save(NewProject, textSaveFolder.Text);

                this.Close();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error creating network - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void buttonChooseSaveFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            fbd.Description = "Select project save folder";
            fbd.ShowDialog();
            if (fbd.SelectedPath.Length > 0)
            {
                textSaveFolder.Text = fbd.SelectedPath;
            }
        }
        private void buttonChooseTrainingSet_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.CheckPathExists = true;
            fd.CheckFileExists = true;
            fd.DefaultExt = ".csv";
            fd.ShowDialog();
            if (fd.FileName.Length > 0)
                textTrainingSet.Text = fd.FileName;
        }
        private void buttonChooseCrossValidationSet_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.CheckPathExists = true;
            fd.CheckFileExists = true;
            fd.DefaultExt = ".csv";
            fd.ShowDialog();
            if (fd.FileName.Length > 0)
                textCvSet.Text = fd.FileName;
        }

    }
}
