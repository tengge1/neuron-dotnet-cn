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

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 此接口表示网络中的突触。 突触负责神经元之间的通信。 典型的神经网络由数百万突触组成。 神经网络的功能取决于这些突触的权重。
    /// </summary>
    public interface ISynapse
    {
        /// <summary>
        /// 获取或设置突触的权重
        /// </summary>
        /// <value>
        /// 突触的权重
        /// </value>
        double Weight { get; set; }

        /// <summary>
        /// 获取父连接器
        /// </summary>
        /// <value>
        /// 包含此突触的父连接器。 它永远不会为空。
        /// </value>
        IConnector Parent { get; }

        /// <summary>
        /// 获取源神经元
        /// </summary>
        /// <value>
        /// 突触的源神经元。 它永远不会为空。
        /// </value>
        INeuron SourceNeuron { get; }

        /// <summary>
        /// 获取目标神经元
        /// </summary>
        /// <value>
        /// 突触的目标神经元。 它永远不会为空。
        /// </value>
        INeuron TargetNeuron { get; }

        /// <summary>
        /// 将信息从源神经元传播到目标神经元
        /// </summary>
        void Propagate();

        /// <summary>
        /// 优化这个突触的权重
        /// </summary>
        /// <param name="learningFactor">
        /// 有效学习因素。 这主要是训练进度和学习的函数率。 它也可以取决于其他因素，如Kohonen网络中的邻域函数。
        /// </param>
        void OptimizeWeight(double learningFactor);

        /// <summary>
        /// 添加小随机噪声到这个突触的权重，以便网络偏离其局部最优位置（进一步学习对它无用的一个平衡状态）
        /// </summary>
        /// <param name="jitterNoiseLimit">
        /// 对随机噪声的最大绝对限制
        /// </param>
        void Jitter(double jitterNoiseLimit);
    }
}