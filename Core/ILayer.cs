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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 这个接口代表神经网络中的一个层。 层是用于类似神经元的容器。 一层内没有两个神经元可以相互连接。
    /// </summary>
    public interface ILayer : ISerializable
    {
        /// <summary>
        /// 获取神经元计数
        /// </summary>
        /// <value>
        /// 图层中的神经元数量。 它总是正数。
        /// </value>
        int NeuronCount { get; }

        /// <summary>
        /// 显示枚举器以迭代层中的所有神经元
        /// </summary>
        /// <value>
        /// 神经元枚举。 没有枚举的神经元可以为null。
        /// </value>
        IEnumerable<INeuron> Neurons { get; }

        /// <summary>
        /// 神经元索引
        /// </summary>
        /// <param name="index">
        /// Index
        /// </param>
        /// <returns>
        /// 位于指定索引的神经元
        /// </returns>
        /// <exception cref="System.IndexOutOfRangeException">
        /// 如果索引超出范围
        /// </exception>
        INeuron this[int index] { get; }

        /// <summary>
        /// 获取源连接器列表
        /// </summary>
        /// <value>
        /// 与此图层关联的源连接器列表。 它既不为null，也不包含空元素。
        /// </value>
        IList<IConnector> SourceConnectors { get; }

        /// <summary>
        /// 获取目标连接器的列表
        /// </summary>
        /// <value>
        /// 与此图层关联的目标连接器的列表。 它既不为null，也不包含空元素。
        /// </value>
        IList<IConnector> TargetConnectors { get; }

        /// <summary>
        /// 获取或设置用于初始化图层的初始化程序
        /// </summary>
        /// <value>
        /// 初始化用于初始化图层。 如果此值为null，则不执行初始化。
        /// </value>
        IInitializer Initializer { get; set; }

        /// <summary>
        /// 获取学习率的初始值
        /// </summary>
        /// <value>
        /// 学习率的初始值。
        /// </value>
        double LearningRate { get; }

        /// <summary>
        /// 获取学习速率函数
        /// </summary>
        /// <value>
        /// 学习率训练时使用的函数。 它永不为null
        /// </value>
        ILearningRateFunction LearningRateFunction { get; }

        /// <summary>
        /// 初始化所有神经元，使他们准备好接受新的训练。
        /// </summary>
        void Initialize();

        /// <summary>
        /// 将学习速率设置为给定值。 该层将在整个学习过程中使用该常数值作为学习速率
        /// </summary>
        /// <param name="learningRate">
        /// The learning rate
        /// </param>
        void SetLearningRate(double learningRate);

        /// <summary>
        /// 设置学习率的初始值和最终值。 在学习过程中，有效学习率从初始值均匀地变化到最终值
        /// </summary>
        /// <param name="initialLearningRate">
        /// Initial value of learning rate
        /// </param>
        /// <param name="finalLearningRate">
        /// Final value of learning rate
        /// </param>
        void SetLearningRate(double initialLearningRate, double finalLearningRate);

        /// <summary>
        /// 设置学习速率函数。
        /// </summary>
        /// <param name="learningRateFunction">
        /// 学习率函数使用。
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果学习速率函数为null
        /// </exception>
        void SetLearningRate(ILearningRateFunction learningRateFunction);

        /// <summary>
        /// 将神经元输入设置为给定数组指定的值
        /// </summary>
        /// <param name="input">
        /// 输入数组
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果输入为null
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// 如果输入数组的长度不同于神经元数量
        /// </exception>
        void SetInput(double[] input);

        /// <summary>
        /// 获取神经元输出作为数组
        /// </summary>
        /// <returns>
        /// 表示神经元输出的双值数组
        /// </returns>
        double[] GetOutput();

        /// <summary>
        /// Runs all neurons in the layer.
        /// </summary>
        void Run();

        /// <summary>
        /// 允许所有神经元及其源连接器学习。 该方法假定一个学习环境，其中输入，输出和其他参数（如果有的话）是适当的。
        /// </summary>
        /// <param name="currentIteration">
        /// 当前学习迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时的总数
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// 如果训练次数为零或负数
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 如果当前迭代是负数，或者如果它大于训练总数
        /// </exception>
        void Learn(int currentIteration, int trainingEpochs);
    }
}