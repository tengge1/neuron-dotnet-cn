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
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.SOM;

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 初始化接口。 初始化器应该为所有具体的可初始化层和连接器定义初始化方法。
    /// </summary>
    public interface IInitializer : ISerializable
    {
        /// <summary>
        /// 初始化激活层中激活神经元的偏置值。
        /// </summary>
        /// <param name="activationLayer">
        /// 活动层初始化
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果活动层为null
        /// </exception>
        void Initialize(ActivationLayer activationLayer);

        /// <summary>
        /// 初始化反向传播连接器中所有反向传播突触的权重。
        /// </summary>
        /// <param name="connector">
        /// 反向传播连接器初始化。
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果connector为null
        /// </exception>
        void Initialize(BackpropagationConnector connector);

        /// <summary>
        /// 初始化Kohonen连接器中所有空间突触的权重。
        /// </summary>
        /// <param name="connector">
        /// Kohonen连接器初始化。
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果connector为null
        /// </exception>
        void Initialize(KohonenConnector connector);
    }
}