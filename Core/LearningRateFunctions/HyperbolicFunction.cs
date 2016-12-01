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
    /// 双曲学习速率函数。 随着训练的进行，学习速率从其初始值双曲线变化到最终值。
    /// </summary>
    [Serializable]
    public sealed class HyperbolicFunction : AbstractFunction
    {
        private readonly double product;

        /// <summary>
        /// 构造具有指定的学习率的初始和最终值的双曲线函数的新实例。
        /// </summary>
        /// <param name="initialLearningRate">
        /// 初始值学习率
        /// </param>
        /// <param name="finalLearningRate">
        /// 最终值学习率
        /// </param>
        public HyperbolicFunction(double initialLearningRate, double finalLearningRate)
            : base(initialLearningRate, finalLearningRate)
        {
            product = initialLearningRate * finalLearningRate;
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
        public HyperbolicFunction(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            product = initialLearningRate * finalLearningRate;
        }

        /// <summary>
        /// 获得当前训练迭代的有效学习率。 （随着训练的进行，学习速率从其初始值双曲线变化到最终值）
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
        public override double GetLearningRate(int currentIteration, int trainingEpochs)
        {
            Helper.ValidatePositive(trainingEpochs, "trainingEpochs");
            Helper.ValidateWithinRange(currentIteration, 0, trainingEpochs - 1, "currentIteration");

            double denominator = finalLearningRate
                + ((initialLearningRate - finalLearningRate) * currentIteration) / trainingEpochs;

            if (denominator == 0)
            {
                return 0d;
            }
            return product / denominator;
        }
    }
}