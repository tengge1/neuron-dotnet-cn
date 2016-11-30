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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NeuronDotNet.Core.Backpropagation
{
    /// <summary>
    /// 反向传播连接器是由连接两个激活层的反向传播突触的集合组成的<见cref =“IConnector”/>。
    /// </summary>
    [Serializable]
    public class BackpropagationConnector
        : Connector<ActivationLayer, ActivationLayer, BackpropagationSynapse>
    {
        internal double momentum = 0.07d;

        /// <summary>
        /// 获取或设置动量（突触保留其先前变化量的趋势）
        /// </summary>
        /// <value>
        /// 突触保持其先前体重变化的趋势。
        /// </value>
        public double Momentum
        {
            get { return momentum; }
            set { momentum = value; }
        }

        /// <summary>
        /// 在给定图层之间创建一个新的完整反向传播连接器。
        /// </summary>
        /// <param name="sourceLayer">
        /// 源层
        /// </param>
        /// <param name="targetLayer">
        /// 目标层
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> sourceLayer </ c>或<c> targetLayer </ c>为<c> null </ c>
        /// </exception>
        public BackpropagationConnector(ActivationLayer sourceLayer, ActivationLayer targetLayer)
            : this(sourceLayer, targetLayer, ConnectionMode.Complete)
        {
        }

        /// <summary>
        /// 使用指定的连接模式在给定图层之间创建新的反向传播连接器。
        /// </summary>
        /// <param name="sourceLayer">
        /// 源层
        /// </param>
        /// <param name="targetLayer">
        /// 目标层
        /// </param>
        /// <param name="connectionMode">
        /// 要使用的连接模式
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> sourceLayer </ c>或<c> targetLayer </ c>为<c> null </ c>
        /// </exception>
        public BackpropagationConnector(ActivationLayer sourceLayer, ActivationLayer targetLayer, ConnectionMode connectionMode)
            : base(sourceLayer, targetLayer, connectionMode)
        {
            ConstructSynapses();
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
        public BackpropagationConnector(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ConstructSynapses();

            this.momentum = info.GetDouble("momentum");
            double[] weights = (double[])info.GetValue("weights", typeof(double[]));

            for (int i = 0; i < synapses.Length; i++)
            {
                synapses[i].Weight = weights[i];
            }
        }

        /// <summary>
        /// 使用序列化连接器所需的数据填充序列化信息
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
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("momentum", momentum);

            double[] weights = new double[synapses.Length];
            for (int i = 0; i < synapses.Length; i++)
            {
                weights[i] = synapses[i].Weight;
            }

            info.AddValue("weights", weights, typeof(double[]));
        }

        /// <summary>
        /// 初始化连接器中的所有突触，并使其准备好进行新训练。 （使用初始化器调整突触的权重）
        /// </summary>
        public override void Initialize()
        {
            if (initializer != null)
            {
                initializer.Initialize(this);
            }
        }

        /// <summary>
        /// 私有帮助函数构建层之间的突触
        /// </summary>
        private void ConstructSynapses()
        {
            int i = 0;
            if (connectionMode == ConnectionMode.Complete)
            {
                foreach (ActivationNeuron targetNeuron in targetLayer.Neurons)
                {
                    foreach (ActivationNeuron sourceNeuron in sourceLayer.Neurons)
                    {
                        synapses[i++] = new BackpropagationSynapse(sourceNeuron, targetNeuron, this);
                    }
                }
            }
            else
            {
                IEnumerator<ActivationNeuron> sourceEnumerator = sourceLayer.Neurons.GetEnumerator();
                IEnumerator<ActivationNeuron> targetEnumerator = targetLayer.Neurons.GetEnumerator();
                while (sourceEnumerator.MoveNext() && targetEnumerator.MoveNext())
                {
                    synapses[i++] = new BackpropagationSynapse(
                        sourceEnumerator.Current, targetEnumerator.Current, this);
                }
            }
        }
    }
}