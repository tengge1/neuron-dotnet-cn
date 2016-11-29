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
    /// 训练样本事件处理程序。 这由与训练样本相关联的事件使用。
    /// </summary>
    /// <param name="sender">
    /// 发件人调用事件
    /// </param>
    /// <param name="e">
    /// 事件参数
    /// </param>
    public delegate void TrainingSampleEventHandler(object sender, TrainingSampleEventArgs e);

    /// <summary>
    /// 训练样本事件参数。 此类表示与训练样本关联的事件的参数
    /// </summary>
    public class TrainingSampleEventArgs : EventArgs
    {
        private int trainingIteration;
        private TrainingSample trainingSample;

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
        /// 获取与事件关联的训练样本
        /// </summary>
        /// <value>
        /// 与事件相关的训练样本
        /// </value>
        public TrainingSample TrainingSample
        {
            get { return trainingSample; }
        }

        /// <summary>
        /// 创建此类的新实例
        /// </summary>
        /// <param name="trainingIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingSample">
        /// 与事件相关联的训练样本
        /// </param>
        public TrainingSampleEventArgs(int trainingIteration, TrainingSample trainingSample)
        {
            this.trainingIteration = trainingIteration;
            this.trainingSample = trainingSample;
        }
    }
}
