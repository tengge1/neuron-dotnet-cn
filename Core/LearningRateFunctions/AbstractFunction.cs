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

namespace NeuronDotNet.Core.LearningRateFunctions
{
    /// <summary>
    /// 学习速率函数的抽象基类。
    /// </summary>
    [Serializable]
    public abstract class AbstractFunction : ILearningRateFunction
    {
        /// <summary>
        /// 初始学习率
        /// </summary>
        protected readonly double initialLearningRate;

        /// <summary>
        /// 最终学习率
        /// </summary>
        protected readonly double finalLearningRate;

        /// <summary>
        /// 获取学习率的初始值
        /// </summary>
        /// <value>
        /// Initial Learning Rate
        /// </value>
        public double InitialLearningRate
        {
            get { return initialLearningRate; }
        }

        /// <summary>
        /// 获得学习率的最终值
        /// </summary>
        /// <value>
        /// Final Learning Rate
        /// </value>
        public double FinalLearningRate
        {
            get { return finalLearningRate; }
        }

        /// <summary>
        /// 构造具有指定的学习率的初始值和最终值的新实例。
        /// </summary>
        /// <param name="initialLearningRate">
        /// 初始值学习率
        /// </param>
        /// <param name="finalLearningRate">
        /// 最终值学习率
        /// </param>
        public AbstractFunction(double initialLearningRate, double finalLearningRate)
        {
            this.initialLearningRate = initialLearningRate;
            this.finalLearningRate = finalLearningRate;
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
        public AbstractFunction(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");

            this.initialLearningRate = info.GetDouble("initialLearningRate");
            this.finalLearningRate = info.GetDouble("finalLearningRate");
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
        /// if <c>info</c> is <c>null</c>
        /// </exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");

            info.AddValue("initialLearningRate", initialLearningRate);
            info.AddValue("finalLearningRate", finalLearningRate);
        }

        /// <summary>
        /// 获得当前训练迭代的有效学习率。
        /// </summary>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期的总数
        /// </param>
        /// <returns>
        /// 当前训练迭代的有效学习率
        /// </returns>
        /// <exception cref="ArgumentException">
        /// 如果<c> trainingEpochs </ c>为零或负值
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 如果<c> currentIteration </ c>为负，或者如果它不小于<c> trainingEpochs </ c>
        /// </exception>
        public abstract double GetLearningRate(int currentIteration, int trainingEpochs);
    }
}
