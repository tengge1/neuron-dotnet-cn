using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeuronDotNet.Core;
using NeuronDotNet.Core.SOM;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // 初始化
            var inputLayer = new KohonenLayer(20);
            var outputLayer = new KohonenLayer(20);
            var network = new KohonenNetwork(inputLayer, outputLayer);
            network.Initialize();

            // 训练
            var inputVector = new double[] { 1, 2, 3, 4, 5 };
            var outputVector = new double[] { 3, 6, 9, 12, 15 };
            var trainingSample = new TrainingSample(inputVector, outputVector);
            network.Learn(trainingSample, 0, 100);

            // 预测
            var testInput = new double[] { 1, 1.5, 3, 6 };
            var testOutput = network.Run(testInput);
            foreach (var i in testOutput)
            {
                Console.Write(i + ",");
            }
            Console.WriteLine();
        }
    }
}
