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

namespace NeuronDotNet.Core
{
    /// <summary>
    /// <para>
    /// 一个抽象的基类来表示一个神经网络。 典型的神经网络由一组由各种非循环互连组成。 输入层获得用户的输入，网络输出从输出层获得。
    /// </para>
    /// <para>
    /// 要创建神经网络，请按照下列步骤操作
    /// <list type="bullet">
    /// <item>创建和自定义图层</item>
    /// <item>在层之间建立连接（不存在周期）</item>
    /// <item>构造网络指定所需的输入和输出层</item>
    /// </list>
    /// </para>
    /// <para>
    /// 有两种模式可以训练神经网络。 在“批量训练”中，允许神经网络通过指定包含各种训练样本的预定义训练集来学习。 在“在线训练模式”中，每次产生随机训练样本（通常由另一个神经网络，称为“教师”网络）并用于训练。 两种模式都由重载的Learn（）方法支持。 Run（）方法用于针对特定输入运行神经网络。
    /// </para>
    /// </summary>
    [Serializable]
    public abstract class Network : INetwork
    {
        /// <summary>
        /// 输入层
        /// </summary>
        protected readonly ILayer inputLayer;

        /// <summary>
        /// 输出层
        /// </summary>
        protected readonly ILayer outputLayer;

        /// <summary>
        /// 网络中的层列表，非循环地排序（第一层是输入层，最后一个是输出层）
        /// </summary>
        protected readonly IList<ILayer> layers;

        /// <summary>
        /// 层之间的连接器列表。
        /// </summary>
        protected readonly IList<IConnector> connectors;

        /// <summary>
        /// 训练方法用于训练网络
        /// </summary>
        protected readonly TrainingMethod trainingMethod;

        /// <summary>
        /// 在“批处理训练”模式期间在新的训练迭代的开始期间调用该事件。
        /// </summary>
        public event TrainingEpochEventHandler BeginEpochEvent;

        /// <summary>
        /// 当网络即将学习训练样本时，调用此事件。
        /// </summary>
        public event TrainingSampleEventHandler BeginSampleEvent;

        /// <summary>
        /// 每当网络成功完成学习训练样本时，调用此事件。
        /// </summary>
        public event TrainingSampleEventHandler EndSampleEvent;

        /// <summary>
        /// 每当在“批处理训练”模式期间成功完成训练迭代时，将调用此事件。
        /// </summary>
        public event TrainingEpochEventHandler EndEpochEvent;

        /// <summary>
        /// 执行抖动操作的时间（间隔）。 如果该值为零，则不执行抖动。
        /// </summary>
        protected int jitterEpoch;

        /// <summary>
        /// 在抖动操作期间添加的随机噪声的最大绝对限制
        /// </summary>
        protected double jitterNoiseLimit;

        /// <summary>
        /// 当需要立即停止训练时，此标志设置为true
        /// </summary>
        protected bool isStopping = false;

        /// <summary>
        /// 获取网络的输入层
        /// </summary>
        /// <value>
        /// 输入网络层。 此属性永远不为空。
        /// </value>
        public ILayer InputLayer
        {
            get { return inputLayer; }
        }

        /// <summary>
        /// 获取网络的输出层
        /// </summary>
        /// <value>
        /// 输出网络层。 此属性永远不为空。
        /// </value>
        public ILayer OutputLayer
        {
            get { return outputLayer; }
        }

        /// <summary>
        /// 获取网络中的层数。
        /// </summary>
        /// <value>
        /// 层计数。 此值始终为正。
        /// </value>
        public int LayerCount
        {
            // 注意，可以有只有一个层的网络（可能在未来版本中）
            get { return layers.Count; }
        }

        /// <summary>
        /// 显示枚举器以迭代网络中的图层。
        /// </summary>
        /// <value>
        /// 层枚举。 网络中的任何图层都不能为空。
        /// </value>
        public IEnumerable<ILayer> Layers
        {
            get
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    yield return layers[i];
                }
            }
        }

        /// <summary>
        ///层索引器
        /// </summary>
        /// <param name="index">
        /// 索引
        /// </param>
        /// <returns>
        /// 在给定的索引的图层
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        ///如果索引超出范围
        /// </exception>
        public ILayer this[int index]
        {
            get { return layers[index]; }
        }

        /// <summary>
        /// 获取网络中的连接器数量。
        /// </summary>
        /// <value>
        /// 连接器计数。 此值从不为负。
        /// </value>
        public int ConnectorCount
        {
            get { return connectors.Count; }
        }

        /// <summary>
        /// 公开一个枚举器来迭代网络中的连接器。
        /// </summary>
        /// <value>
        /// 连接器枚举器。 网络中没有连接器可以为null。
        /// </value>
        public IEnumerable<IConnector> Connectors
        {
            get
            {
                for (int i = 0; i < connectors.Count; i++)
                {
                    yield return connectors[i];
                }
            }
        }

        /// <summary>
        /// 获取或设置抖动噪声的最大绝对限制
        /// </summary>
        /// <value>
        /// 抖动时添加的随机噪声的最大绝对限制
        /// </value>
        public double JitterNoiseLimit
        {
            get { return jitterNoiseLimit; }
            set { jitterNoiseLimit = value; }
        }

        /// <summary>
        /// 获取或设置抖动纪元
        /// </summary>
        /// <value>
        /// 执行抖动的时期（间隔）。 如果此值不为正，则不执行抖动。
        /// </value>
        public int JitterEpoch
        {
            get { return jitterEpoch; }
            set { jitterEpoch = value; }
        }

        /// <summary>
        /// 创建一个新的神经网络
        /// </summary>
        /// <param name="inputLayer">
        /// 输入层
        /// </param>
        /// <param name="outputLayer">
        /// 输出层
        /// </param>
        /// <param name="trainingMethod">
        /// 使用培训方法
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果输入图层或输出图层为null。
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果训练方法无效
        /// </exception>
        protected Network(ILayer inputLayer, ILayer outputLayer, TrainingMethod trainingMethod)
        {
            // 验证
            Helper.ValidateNotNull(inputLayer, "inputLayer");
            Helper.ValidateNotNull(outputLayer, "outputLayer");
            Helper.ValidateEnum(typeof(TrainingMethod), trainingMethod, "trainingMethod");

            // 将参数分配给相应的变量
            this.inputLayer = inputLayer;
            this.outputLayer = inputLayer;
            this.trainingMethod = trainingMethod;

            // 使用默认值初始化抖动参数
            this.jitterEpoch = 73;
            this.jitterNoiseLimit = 0.03d;

            // 创建层和连接器的列表
            this.layers = new List<ILayer>();
            this.connectors = new List<IConnector>();

            // 通过从输入图层拓扑地访问图层来填充列表
            Stack<ILayer> stack = new Stack<ILayer>();
            stack.Push(inputLayer);

            // Indegree地图
            IDictionary<ILayer, int> inDegree = new Dictionary<ILayer, int>();
            while (stack.Count > 0)
            {
                // 将“堆栈顶部”添加到图层列表
                this.outputLayer = stack.Pop();
                layers.Add(this.outputLayer);

                // 将目标连接器添加到连接器列表，确保它们不会导致循环
                foreach (IConnector connector in this.outputLayer.TargetConnectors)
                {
                    connectors.Add(connector);
                    ILayer targetLayer = connector.TargetLayer;
                    if (layers.Contains(targetLayer))
                    {
                        throw new InvalidOperationException("Cycle Exists in the network structure");
                    }

                    // 实际删除此图层
                    inDegree[targetLayer] =
                        inDegree.ContainsKey(targetLayer)
                        ? inDegree[targetLayer] - 1
                        : targetLayer.SourceConnectors.Count - 1;

                    // 将未访问的目标层推送到栈上，如果它的有效inDree值为零
                    if (inDegree[targetLayer] == 0)
                    {
                        stack.Push(targetLayer);
                    }
                }
            }
            // 最后一层应该与输出层相同
            if (outputLayer != this.outputLayer)
            {
                throw new ArgumentException("The outputLayer is invalid", "outputLayer");
            }
            // 初始化新创建的网络
            Initialize();
        }

        /// <summary>
        /// 反序列化构造函数。 假定提供的序列化信息包含有效数据。
        /// </summary>
        /// <param name="info">
        /// 序列化信息以反序列化和获取值
        /// </param>
        /// <param name="context">
        /// 序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public Network(SerializationInfo info, StreamingContext context)
        {
            // 验证
            Helper.ValidateNotNull(info, "info");

            this.inputLayer = info.GetValue("inputLayer", typeof(ILayer)) as ILayer;
            this.outputLayer = info.GetValue("outputLayer", typeof(ILayer)) as ILayer;
            this.layers = info.GetValue("layers", typeof(IList<ILayer>)) as IList<ILayer>;
            this.connectors = info.GetValue("connectors", typeof(IList<IConnector>)) as IList<IConnector>;
            this.trainingMethod = (TrainingMethod)info.GetValue("trainingMethod", typeof(TrainingMethod));
            this.jitterEpoch = info.GetInt32("jitterEpoch");
            this.jitterNoiseLimit = info.GetDouble("jitterNoiseLimit");
        }

        /// <summary>
        /// 使用序列化网络所需的数据填充序列化信息。
        /// </summary>
        /// <param name="info">
        /// 填充序列化数据的信息
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // 验证
            Helper.ValidateNotNull(info, "info");

            // 填充数据
            info.AddValue("inputLayer", inputLayer, typeof(ILayer));
            info.AddValue("outputLayer", outputLayer, typeof(ILayer));
            info.AddValue("layers", layers, typeof(IList<ILayer>));
            info.AddValue("connectors", connectors, typeof(IList<IConnector>));
            info.AddValue("trainingMethod", trainingMethod, typeof(TrainingMethod));
            info.AddValue("jitterNoiseLimit", jitterNoiseLimit);
            info.AddValue("jitterEpoch", jitterEpoch);
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
        protected virtual void OnBeginEpoch(int currentIteration, TrainingSet trainingSet)
        {
            if (BeginEpochEvent != null)
            {
                BeginEpochEvent(this, new TrainingEpochEventArgs(currentIteration, trainingSet));
            }
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
        protected virtual void OnEndEpoch(int currentIteration, TrainingSet trainingSet)
        {
            if (EndEpochEvent != null)
            {
                EndEpochEvent(this, new TrainingEpochEventArgs(currentIteration, trainingSet));
            }
        }

        /// <summary>
        /// 调用BeginSampleEvent
        /// </summary>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="currentSample">
        /// 当前样品即将被训练
        /// </param>
        protected virtual void OnBeginSample(int currentIteration, TrainingSample currentSample)
        {
            if (BeginSampleEvent != null)
            {
                BeginSampleEvent(this, new TrainingSampleEventArgs(currentIteration, currentSample));
            }
        }

        /// <summary>
        /// 调用BeginSampleEvent
        /// </summary>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="currentSample">
        /// 当前样品已成功训练
        /// </param>
        protected virtual void OnEndSample(int currentIteration, TrainingSample currentSample)
        {
            if (EndSampleEvent != null)
            {
                EndSampleEvent(this, new TrainingSampleEventArgs(currentIteration, currentSample));
            }
        }

        /// <summary>
        /// 将学习速率设置为给定值。 网络中的所有层将在学习过程中使用该恒定值作为学习速率。...
        /// </summary>
        /// <param name="learningRate">
        /// 学习率
        /// </param>
        public void SetLearningRate(double learningRate)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].SetLearningRate(learningRate);
            }
        }

        /// <summary>
        /// 设置学习率的初始值和最终值。 在学习过程中，所有网络中的层将使用从一致地变化的有效学习速率将初始值转换为最终值。
        /// </summary>
        /// <param name="initialLearningRate">
        /// 学习率的初始值
        /// </param>
        /// <param name="finalLearningRate">
        /// 学习率的最终值
        /// </param>
        public void SetLearningRate(double initialLearningRate, double finalLearningRate)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].SetLearningRate(initialLearningRate, finalLearningRate);
            }
        }

        /// <summary>
        /// 设置学习速率函数。
        /// </summary>
        /// <param name="learningRateFunction">
        /// 学习率函数使用。
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> learningRateFunction </ c>是<c> null </ c>
        /// </exception>
        public void SetLearningRate(ILearningRateFunction learningRateFunction)
        {
            // 验证被委派
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].SetLearningRate(learningRateFunction);
            }
        }

        /// <summary>
        /// 初始化所有层和连接器，使他们准备接受新的培训。
        /// </summary>
        public virtual void Initialize()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Initialize();
                foreach (IConnector connector in layers[i].TargetConnectors)
                {
                    connector.Initialize();
                }
            }
        }

        /// <summary>
        /// 针对给定的输入运行神经网络
        /// </summary>
        /// <param name="input">
        /// 输入到网络
        /// </param>
        /// <returns>
        /// 网络的输出
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 如果<c>输入</ c>为<c> null </ c>
        /// </exception>
        public virtual double[] Run(double[] input)
        {
            // 验证被委托
            inputLayer.SetInput(input);
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Run();
            }
            return outputLayer.GetOutput();
        }

        /// <summary>
        /// 训练给定训练集的神经网络（批处理训练）
        /// </summary>
        /// <param name="trainingSet">
        /// 使用的训练集
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期数。 （所有样本在每个训练时期以一些随机顺序训练）
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> trainingSet </ c>是<c> null </ c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果<c> trainingEpochs </ c>为零或负值
        /// </exception>
        public virtual void Learn(TrainingSet trainingSet, int trainingEpochs)
        {
            // 验证
            Helper.ValidateNotNull(trainingSet, "trainingSet");
            Helper.ValidatePositive(trainingEpochs, "trainingEpochs");
            if ((trainingSet.InputVectorLength != inputLayer.NeuronCount)
                || (trainingMethod == TrainingMethod.Supervised && trainingSet.OutputVectorLength != outputLayer.NeuronCount)
                || (trainingMethod == TrainingMethod.Unsupervised && trainingSet.OutputVectorLength != 0))
            {
                throw new ArgumentException("Invalid training set");
            }

            // 重置isStopping
            isStopping = false;

            // 重新初始化网络
            Initialize();
            for (int currentIteration = 0; currentIteration < trainingEpochs; currentIteration++)
            {
                int[] randomOrder = Helper.GetRandomOrder(trainingSet.TrainingSampleCount);
                // 开始新的训练时期
                OnBeginEpoch(currentIteration, trainingSet);

                // 检查抖动时期
                if (jitterEpoch > 0 && currentIteration % jitterEpoch == 0)
                {
                    for (int i = 0; i < connectors.Count; i++)
                    {
                        connectors[i].Jitter(jitterNoiseLimit);
                    }
                }
                for (int index = 0; index < trainingSet.TrainingSampleCount; index++)
                {
                    TrainingSample randomSample = trainingSet[randomOrder[index]];

                    // 学习随机训练样本
                    OnBeginSample(currentIteration, randomSample);
                    LearnSample(trainingSet[randomOrder[index]], currentIteration, trainingEpochs);
                    OnEndSample(currentIteration, randomSample);

                    // 检查我们是否需要停止
                    if (isStopping) { isStopping = false; return; }
                }

                // 训练时期成功完成
                OnEndEpoch(currentIteration, trainingSet);

                // 检查我们是否需要停止
                if (isStopping) { isStopping = false; return; }
            }
        }

        /// <summary>
        /// 训练给定训练样本的网络（在线训练模式）。 注意这个方法仅训练样本一次，而不管当前的时期是什么。 参数只是用来找出训练进度，并根据它调整参数。
        /// </summary>
        /// <param name="trainingSample">
        /// 培训样品使用
        /// </param>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期数
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> trainingSample </ c>为<c> null </ c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果<c> trainingEpochs </ c>不为正
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 如果<c> currentIteration </ c>为负，或者如果它不小于<c> trainingEpochs </ c>
        /// </exception>
        public virtual void Learn(TrainingSample trainingSample, int currentIteration, int trainingEpochs)
        {
            Helper.ValidateNotNull(trainingSample, "trainingSample");
            Helper.ValidatePositive(trainingEpochs, "trainingEpochs");
            Helper.ValidateWithinRange(currentIteration, 0, trainingEpochs - 1, "currentIteration");

            OnBeginSample(currentIteration, trainingSample);
            LearnSample(trainingSample, currentIteration, trainingEpochs);
            OnEndSample(currentIteration, trainingSample);
        }

        /// <summary>
        /// 如果网络当前正在学习，则此方法停止学习。
        /// </summary>
        public void StopLearning()
        {
            isStopping = true;
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
        protected abstract void LearnSample(TrainingSample trainingSample, int currentIteration, int trainingEpochs);
    }
}