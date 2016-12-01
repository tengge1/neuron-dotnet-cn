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
    /// 一个<see cref =“IInitializer”/>使用随机函数
    /// </summary>
    [Serializable]
    public class RandomFunction : IInitializer
    {
        private readonly double minLimit;
        private readonly double maxLimit;

        /// <summary>
        /// 获取最小随机限制
        /// </summary>
        /// <value>
        /// 随机初始值的最小限制
        /// </value>
        public double MinLimit
        {
            get { return minLimit; }
        }

        /// <summary>
        /// 获取最大随机限制
        /// </summary>
        /// <value>
        /// 随机初始值的最大限制
        /// </value>
        public double MaxLimit
        {
            get { return maxLimit; }
        }

        /// <summary>
        /// 创建一个使用从0到1的随机值的新随机初始化函数
        /// </summary>
        public RandomFunction()
            : this(0, 1)
        {
        }

        /// <summary>
        /// 使用指定之间的随机值创建新的随机初始化函数
        /// limits.
        /// </summary>
        /// <param name="minLimit">
        /// 最小限制
        /// </param>
        /// <param name="maxLimit">
        /// 最大限制
        /// </param>
        public RandomFunction(double minLimit, double maxLimit)
        {
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
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
        public RandomFunction(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");

            this.minLimit = info.GetDouble("minLimit");
            this.maxLimit = info.GetDouble("maxLimit");
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
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");

            info.AddValue("minLimit", minLimit);
            info.AddValue("maxLimit", maxLimit);
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
            Helper.ValidateNotNull(activationLayer, "layer");
            foreach (ActivationNeuron neuron in activationLayer.Neurons)
            {
                neuron.bias = Helper.GetRandom(minLimit, maxLimit);
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
            foreach (BackpropagationSynapse synapse in connector.Synapses)
            {
                synapse.Weight = Helper.GetRandom(minLimit, maxLimit);
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
            foreach (KohonenSynapse synapse in connector.Synapses)
            {
                synapse.Weight = Helper.GetRandom(minLimit, maxLimit);
            }
        }
    }
}
