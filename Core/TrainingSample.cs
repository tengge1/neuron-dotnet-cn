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

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 这个类表示用于训练神经网络的训练样本
    /// </summary>
    [Serializable]
    public class TrainingSample : ISerializable
    {
        private readonly double[] inputVector;
        private readonly double[] outputVector;
        private readonly double[] normalizedInputVector;
        private readonly double[] normalizedOutputVector;
        private readonly int hashCode;

        /// <summary>
        /// 获取输入向量的值。
        /// </summary>
        /// <value>
        /// 输入向量。 它永远不会<c> null </ c>。
        /// </value>
        public double[] InputVector
        {
            get { return inputVector; }
        }

        /// <summary>
        /// 获取预期输出向量的值
        /// </summary>
        /// <value>
        /// 输出向量。 它永远不会<c> null </ c>。
        /// </value>
        public double[] OutputVector
        {
            get { return outputVector; }
        }

        /// <summary>
        /// 以归一化形式获取输入向量的值
        /// </summary>
        /// <value>
        /// 归一化输入向量。 它永远不会<c> null </ c>。
        /// </value>
        public double[] NormalizedInputVector
        {
            get { return normalizedInputVector; }
        }

        /// <summary>
        /// 以归一化形式获取输出向量的值。
        /// </summary>
        /// <value>
        /// 归一化输出向量。 它永远不会<c> null </ c>。
        /// </value>
        public double[] NormalizedOutputVector
        {
            get { return normalizedOutputVector; }
        }

        /// <summary>
        /// 创建一个新的无监督训练样本
        /// </summary>
        /// <param name="vector">
        /// 向量表示无监督训练样本
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果vector是<c> null </ c>
        /// </exception>
        public TrainingSample(double[] vector)
            : this(vector, new double[0])
        {
        }

        /// <summary>
        /// 创建新的训练样本。 参数被克隆到训练样本中。 因此，对参数的任何修改都不会反映在训练样本中。
        /// </summary>
        /// <param name="inputVector">
        /// 输入向量
        /// </param>
        /// <param name="outputVector">
        /// 预期输出向量
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果任何参数是<c> null </ c>
        /// </exception>
        public TrainingSample(double[] inputVector, double[] outputVector)
        {
            // 验证
            Helper.ValidateNotNull(inputVector, "inputVector");
            Helper.ValidateNotNull(outputVector, "outputVector");

            // 克隆并初始化
            this.inputVector = (double[])inputVector.Clone();
            this.outputVector = (double[])outputVector.Clone();

            // 一些神经网络需要归一化形式的输入。作为优化措施，我们规范和存储训练样本
            this.normalizedInputVector = Helper.Normalize(inputVector);
            this.normalizedOutputVector = Helper.Normalize(outputVector);

            // 计算哈希码
            hashCode = 0;
            for (int i = 0; i < inputVector.Length; i++)
            {
                hashCode ^= inputVector[i].GetHashCode();
            }
        }

        /// <summary>
        /// 反序列化构造函数
        /// </summary>
        /// <param name="info">
        /// 序列化信息反序列化和获取数据
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public TrainingSample(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");

            this.inputVector = (double[])info.GetValue("inputVector", typeof(double[]));
            this.outputVector = (double[])info.GetValue("outputVector", typeof(double[]));
            this.normalizedInputVector = Helper.Normalize(inputVector);
            this.normalizedOutputVector = Helper.Normalize(outputVector);

            hashCode = 0;
            for (int i = 0; i < inputVector.Length; i++)
            {
                hashCode ^= inputVector[i].GetHashCode();
            }
        }

        /// <summary>
        /// 用串行化训练样本所需的数据填充序列化信息
        /// </summary>
        /// <param name="info">
        /// 用于填充数据的序列化信息
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");

            info.AddValue("inputVector", inputVector, typeof(double[]));
            info.AddValue("outputVector", outputVector, typeof(double[]));
        }

        /// <summary>
        /// 确定给定对象是否等于此实例
        /// </summary>
        /// <param name="obj">
        /// 要与此实例进行比较的对象
        /// </param>
        /// <returns>
        /// <c> true </ c>如果给定对象等于此实例，则<c> false </ c>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is TrainingSample)
            {
                TrainingSample sample = (TrainingSample)obj;
                int size;
                if ((size = sample.inputVector.Length) == inputVector.Length)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (inputVector[i] != sample.inputVector[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 作为特定类型的哈希函数
        /// </summary>
        /// <returns>
        /// 当前对象的哈希码
        /// </returns>
        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}
