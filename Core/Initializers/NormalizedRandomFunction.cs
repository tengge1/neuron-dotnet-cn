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
    /// 使用规范随机函数的<见cref =“IInitializer”/>。
    /// </summary>
    public class NormalizedRandomFunction : IInitializer
    {
        /// <summary>
        /// 创建一个新的标准化随机函数
        /// </summary>
        public NormalizedRandomFunction()
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
        public NormalizedRandomFunction(SerializationInfo info, StreamingContext context)
        {
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
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
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

            int i = 0;
            double[] normalized = Helper.GetRandomVector(activationLayer.NeuronCount, 1d);
            foreach (ActivationNeuron neuron in activationLayer.Neurons)
            {
                neuron.bias = normalized[i++];
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

            int i = 0;
            double[] normalized = Helper.GetRandomVector(connector.SynapseCount, 1d);
            foreach (BackpropagationSynapse synapse in connector.Synapses)
            {
                synapse.Weight = normalized[i++];
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

            int i = 0;
            double[] normalized = Helper.GetRandomVector(connector.SynapseCount, 1d);
            foreach (KohonenSynapse synapse in connector.Synapses)
            {
                synapse.Weight = normalized[i++];
            }
        }
    }
}
