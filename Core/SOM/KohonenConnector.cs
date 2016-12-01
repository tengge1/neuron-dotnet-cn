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
using NeuronDotNet.Core.Initializers;

namespace NeuronDotNet.Core.SOM
{
    /// <summary>
    /// Kohonen连接器是由连接任何层到Kohonen层的Kohonen突触的集合组成的<see cref =“IConnector”/>。
    /// </summary>
    [Serializable]
    public class KohonenConnector : Connector<ILayer, KohonenLayer, KohonenSynapse>
    {
        /// <summary>
        /// 在给定图层之间创建一个新的Kohonen连接器。
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
        public KohonenConnector(ILayer sourceLayer, KohonenLayer targetLayer)
            : base(sourceLayer, targetLayer, ConnectionMode.Complete)
        {
            this.initializer = new RandomFunction();

            int i = 0;
            foreach (PositionNeuron targetNeuron in targetLayer.Neurons)
            {
                foreach (INeuron sourceNeuron in sourceLayer.Neurons)
                {
                    synapses[i++] = new KohonenSynapse(sourceNeuron, targetNeuron, this);
                }
            }
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
        public KohonenConnector(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            double[] weights = (double[])info.GetValue("weights", typeof(double[]));

            int i = 0;
            foreach (INeuron sourceNeuron in sourceLayer.Neurons)
            {
                foreach (PositionNeuron targetNeuron in targetLayer.Neurons)
                {
                    synapses[i] = new KohonenSynapse(sourceNeuron, targetNeuron, this);
                    synapses[i].Weight = weights[i];
                    i++;
                }
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
    }
}
