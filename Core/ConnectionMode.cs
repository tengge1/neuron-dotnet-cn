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
    /// 图层之间的连接模式。
    /// </summary>
    public enum ConnectionMode
    {
        /// <summary>
        /// 完全连接模式，其中源层的所有神经元连接到目标层的所有神经元。
        /// </summary>
        Complete = 0,

        /// <summary>
        /// 单一连接模式，其中源层中的每个神经元连接到目标层中的单个不同的神经元。源层和目标层应具有相同数量的神经元。
        /// </summary>
        OneOne = 1
    }
}
