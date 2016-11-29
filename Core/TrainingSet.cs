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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 训练集表示在“批处理训练”过程中使用的一组训练样本。
    /// </summary>
    [Serializable]
    public sealed class TrainingSet : ISerializable
    {
        private readonly int inputVectorLength;
        private readonly int outputVectorLength;
        private readonly IList<TrainingSample> trainingSamples;

        /// <summary>
        /// 获取集合中的训练样本数
        /// </summary>
        /// <value>
        /// 集合中的训练样本数。 此值从不为负。
        /// </value>
        public int TrainingSampleCount
        {
            get { return trainingSamples.Count; }
        }

        /// <summary>
        /// 获取输入向量的长度。
        /// </summary>
        /// <value>
        /// 输入矢量长度。 这总是正的，并且等于要训练的网络中的输入神经元的数量。
        /// </value>
        public int InputVectorLength
        {
            get { return inputVectorLength; }
        }

        /// <summary>
        /// 获取预期输出向量的长度。
        /// </summary>
        /// <value>
        /// 输出矢量长度。 对于无人监督的训练集，该值为零。 对于监督训练集，该值是正的，并且等于网络中的输出神经元的数量。
        /// </value>
        public int OutputVectorLength
        {
            get { return outputVectorLength; }
        }

        /// <summary>
        /// 在集合中的训练样本上展示一个枚举器到迭代器。
        /// </summary>
        /// <value>
        /// 训练样本枚举器。 没有返回的训练样本为<c> null </ c>。
        /// </value>
        public IEnumerable<TrainingSample> TrainingSamples
        {
            get
            {
                for (int i = 0; i < trainingSamples.Count; i++)
                {
                    yield return trainingSamples[i];
                }
            }
        }

        /// <summary>
        /// 训练样本索引器
        /// </summary>
        /// <param name="index">
        /// 训练样本索引
        /// </param>
        /// <returns>
        /// 在给定索引处训练样本
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// 如果索引超出范围
        /// </exception>
        public TrainingSample this[int index]
        {
            get { return trainingSamples[index]; }
        }

        /// <summary>
        /// 创建一个新的无监督训练集
        /// </summary>
        /// <param name="vectorSize">
        /// 训练集中向量的预期大小（注意：这应等于输入神经元的数量。）
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果vectorSize为零或负数
        /// </exception>
        public TrainingSet(int vectorSize)
            : this(vectorSize, 0)
        {
        }

        /// <summary>
        /// 创建一个新的监督训练集
        /// </summary>
        /// <param name="inputVectorLength">
        /// 输入矢量的长度
        /// </param>
        /// <param name="outputVectorLength">
        /// 预期输出向量的长度（无监督训练为零）
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果输入向量长度为零或负数，或输出向量长度为负数
        /// </exception>
        public TrainingSet(int inputVectorLength, int outputVectorLength)
        {
            // 验证
            Helper.ValidatePositive(inputVectorLength, "inputVectorLength");
            Helper.ValidateNotNegative(outputVectorLength, "outputVectorLength");

            // 初始化实例变量
            this.inputVectorLength = inputVectorLength;
            this.outputVectorLength = outputVectorLength;
            this.trainingSamples = new List<TrainingSample>();
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
        public TrainingSet(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");

            this.inputVectorLength = info.GetInt32("inputVectorLength");
            this.outputVectorLength = info.GetInt32("outputVectorLength");
            this.trainingSamples = info.GetValue("trainingSamples", typeof(IList<TrainingSample>)) as IList<TrainingSample>;
        }

        /// <summary>
        /// 用序列化训练集所需的数据填充序列化信息
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

            info.AddValue("inputVectorLength", inputVectorLength);
            info.AddValue("outputVectorLength", outputVectorLength);
            info.AddValue("trainingSamples", trainingSamples, typeof(IList<TrainingSample>));
        }

        /// <summary>
        /// 向训练集添加新的监督训练样本。 如果已经存在，它将被替换。
        /// </summary>
        /// <param name="sample">
        /// 要添加的示例
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> sample </ c>为<c> null </ c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果输入向量或输出向量的大小与其预期大小不同
        /// </exception>
        public void Add(TrainingSample sample)
        {
            // 验证
            Helper.ValidateNotNull(sample, "sample");
            if (sample.InputVector.Length != inputVectorLength)
            {
                throw new ArgumentException
                    ("Input vector must be of size " + inputVectorLength, "sample");
            }
            if (sample.OutputVector.Length != outputVectorLength)
            {
                throw new ArgumentException
                    ("Output vector must be of size " + outputVectorLength, "sample");
            }

            // 请注意，正在添加引用。 （样本是不可变的）
            trainingSamples.Add(sample);
        }

        /// <summary>
        /// 去除对应于给定向量的训练样本
        /// </summary>
        /// <param name="inputVector">
        /// 要删除的样本的输入向量
        /// </param>
        /// <returns>
        /// <c> true </ c>如果成功，<c> false </ c>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 如果输入向量<c> null </ c>
        /// </exception>
        public bool Remove(double[] inputVector)
        {
            return Remove(new TrainingSample(inputVector));
        }

        /// <summary>
        /// 删除给定的训练样本
        /// </summary>
        /// <param name="sample">
        /// 要除去的样品
        /// </param>
        /// <returns>
        /// <c> true </ c>如果成功，<c> false </ c>
        /// </returns>
        public bool Remove(TrainingSample sample)
        {
            return trainingSamples.Remove(sample);
        }

        /// <summary>
        /// 确定训练集合是否包含对应于给定向量的训练样本
        /// </summary>
        /// <param name="inputVector">
        /// 要定位的样本的输入向量
        /// </param>
        /// <returns>
        /// <c> true </ c>如果存在，<c> false </ c>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// 如果输入向量<c> null </ c>
        /// </exception>
        public bool Contains(double[] inputVector)
        {
            return Contains(new TrainingSample(inputVector));
        }

        /// <summary>
        /// 确定训练样本是否存在于集合中
        /// </summary>
        /// <param name="sample">
        /// 要定位的示例
        /// </param>
        /// <returns>
        /// <c> true </ c>如果存在，<c> false </ c>
        /// </returns>
        public bool Contains(TrainingSample sample)
        {
            return trainingSamples.Contains(sample);
        }

        /// <summary>
        /// 删除训练集中的所有训练样本。
        /// </summary>
        public void Clear()
        {
            trainingSamples.Clear();
        }
    }
}