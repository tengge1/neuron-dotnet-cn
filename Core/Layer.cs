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
using NeuronDotNet.Core.LearningRateFunctions;

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 层是类似神经元的抽象容器。 一层内没有两个神经元可以相互连接。
    /// </summary>
    /// <typeparam name="TNeuron">图层中的神经元类型</typeparam>
    [Serializable]
    public abstract class Layer<TNeuron> : ILayer where TNeuron : INeuron
    {
        /// <summary>
        /// 图层中的神经元数组。 这从不为空。
        /// </summary>
        protected readonly TNeuron[] neurons;

        /// <summary>
        /// 源连接器列表。 此只读值从不为空。
        /// </summary>
        protected readonly List<IConnector> sourceConnectors = new List<IConnector>();

        /// <summary>
        /// 目标连接器列表。 此只读值从不为空。
        /// </summary>
        protected readonly List<IConnector> targetConnectors = new List<IConnector>();

        /// <summary>
        /// 学习速率函数
        /// </summary>
        protected ILearningRateFunction learningRateFunction;

        /// <summary>
        /// 初始化用于初始化图层。 如果这个值为null，神经元将有可初始化参数的默认值（通常为零）
        /// </summary>
        protected IInitializer initializer = null;

        /// <summary>
        /// 获得神经元计数
        /// </summary>
        /// <value>
        /// 图层中的神经元数量。 它始终是正的。
        /// </value>
        public int NeuronCount
        {
            get { return neurons.Length; }
        }

        /// <summary>
        /// 显示枚举器以迭代层中的所有神经元
        /// </summary>
        /// <value>
        /// 神经元枚举。 没有枚举的神经元可以为null。
        /// </value>
        public IEnumerable<TNeuron> Neurons
        {
            get
            {
                for (int i = 0; i < neurons.Length; i++)
                {
                    yield return neurons[i];
                }
            }
        }

        /// <summary>
        /// 神经元索引
        /// </summary>
        /// <param name="index">
        /// 索引
        /// </param>
        /// <returns>
        /// 指定位置的索引
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// 如果索引超出范围
        /// </exception>
        public TNeuron this[int index]
        {
            get { return neurons[index]; }
        }

        IEnumerable<INeuron> ILayer.Neurons
        {
            get
            {
                for (int i = 0; i < neurons.Length; i++)
                {
                    yield return neurons[i];
                }
            }
        }

        INeuron ILayer.this[int index]
        {
            get { return neurons[index]; }
        }

        /// <summary>
        /// 获取源连接器列表
        /// </summary>
        /// <value>
        /// 与此图层关联的源连接器列表。 它既不为null，也不包含空元素。
        /// </value>
        public IList<IConnector> SourceConnectors
        {
            get { return sourceConnectors; }
        }

        /// <summary>
        /// 获取目标连接器的列表
        /// </summary>
        /// <value>
        /// 与此图层关联的目标连接器的列表。 它既不为null，也不包含空元素。
        /// </value>
        public IList<IConnector> TargetConnectors
        {
            get { return targetConnectors; }
        }

        /// <summary>
        /// 获取或设置用于初始化图层的初始化程序
        /// </summary>
        /// <value>
        /// 初始化用于初始化图层。 如果此值为null，则不执行初始化。
        /// </value>
        public IInitializer Initializer
        {
            get { return initializer; }
            set { initializer = value; }
        }

        /// <summary>
        /// 获取学习率的初始值
        /// </summary>
        /// <value>
        /// 学习率的初始值。
        /// </value>
        public double LearningRate
        {
            get { return learningRateFunction.InitialLearningRate; }
        }

        /// <summary>
        /// 获取学习速率函数
        /// </summary>
        /// <value>
        /// 学习率训练时使用的函数。 它永远不会为空
        /// </value>
        public ILearningRateFunction LearningRateFunction
        {
            get { return learningRateFunction; }
        }

        /// <summary>
        /// 创建新图层
        /// </summary>
        /// <param name="neuronCount">
        /// 图层中的神经元数量
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果神经元计数不为正
        /// </exception>
        protected Layer(int neuronCount)
        {
            Helper.ValidatePositive(neuronCount, "neuronCount");

            this.neurons = new TNeuron[neuronCount];
            this.learningRateFunction = new LinearFunction(0.3d, 0.05d);
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
        /// 如果info为null
        /// </exception>
        protected Layer(SerializationInfo info, StreamingContext context)
        {
            // 验证
            Helper.ValidateNotNull(info, "info");

            // 反序列化
            int neuronCount = info.GetInt32("neuronCount");
            this.neurons = new TNeuron[neuronCount];

            this.initializer = info.GetValue("initializer", typeof(IInitializer)) as IInitializer;
            this.learningRateFunction = info.GetValue("learningRateFunction", typeof(ILearningRateFunction)) as ILearningRateFunction;
        }

        /// <summary>
        /// 使用序列化图层所需的数据填充序列化信息
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
            // 验证
            Helper.ValidateNotNull(info, "info");

            // 填充
            info.AddValue("neuronCount", neurons.Length);
            info.AddValue("initializer", initializer, typeof(IInitializer));
            info.AddValue("learningRateFunction", learningRateFunction, typeof(ILearningRateFunction));
        }

        /// <summary>
        /// 将神经元输入设置为给定数组指定的值
        /// </summary>
        /// <param name="input">
        /// 输入数组
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果输入为null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果输入数组的长度不同于神经元数量
        /// </exception>
        public virtual void SetInput(double[] input)
        {
            // 验证
            Helper.ValidateNotNull(input, "input");

            if (neurons.Length != input.Length)
            {
                throw new ArgumentException("Length of input array should be same as neuron count", "input");
            }

            // 绑定输入
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].Input = input[i];
            }
        }

        /// <summary>
        /// 获取神经元输出作为数组
        /// </summary>
        /// <returns>
        /// 表示神经元输出的双值数组
        /// </returns>
        public virtual double[] GetOutput()
        {
            double[] output = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
            {
                output[i] = neurons[i].Output;
            }
            return output;
        }

        /// <summary>
        /// 将学习速率设置为给定值。 该层将在整个学习过程中使用该常数值作为学习速率
        /// </summary>
        /// <param name="learningRate">
        /// 学习率
        /// </param>
        public void SetLearningRate(double learningRate)
        {
            SetLearningRate(learningRate, learningRate);
        }

        /// <summary>
        /// 设置学习率的初始值和最终值。 在学习过程中，有效学习率从初始值均匀地变化到最终值
        /// </summary>
        /// <param name="initialLearningRate">
        /// 学习率的初始值
        /// </param>
        /// <param name="finalLearningRate">
        /// 学习率的最终值
        /// </param>
        public void SetLearningRate(double initialLearningRate, double finalLearningRate)
        {
            SetLearningRate(new LinearFunction(initialLearningRate, finalLearningRate));
        }

        /// <summary>
        /// 设置学习速率函数。
        /// </summary>
        /// <param name="learningRateFunction">
        /// 学习率函数使用。
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果学习速率函数为null
        /// </exception>
        public void SetLearningRate(ILearningRateFunction learningRateFunction)
        {
            Helper.ValidateNotNull(learningRateFunction, "learningRateFunction");
            this.learningRateFunction = learningRateFunction;
        }

        /// <summary>
        /// 初始化所有神经元，使他们准备好接受新训练。
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// 运行图层中的所有神经元。
        /// </summary>
        public virtual void Run()
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].Run();
            }
        }

        /// <summary>
        /// 允许所有神经元及其源连接器学习。 该方法假定一个学习环境，其中输入，输出和其他参数（如果有的话）是适当的。
        /// </summary>
        /// <param name="currentIteration">
        /// 当前学习迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期的总数
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果训练次数为零或负值
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 如果当前迭代为负，或者如果它大于训练总数
        /// </exception>
        public virtual void Learn(int currentIteration, int trainingEpochs)
        {
            // 验证委托
            double effectiveRate = learningRateFunction.GetLearningRate(currentIteration, trainingEpochs);
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].Learn(effectiveRate);
            }
        }
    }
}