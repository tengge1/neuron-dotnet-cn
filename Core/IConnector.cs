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
    /// 此接口表示连接器。 连接器是连接网络中两个层的突触的集合。
    /// </summary>
    public interface IConnector : ISerializable
    {
        /// <summary>
        /// 获取源图层
        /// </summary>
        /// <value>
        /// 源层。 它永不会为null。
        /// </value>
        ILayer SourceLayer { get; }

        /// <summary>
        /// 获取目标图层
        /// </summary>
        /// <value>
        /// 目标层。 它永不为null。
        /// </value>
        ILayer TargetLayer { get; }

        /// <summary>
        /// 获取连接器中的突触数。
        /// </summary>
        /// <value>
        /// 突触计数。 它始终是正的。
        /// </value>
        int SynapseCount { get; }

        /// <summary>
        /// 公开一个枚举器来迭代连接器中的所有突触。
        /// </summary>
        /// <value>
        /// 突触枚举。 枚举的突触不能为null。
        /// </value>
        IEnumerable<ISynapse> Synapses { get; }

        /// <summary>
        /// 获取连接模式
        /// </summary>
        /// <value>
        /// 连接模式
        /// </value>
        ConnectionMode ConnectionMode { get; }

        /// <summary>
        /// 获取或设置用于初始化连接器的初始化程序
        /// </summary>
        /// <value>
        /// 初始化用于初始化连接器。 如果此值为null，则不执行初始化。
        /// </value>
        IInitializer Initializer { get; set; }

        /// <summary>
        /// 初始化连接器中的所有突触，并使其准备好进行新训练。 （使用初始化器调整突触的权重）
        /// </summary>
        void Initialize();

        /// <summary>
        /// 将微弱的随机噪声添加到突触的权重，使得网络偏离其局部最优位置（深度学习中没有用到局部平衡状态）
        /// </summary>
        /// <param name="jitterNoiseLimit">
        /// 对随机噪声的最大值的绝对值限制
        /// </param>
        void Jitter(double jitterNoiseLimit);
    }
}