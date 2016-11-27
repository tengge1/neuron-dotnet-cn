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
using NeuronDotNet.Core.Initializers;

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 连接器表示连接网络中两个层的突触的集合。
    /// </summary>
    /// <typeparam name="TSourceLayer">源层类型</typeparam>
    /// <typeparam name="TTargetLayer">目标层类型</typeparam>
    /// <typeparam name="TSynapse">突触类型</typeparam>
    [Serializable]
    public abstract class Connector<TSourceLayer, TTargetLayer, TSynapse> : IConnector
        where TSourceLayer : ILayer
        where TTargetLayer : ILayer
        where TSynapse : ISynapse
    {
        /// <summary>
        /// 源层。它在构造函数中初始化，并且以后不会更改。它永不为null。
        /// </summary>
        protected readonly TSourceLayer sourceLayer;

        /// <summary>
        /// 目标层。它在构造函数中初始化，并且以后不会更改。它永不为null。
        /// </summary>
        protected readonly TTargetLayer targetLayer;

        /// <summary>
        /// 连接器中的突触数组。它永不为null。
        /// </summary>
        protected readonly TSynapse[] synapses;

        /// <summary>
        /// 连接模式（单一连接模式或完全连接模式）。它在构造函数中初始化并且是不可变的。
        /// </summary>
        protected readonly ConnectionMode connectionMode;

        /// <summary>
        /// 初始化连接器
        /// </summary>
        protected IInitializer initializer;

        /// <summary>
        /// 获取源层
        /// </summary>
        /// <value>
        /// 源层。它用不为null。
        /// </value>
        public TSourceLayer SourceLayer
        {
            get { return sourceLayer; }
        }

        /// <summary>
        /// 获取目标层
        /// </summary>
        /// <value>
        /// 目标层。它永不为null。
        /// </value>
        public TTargetLayer TargetLayer
        {
            get { return targetLayer; }
        }

        /// <summary>
        /// 获取连接器中的突触数。
        /// </summary>
        /// <value>
        /// 突触计数。 它始终是正的。
        /// </value>
        public int SynapseCount
        {
            get { return synapses.Length; }
        }

        /// <summary>
        /// 公开一个枚举器来迭代连接器中的所有突触。
        /// </summary>
        /// <value>
        /// 突触枚举。 没有枚举的突触可以为null。
        /// </value>
        public IEnumerable<TSynapse> Synapses
        {
            get
            {
                for (int i = 0; i < synapses.Length; i++)
                {
                    yield return synapses[i];
                }
            }
        }

        ILayer IConnector.SourceLayer
        {
            get { return sourceLayer; }
        }

        ILayer IConnector.TargetLayer
        {
            get { return targetLayer; }
        }

        IEnumerable<ISynapse> IConnector.Synapses
        {
            get
            {
                for (int i = 0; i < synapses.Length; i++)
                {
                    yield return synapses[i];
                }
            }
        }

        /// <summary>
        /// 获取连接模式
        /// </summary>
        /// <value>
        /// 连接模式
        /// </value>
        public ConnectionMode ConnectionMode
        {
            get { return connectionMode; }
        }

        /// <summary>
        /// 获取或设置用于初始化连接器的初始化程序
        /// </summary>
        /// <value>
        /// 初始化用于初始化连接器。 如果此值为null，则不执行初始化。
        /// </value>
        public IInitializer Initializer
        {
            get { return initializer; }
            set { initializer = value; }
        }

        /// <summary>
        /// 使用指定的连接模式在给定图层之间创建新连接器。
        /// </summary>
        /// <param name="sourceLayer">
        /// 源层
        /// </param>
        /// <param name="targetLayer">
        /// 目标层
        /// </param>
        /// <param name="connectionMode">
        /// 使用的连接模式
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果源层或目标层为null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果连接模式无效
        /// </exception>
        protected Connector(TSourceLayer sourceLayer, TTargetLayer targetLayer, ConnectionMode connectionMode)
        {
            // Validate
            Helper.ValidateNotNull(sourceLayer, "sourceLayer");
            Helper.ValidateNotNull(targetLayer, "targetLayer");

            targetLayer.SourceConnectors.Add(this);
            sourceLayer.TargetConnectors.Add(this);

            this.sourceLayer = sourceLayer;
            this.targetLayer = targetLayer;
            this.connectionMode = connectionMode;
            this.initializer = new NguyenWidrowFunction();

            // 因为突触数组是只读的，所以应该在这里初始化
            switch (connectionMode)
            {
                case ConnectionMode.Complete:
                    synapses = new TSynapse[sourceLayer.NeuronCount * targetLayer.NeuronCount];
                    break;
                case ConnectionMode.OneOne:
                    if (sourceLayer.NeuronCount == targetLayer.NeuronCount)
                    {
                        synapses = new TSynapse[sourceLayer.NeuronCount];
                        break;
                    }
                    throw new ArgumentException(
                        "One-One connector cannot be formed between these layers", "connectionMode");
                default:
                    throw new ArgumentException("Invalid Connection Mode", "connectionMode");
            }
        }

        /// <summary>
        /// 反序列化构造函数
        /// </summary>
        /// <param name="info">
        /// 用于反序列化和获取数据的序列化信息
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果info为null
        /// </exception>
        protected Connector(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");

            this.sourceLayer = (TSourceLayer)info.GetValue("sourceLayer", typeof(TSourceLayer));
            this.targetLayer = (TTargetLayer)info.GetValue("targetLayer", typeof(TTargetLayer));
            this.initializer = (IInitializer)info.GetValue("initializer", typeof(IInitializer));

            this.connectionMode = (ConnectionMode)info.GetValue("connectionMode", typeof(ConnectionMode));

            targetLayer.SourceConnectors.Add(this);
            sourceLayer.TargetConnectors.Add(this);

            if (connectionMode == ConnectionMode.Complete)
            {
                synapses = new TSynapse[sourceLayer.NeuronCount * targetLayer.NeuronCount];
            }
            else
            {
                synapses = new TSynapse[sourceLayer.NeuronCount];
            }
        }

        /// <summary>
        /// 使用序列化信息和所需的数据来序列化连接器
        /// </summary>
        /// <param name="info">
        /// 用于填充数据的序列化信息
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果info为null
        /// </exception>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");
            info.AddValue("sourceLayer", sourceLayer, typeof(TSourceLayer));
            info.AddValue("targetLayer", targetLayer, typeof(TTargetLayer));
            info.AddValue("initializer", initializer, typeof(IInitializer));

            info.AddValue("connectionMode", connectionMode, typeof(ConnectionMode));
        }

        /// <summary>
        /// 将微弱的随机噪声添加到突触的权重，使得网络偏离其局部最优位置（深度学习中没有用到局部平衡状态）
        /// </summary>
        /// <param name="jitterNoiseLimit">
        /// 对随机噪声的最大值的绝对值限制
        /// </param>
        public void Jitter(double jitterNoiseLimit)
        {
            for (int i = 0; i < synapses.Length; i++)
            {
                synapses[i].Jitter(jitterNoiseLimit);
            }
        }

        /// <summary>
        /// 获取属于此连接器的神经元的源突触集合的枚举器
        /// </summary>
        /// <param name="neuron">
        /// 神经元
        /// </param>
        /// <returns>
        /// 属于此连接器的神经元的源突触的集合的枚举器
        /// </returns>
        public IEnumerable<TSynapse> GetSourceSynapses(INeuron neuron)
        {
            foreach (TSynapse synapse in neuron.SourceSynapses)
            {
                if (synapse.Parent == this)
                {
                    yield return synapse;
                }
            }
        }

        /// <summary>
        /// 获取属于此连接器的神经元的目标突触的集合的枚举器
        /// </summary>
        /// <param name="neuron">
        /// 神经元
        /// </param>
        /// <returns>
        /// 属于此连接器的神经元的目标突触的集合的枚举器
        /// </returns>
        public IEnumerable<TSynapse> GetTargetSynapses(INeuron neuron)
        {
            foreach (TSynapse synapse in neuron.TargetSynapses)
            {
                if (synapse.Parent == this)
                {
                    yield return synapse;
                }
            }
        }

        /// <summary>
        /// 初始化连接器中的所有突触，并使其准备好进行新训练。 （使用初始化器调整突触的权重）
        /// </summary>
        public abstract void Initialize();
    }
}