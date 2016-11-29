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

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 训练时期事件处理程序。 此代理处理在训练时期开始或结束时调用的事件
    /// </summary>
    /// <param name="sender">
    /// 发件人调用事件
    /// </param>
    /// <param name="e">
    /// 事件参数
    /// </param>
    public delegate void TrainingEpochEventHandler(object sender, TrainingEpochEventArgs e);

    /// <summary>
    /// 训练时期事件参数
    /// </summary>
    public class TrainingEpochEventArgs : EventArgs
    {
        private int trainingIteration;
        private TrainingSet trainingSet;

        /// <summary>
        /// 获取当前的训练迭代
        /// </summary>
        /// <value>
        /// 当前训练迭代。
        /// </value>
        public int TrainingIteration
        {
            get { return trainingIteration; }
        }

        /// <summary>
        /// 获取与训练集相关联
        /// </summary>
        /// <value>
        /// 与事件相关的训练集
        /// </value>
        public TrainingSet TrainingSet
        {
            get { return trainingSet; }
        }

        /// <summary>
        /// 创建训练时期事件参数的新实例
        /// </summary>
        /// <param name="trainingIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingSet">
        /// 与事件相关联的训练集
        /// </param>
        public TrainingEpochEventArgs(int trainingIteration, TrainingSet trainingSet)
        {
            this.trainingSet = trainingSet;
            this.trainingIteration = trainingIteration;
        }
    }
}
