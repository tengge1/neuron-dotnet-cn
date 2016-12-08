using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.LearningRateFunctions;
using NeuronDotNet.Core.SOM;

namespace ScatterFitting
{
    /// <summary>
    /// 窗体
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 测试数据
        /// </summary>
        double[,] data = new double[,]
        {
            { 25.56,    0,      1.32 },
            { 25.56,    20.69,  1.33 },
            { 25.56,    41.37,  1.35 },
            { 25.56,    62.06,  1.36 },
            { 25.56,    82.74,  1.37 },
            { 25.56,   103.43,  1.38 },
            { 93.33,    0,      1.26 },
            { 93.33,   20.69,   1.28 },
            { 93.33,   41.37,   1.29 },
            { 93.33,   62.06,   1.3 },
            { 93.33,   82.74,   1.32 },
            { 93.33,   103.43,  1.33 },
            { 176.67,  20.69,   1.21 },
            { 176.67,  41.37,   1.23 },
            { 176.67,  62.06,   1.25 },
            { 176.67,  82.74,   1.26 },
            { 176.67,  103.43,  1.28 }
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < 17; i++)
            {
                var row = new DataGridViewRow();
                for (var j = 0; j < 3; j++)
                {
                    var cell = new DataGridViewTextBoxCell();
                    cell.Value = data[i, j];
                    row.Cells.Add(cell);
                }
                dgvData.Rows.Add(row);
            }
        }

        /// <summary>
        /// 点击计算按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCalculate_Click(object sender, EventArgs e)
        {
            // 创建输入层、隐层和输出层
            var inputLayer = new LinearLayer(2);
            var hiddenLayer = new LinearLayer(50);
            var outputLayer = new LinearLayer(1);

            // 创建层之间的关联
            new BackpropagationConnector(inputLayer, hiddenLayer, ConnectionMode.Complete);
            new BackpropagationConnector(hiddenLayer, outputLayer, ConnectionMode.Complete);

            // 创建神经网络
            var network = new BackpropagationNetwork(inputLayer, outputLayer);
            network.Initialize();

            // 进行训练
            for (var i = 0; i < 17; i++)
            {
                var x1 = data[i, 0];
                var x2 = data[i, 1];
                var y = data[i, 2];

                var inputVector = new double[] { x1, x2 };
                var outputVector = new double[] { y };
                var trainingSample = new TrainingSample(inputVector, outputVector);
                network.Learn(trainingSample, i, 17);
            }

            // 进行预测
            for (var i = 0; i < 17; i++)
            {
                var x1 = data[i, 0];
                var x2 = data[i, 1];
                var y = data[i, 2];

                var testInput = new double[] { x1, x2 };
                var testOutput = network.Run(testInput);

                dgvData.Rows[i].Cells[3].Value = testOutput[0];
            }
        }
    }
}
