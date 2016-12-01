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

namespace NeuronDotNet.Core.SOM
{
    /// <summary>
    /// Kohonen图层中位置神经元的晶格拓扑
    /// </summary>
    public enum LatticeTopology
    {
        // 矩形网格中神经元的排列
        //
        //            0 0 0 0 0 0
        //            0 0 0 * 0 0
        //            0 0 * O * 0
        //            0 0 0 * 0 0
        //            0 0 0 0 0 0
        //
        // 'O'的四个直接邻居被示为'*'

        /// <summary>
        /// 每个神经元有四个直接邻居
        /// </summary>
        Rectangular = 0,



        // 六角形网格中的神经元的排列
        //
        //            0 0 0 0 0
        //             0 0 * * 0
        //            0 0 * O * 0
        //             0 0 * * 0 0
        //              0 0 0 0 0
        //
        // 'O'的六个直接邻居被示为'*'

        /// <summary>
        /// 每个神经元具有六个直接邻居
        /// </summary>
        Hexagonal = 1,
    }
}
