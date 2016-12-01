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
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.SOM;

namespace NeuronDotNet.Core.Initializers
{
    /// <summary>
    /// 使用Nguyen Widrow函数的<see cref =“IInitializer”/>。
    /// </summary>
    [Serializable]
    public class NguyenWidrowFunction : IInitializer
    {
        private readonly double outputRange;

        /// <summary>
        /// 获取输出范围
        /// </summary>
        /// <value>
        /// 网络输出所需的值范围
        /// </value>
        public double OutputRange
        {
            get { return outputRange; }
        }

        /// <summary>
        /// 创建新的NGuyen Widrow初始化函数
        /// </summary>
        public NguyenWidrowFunction()
            : this(1d)
        {
        }

        /// <summary>
        /// 使用给定的输出范围创建新的NGuyen Widrow功能
        /// </summary>
        /// <param name="outputRange">
        /// 神经元的输出可以采用的值的范围（即，最大减去最小值）
        /// </param>
        public NguyenWidrowFunction(double outputRange)
        {
            this.outputRange = outputRange;
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
        public NguyenWidrowFunction(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");
            this.outputRange = info.GetDouble("outputRange");
        }

        /// <summary>
        /// 用序列化初始化程序所需的数据填充序列化信息
        /// </summary>
        /// <param name="info">
        /// 用于填充数据的序列化信息
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");
            info.AddValue("outputRange", outputRange);
        }

        /// <summary>
        /// 初始化激活层中激活神经元的偏置值。
        /// </summary>
        /// <param name="activationLayer">
        /// 激活层初始化
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> activationLayer </ c>为<c> null </ c>
        /// </exception>
        public void Initialize(ActivationLayer activationLayer)
        {
            Helper.ValidateNotNull(activationLayer, "activationLayer");

            int hiddenNeuronCount = 0;
            foreach (IConnector targetConnector in activationLayer.TargetConnectors)
            {
                hiddenNeuronCount += targetConnector.TargetLayer.NeuronCount;
            }

            double nGuyenWidrowFactor = NGuyenWidrowFactor(activationLayer.NeuronCount, hiddenNeuronCount);

            foreach (ActivationNeuron neuron in activationLayer.Neurons)
            {
                neuron.bias = Helper.GetRandom(-nGuyenWidrowFactor, nGuyenWidrowFactor);
            }
        }

        /// <summary>
        /// 初始化反向传播连接器中的所有反向传播突触的权重。
        /// </summary>
        /// <param name="connector">
        /// 反向传播连接器初始化。
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c>连接器</ c>为<c> null </ c>
        /// </exception>
        public void Initialize(BackpropagationConnector connector)
        {
            Helper.ValidateNotNull(connector, "connector");

            double nGuyenWidrowFactor = NGuyenWidrowFactor(
                connector.SourceLayer.NeuronCount, connector.TargetLayer.NeuronCount);

            int synapsesPerNeuron = connector.SynapseCount / connector.TargetLayer.NeuronCount;

            foreach (INeuron neuron in connector.TargetLayer.Neurons)
            {
                int i = 0;
                double[] normalizedVector = Helper.GetRandomVector(synapsesPerNeuron, nGuyenWidrowFactor);
                foreach (BackpropagationSynapse synapse in connector.GetSourceSynapses(neuron))
                {
                    synapse.Weight = normalizedVector[i++];
                }
            }
        }

        /// <summary>
        /// 初始化Kohonen连接器中所有空间突触的权重。
        /// </summary>
        /// <param name="connector">
        /// Kohonen连接器初始化。
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c>连接器</ c>为<c> null </ c>
        /// </exception>
        public void Initialize(KohonenConnector connector)
        {
            Helper.ValidateNotNull(connector, "connector");
            double nGuyenWidrowFactor = NGuyenWidrowFactor(
                connector.SourceLayer.NeuronCount, connector.TargetLayer.NeuronCount);

            int synapsesPerNeuron = connector.SynapseCount / connector.TargetLayer.NeuronCount;

            foreach (INeuron neuron in connector.TargetLayer.Neurons)
            {
                int i = 0;
                double[] normalizedVector = Helper.GetRandomVector(synapsesPerNeuron, nGuyenWidrowFactor);
                foreach (KohonenSynapse synapse in connector.GetSourceSynapses(neuron))
                {
                    synapse.Weight = normalizedVector[i++];
                }
            }
        }

        /// <summary>
        /// 私有帮助方法来计算Nguyen-Widrow因子
        /// </summary>
        /// <param name="inputNeuronCount">
        /// 输入神经元数
        /// </param>
        /// <param name="hiddenNeuronCount">
        /// 隐藏神经元的数量
        /// </param>
        /// <returns>
        /// Nguyen-Widrow因子
        /// </returns>
        private double NGuyenWidrowFactor(int inputNeuronCount, int hiddenNeuronCount)
        {
            return 0.7d * Math.Pow(hiddenNeuronCount, (1d / inputNeuronCount)) / outputRange;
        }
    }
}