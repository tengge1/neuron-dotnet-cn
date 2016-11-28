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

using System.Runtime.Serialization;

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 学习速率函数接口。 该接口定义了随着训练进展，学习速率从其初始值改变到其最终值的方式。
    /// </summary>
    public interface ILearningRateFunction : ISerializable
    {
        /// <summary>
        /// 获取学习率的初始值
        /// </summary>
        /// <value>
        /// 初始学习率
        /// </value>
        double InitialLearningRate { get; }

        /// <summary>
        /// 获得学习率的最终值
        /// </summary>
        /// <value>
        /// 最终学习率
        /// </value>
        double FinalLearningRate { get; }

        /// <summary>
        /// 获得当前训练迭代的有效学习率。 不对参数执行验证。
        /// </summary>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 总训练迭代
        /// </param>
        /// <returns>
        /// 当前训练迭代的有效学习率
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// 如果训练次数为零或负数
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 如果当前迭代为负，或者大于训练总次数
        /// </exception>
        double GetLearningRate(int currentIteration, int trainingEpochs);
    }
}