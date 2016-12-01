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

namespace NeuronDotNet.Core.SOM
{
    /// <summary>
    /// 此接口表示邻域函数。 邻域函数确定相对于获胜者神经元的每个神经元的邻域。 该功能取决于层的形状以及训练进度。
    /// </summary>
    public interface INeighborhoodFunction : ISerializable
    {
        /// <summary>
        /// 确定给定的Kohonen层中的每个神经元的邻域相对于获胜者神经元。
        /// </summary>
        /// <param name="layer">
        /// 含有神经元的Kohonen层
        /// </param>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时代
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// 如果<c> trainingEpochs </ c>为零或负值
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 如果<c> currentIteration </ c>为负，或者如果它不小于<c> trainingEpochs </ c>
        /// </exception>
        void EvaluateNeighborhood(KohonenLayer layer, int currentIteration, int trainingEpochs);
    }
}
