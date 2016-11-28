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
    /// <para>
    /// 这个接口代表一个神经网络。 典型的神经网络由一组由各种非循环互连组成。 输入层获得用户的输入，网络输出从输出层获得。
    /// </para>
    /// <para>
    /// 要创建神经网络，请按照下列步骤操作
    /// <list type="bullet">
    /// <item>创建和自定义图层</item>
    /// <item>在层之间建立连接（不存在周期）</item>
    /// <item>构造网络指定所需的输入和输出层</item>
    /// </list>
    /// </para>
    /// <para>
    /// 有两种模式可以训练神经网络。 在“批量训练”中，允许神经网络通过指定包含各种训练样本的预定义训练集来学习。 在“在线训练模式”中，每次产生随机训练样本（通常由另一个神经网络，称为“教师”网络）并用于训练。 两种模式都由重载的Learn()方法支持。 Run()方法用于针对特定输入运行神经网络。
    /// </para>
    /// </summary>
    public interface INetwork : ISerializable
    {
        /// <summary>
        /// 获取网络的输入层
        /// </summary>
        /// <value>
        /// 输入网络层。 此属性永远不为空。
        /// </value>
        ILayer InputLayer { get; }

        /// <summary>
        /// 获取网络的输出层
        /// </summary>
        /// <value>
        /// 输出网络层。 此属性永远不为空。
        /// </value>
        ILayer OutputLayer { get; }

        /// <summary>
        /// 获取网络中的层数。
        /// </summary>
        /// <value>
        /// 层计数。 此值始终为正。
        /// </value>
        int LayerCount { get; }

        /// <summary>
        /// 显示枚举器以迭代网络中的图层。
        /// </summary>
        /// <value>
        /// 层枚举。 网络中的任何图层都不能为空。
        /// </value>
        IEnumerable<ILayer> Layers { get; }

        /// <summary>
        /// 层索引器
        /// </summary>
        /// <param name="index">
        /// 索引
        /// </param>
        /// <returns>
        /// 图层在给定的索引
        /// </returns>
        /// <exception cref="System.IndexOutOfRangeException">
        /// 如果索引超出范围
        /// </exception>
        ILayer this[int index] { get; }

        /// <summary>
        /// 获取网络中的连接器数量。
        /// </summary>
        /// <value>
        /// 连接器计数。 此值从不为负。
        /// </value>
        int ConnectorCount { get; }

        /// <summary>
        /// 公开一个枚举器来迭代网络中的连接器。
        /// </summary>
        /// <value>
        /// 连接器枚举器。 网络中没有连接器可以为null。
        /// </value>
        IEnumerable<IConnector> Connectors { get; }

        /// <summary>
        /// 获取或设置抖动噪声的最大绝对限制
        /// </summary>
        /// <value>
        /// 抖动时添加的随机噪声的最大绝对限制
        /// </value>
        double JitterNoiseLimit { get; set; }

        /// <summary>
        /// 获取或设置抖动纪元
        /// </summary>
        /// <value>
        /// 执行抖动的时期（间隔）。 如果此值不为正，则不执行抖动。
        /// </value>
        int JitterEpoch { get; set; }

        /// <summary>
        /// 在“批处理训练”模式期间在新的训练迭代的开始期间调用该事件。
        /// </summary>
        event TrainingEpochEventHandler BeginEpochEvent;

        /// <summary>
        /// 当网络即将学习训练样本时，调用此事件。
        /// </summary>
        event TrainingSampleEventHandler BeginSampleEvent;

        /// <summary>
        /// 每当网络成功完成学习训练样本时，调用此事件。
        /// </summary>
        event TrainingSampleEventHandler EndSampleEvent;

        /// <summary>
        /// 每当在“批处理训练”模式期间成功完成训练迭代时，将调用此事件。
        /// </summary>
        event TrainingEpochEventHandler EndEpochEvent;

        /// <summary>
        /// 将学习速率设置为给定值。 网络中的所有层将在学习过程中使用该常数值作为学习速率。
        /// </summary>
        /// <param name="learningRate">
        /// 学习率
        /// </param>
        void SetLearningRate(double learningRate);

        /// <summary>
        /// 设置学习率的初始值和最终值。 在学习过程中，所有网络中的层将使用从一致地变化的有效学习速率将初始值转换为最终值。
        /// </summary>
        /// <param name="initialLearningRate">
        /// 学习率的初始值
        /// </param>
        /// <param name="finalLearningRate">
        /// 学习率的最终值
        /// </param>
        void SetLearningRate(double initialLearningRate, double finalLearningRate);

        /// <summary>
        /// 设置学习速率函数。
        /// </summary>
        /// <param name="learningRateFunction">
        /// 学习率函数使用。
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果学习速率函数为null
        /// </exception>
        void SetLearningRate(ILearningRateFunction learningRateFunction);

        /// <summary>
        /// 初始化所有层和连接器，使他们准备接受新的培训。
        /// </summary>
        void Initialize();

        /// <summary>
        /// 针对给定的输入运行神经网络
        /// </summary>
        /// <param name="input">
        /// 输入到网络
        /// </param>
        /// <returns>
        /// 网络的输出
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// 如果输入数组为null
        /// </exception>
        double[] Run(double[] input);

        /// <summary>
        /// 训练给定训练集的神经网络（批处理训练）
        /// </summary>
        /// <param name="trainingSet">
        /// 训练集使用
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期数。 （所有样本在每个训练时期以一些随机顺序训练）
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果训练集为null
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// 如果训练时期是零或负数
        /// </exception>
        void Learn(TrainingSet trainingSet, int trainingEpochs);

        /// <summary>
        /// 训练给定训练样本的网络（在线训练模式）。 注意这个方法仅训练样本一次，而不管当前的时期是什么。 参数只是用来评估训练进度和根据它调整参数值。
        /// </summary>
        /// <param name="trainingSample">
        /// 使用的培训样品
        /// </param>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练次数
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果训练样本为null
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// 如果训练次数不是正数
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 如果当前迭代次数是负数，或者如果它大于训练总次数
        /// </exception>
        void Learn(TrainingSample trainingSample, int currentIteration, int trainingEpochs);

        /// <summary>
        /// 如果网络当前正在学习，则该方法停止学习。
        /// </summary>
        void StopLearning();
    }
}