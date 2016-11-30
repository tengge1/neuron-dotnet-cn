/***********************************************************************************************
 COPYRIGHT 2008 Vijeth D

 This file is part of NeuronDotNet.
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

using System;
using System.Runtime.Serialization;

namespace NeuronDotNet.Core.Backpropagation
{
    /// <summary>
    /// 这个类扩展了一个<see cref =“Network”/>并且表示一个反向传播神经网络。
    /// </summary>
    [Serializable]
    public class BackpropagationNetwork : Network
    {
        private double meanSquaredError;
        private bool isValidMSE;

        /// <summary>
        /// 获取均方误差的值
        /// </summary>
        /// <value>
        /// 当前训练时期的误差的均方值
        /// </value>
        public double MeanSquaredError
        {
            get { return isValidMSE ? meanSquaredError : 0d; }
        }

        /// <summary>
        /// 创建新的反向传播网络，具有指定的输入和输出层。 （在使用构造函数之前，您需要使用适当的突触来连接所有层。对网络结构创建后所做的任何更改都可能导致完全故障）
        /// </summary>
        /// <param name="inputLayer">
        /// 输入层
        /// </param>
        /// <param name="outputLayer">
        /// 输出层
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> inputLayer </ c>或<c> outputLayer </ c>为<c> null </ c>
        /// </exception>
        public BackpropagationNetwork(ActivationLayer inputLayer, ActivationLayer outputLayer)
            : base(inputLayer, outputLayer, TrainingMethod.Supervised)
        {
            this.meanSquaredError = 0d;
            this.isValidMSE = false;
        }

        /// <summary>
        /// 反序列化构造函数
        /// </summary>
        /// <param name="info">
        /// 序列化信息反序列化和获取数据
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public BackpropagationNetwork(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// <para>
        /// 训练给定训练样本的网络（在线训练模式）。 注意，该方法仅训练样本一次，而不考虑<c> currentIteration </ c>和<c> trainingEpochs </ c>的值。 这些参数仅仅用于调整取决于训练进度的训练参数。
        /// </para>
        /// </summary>
        /// <param name="trainingSample">
        /// 使用的训练样本
        /// </param>
        /// <param name="currentIteration">
        /// 当前训练时期
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期数
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> trainingSample </ c>为<c> null </ c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果<c> trainingEpochs </ c>不为正，或者<c> currentIteration </ c>为负或if<c> currentIteration</ c> 小于<c> trainingEpochs</ c>
        /// </exception>
        public override void Learn(TrainingSample trainingSample, int currentIteration, int trainingEpochs)
        {
            meanSquaredError = 0d;
            isValidMSE = true;
            base.Learn(trainingSample, currentIteration, trainingEpochs);
        }

        /// <summary>
        /// 调用BeginEpochEvent
        /// </summary>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingSet">
        /// 训练集即将被训练
        /// </param>
        protected override void OnBeginEpoch(int currentIteration, TrainingSet trainingSet)
        {
            meanSquaredError = 0d;
            isValidMSE = false;
            base.OnBeginEpoch(currentIteration, trainingSet);
        }

        /// <summary>
        /// 调用EndEpochEvent
        /// </summary>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingSet">
        /// 训练集成功训练了这个时代
        /// </param>
        protected override void OnEndEpoch(int currentIteration, TrainingSet trainingSet)
        {
            meanSquaredError /= trainingSet.TrainingSampleCount;
            isValidMSE = true;
            base.OnEndEpoch(currentIteration, trainingSet);
        }

        /// <summary>
        /// 一个受保护的帮助函数，用于训练单个学习样本
        /// </summary>
        /// <param name="trainingSample">
        /// 使用的训练样本
        /// </param>
        /// <param name="currentIteration">
        /// 当前训练时期（假设为正且小于<c> trainingEpochs </ c>）
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期数（假定为正）
        /// </param>
        protected override void LearnSample(TrainingSample trainingSample, int currentIteration, int trainingEpochs)
        {
            // 这里没有验证
            int layerCount = layers.Count;

            // 设置输入向量
            inputLayer.SetInput(trainingSample.InputVector);

            for (int i = 0; i < layerCount; i++)
            {
                layers[i].Run();
            }

            // 设置错误
            meanSquaredError += (outputLayer as ActivationLayer).SetErrors(trainingSample.OutputVector);

            // 反向传播错误
            for (int i = layerCount; i > 0;)
            {
                ActivationLayer layer = layers[--i] as ActivationLayer;
                if (layer != null)
                {
                    layer.EvaluateErrors();
                }
            }

            // 优化突触权重和神经元偏差值
            for (int i = 0; i < layerCount; i++)
            {
                layers[i].Learn(currentIteration, trainingEpochs);
            }
        }
    }
}