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
using System.Runtime.Serialization;
using NeuronDotNet.Core.Initializers;

namespace NeuronDotNet.Core.Backpropagation
{
    /// <summary>
    /// <see cref =“ActivationLayer”/>使用正弦激活函数
    /// </summary>
    [Serializable]
    public class SineLayer : ActivationLayer
    {
        /// <summary>
        /// 构造一个新的SineLayer包含指定数量的神经元
        /// </summary>
        /// <param name="neuronCount">
        /// 神经元的数量
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果<c> neuronCount </ c>为零或负数
        /// </exception>
        public SineLayer(int neuronCount)
            : base(neuronCount)
        {
            this.initializer = new NguyenWidrowFunction();
        }

        /// <summary>
        /// 正弦激活功能
        /// </summary>
        /// <param name="input">
        /// 电流输入到神经元
        /// </param>
        /// <param name="previousOutput">
        /// 神经元上的先前输出
        /// </param>
        /// <returns>
        /// 激活的值
        /// </returns>
        public override double Activate(double input, double previousOutput)
        {
            return Math.Sin(input);
        }

        /// <summary>
        /// 正弦函数的导数
        /// </summary>
        /// <param name="input">
        /// 电流输入到神经元
        /// </param>
        /// <param name="output">
        /// 电流输出（激活）在神经元
        /// </param>
        /// <returns>
        /// 激活函数的导数的结果
        /// </returns>
        public override double Derivative(double input, double output)
        {
            return Math.Sqrt(1 - output * output);
        }

        /// <summary>
        /// 反序列化构造函数
        /// </summary>
        /// <param name="info">
        /// 反序列化的信息
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public SineLayer(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}