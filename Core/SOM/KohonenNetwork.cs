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

namespace NeuronDotNet.Core.SOM
{
    /// <summary>
    /// 这个类扩展了一个<see cref =“Network”/>并代表Kohonen自组织地图。
    /// </summary>
    [Serializable]
    public class KohonenNetwork : Network
    {
        /// <summary>
        /// 获取网络的优胜者神经元
        /// </summary>
        /// <value>
        /// 优胜者神经元
        /// </value>
        public PositionNeuron Winner
        {
            get { return (outputLayer as KohonenLayer).Winner; }
        }

        /// <summary>
        ///创建一个新的Kohonen SOM，具有指定的输入和输出层。 （在使用构造函数之前，需要使用适当的突触来连接所有层）对网络结构进行的任何更改在此后可能导致网络完全故障）
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
        public KohonenNetwork(ILayer inputLayer, KohonenLayer outputLayer)
            : base(inputLayer, outputLayer, TrainingMethod.Unsupervised)
        {
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
        public KohonenNetwork(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 一个受保护的帮助函数，用于训练单个学习样本
        /// </summary>
        /// <param name="trainingSample">
        /// 培训样品使用
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
            inputLayer.SetInput(trainingSample.InputVector);
            foreach (ILayer layer in layers)
            {
                layer.Run();
                layer.Learn(currentIteration, trainingEpochs);
            }
        }
    }
}