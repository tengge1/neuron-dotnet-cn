using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.LearningRateFunctions;
using NeuronDotNet.Core.SOM;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // 创建输入层、隐层和输出层
            var inputLayer = new LinearLayer(1);
            var hiddenLayer = new LinearLayer(5);
            var outputLayer = new LinearLayer(1);

            // 创建层之间的关联
            new BackpropagationConnector(inputLayer, hiddenLayer, ConnectionMode.Complete);
            new BackpropagationConnector(hiddenLayer, outputLayer, ConnectionMode.Complete);

            // 创建神经网络
            var network = new BackpropagationNetwork(inputLayer, outputLayer);
            //network.SetLearningRate(new LinearFunction(0.1, 0.6));
            network.Initialize();

            // 训练
            var ran = new Random();
            for (var i = 0; i < 100; i++)
            {
                var inputVector = new double[] { i };
                var outputVector = new double[] { Math.PI * i };
                var trainingSample = new TrainingSample(inputVector, outputVector);
                network.Learn(trainingSample, i, 100);
            }

            // 预测
            var testInput = new double[] { 1 };
            var testOutput = network.Run(testInput);
            Console.WriteLine(testOutput[0]);
        }
    }
}
