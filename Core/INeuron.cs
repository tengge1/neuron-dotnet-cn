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

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 神经元接口。 神经元是神经网络的基本单元。
    /// </summary>
    public interface INeuron
    {
        /// <summary>
        /// 获取或设置神经元输入。
        /// </summary>
        /// <value>
        /// 输入到神经元。 对于输入神经元，该值由用户指定，而其他神经元将在源突触传播时更新其输入
        /// </value>
        double Input { get; set; }

        /// <summary>
        /// 获取神经元的输出。
        /// </summary>
        /// <value>
        /// 神经元输出
        /// </value>
        double Output { get; }

        /// <summary>
        /// 获取与这个神经元相关的源突触的列表
        /// </summary>
        /// <value>
        /// 源突触的列表。 它既不能为null，也不能包含空元素。
        /// </value>
        IList<ISynapse> SourceSynapses { get; }

        /// <summary>
        /// 获取与该神经元相关联的目标突触的列表
        /// </summary>
        /// <value>
        /// 目标突触的列表。 它既不能为null，也不能包含空元素。
        /// </value>
        IList<ISynapse> TargetSynapses { get; }

        /// <summary>
        /// 获取包含此神经元的父层
        /// </summary>
        /// <value>
        /// 包含这个神经元的父层。 它永远不会为空
        /// </value>
        ILayer Parent { get; }

        /// <summary>
        /// 运行神经元。 （传播源突触并更新输入和输出值）
        /// </summary>
        void Run();

        /// <summary>
        /// 训练与这个神经元和相关的源突触相关的各种参数。
        /// </summary>
        /// <param name="learningRate">
        /// 当前学习率（这取决于培训进度）
        /// </param>
        void Learn(double learningRate);
    }
}
