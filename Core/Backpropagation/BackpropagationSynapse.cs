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

namespace NeuronDotNet.Core.Backpropagation
{
    /// <summary>
    /// 反向传播突触连接两个激活神经元。 典型的反向传播网络包含数千个这样的突触。
    /// </summary>
    public class BackpropagationSynapse : ISynapse
    {
        private double weight;
        private double delta;
        private readonly ActivationNeuron sourceNeuron;
        private readonly ActivationNeuron targetNeuron;
        private readonly BackpropagationConnector parent;

        /// <summary>
        /// 获取源神经元
        /// </summary>
        /// <value>
        /// 突触的源神经元。 它永远不会<c> null </ c>。
        /// </value>
        public ActivationNeuron SourceNeuron
        {
            get { return sourceNeuron; }
        }

        /// <summary>
        /// 获取目标神经元
        /// </summary>
        /// <value>
        /// 突触的目标神经元。 它永远不会<c> null </ c>。
        /// </value>
        public ActivationNeuron TargetNeuron
        {
            get { return targetNeuron; }
        }

        INeuron ISynapse.SourceNeuron
        {
            get { return sourceNeuron; }
        }

        INeuron ISynapse.TargetNeuron
        {
            get { return targetNeuron; }
        }

        /// <summary>
        /// 获取或设置突触的权重
        /// </summary>
        /// <value>
        /// 突触的权重
        /// </value>
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        /// <summary>
        /// 获取父连接器
        /// </summary>
        /// <value>
        /// 包含此突触的父连接器。 它永远不会<c> null </ c>。
        /// </value>
        public BackpropagationConnector Parent
        {
            get { return parent; }
        }

        IConnector ISynapse.Parent
        {
            get { return parent; }
        }

        /// <summary>
        /// 创建一个新的反向传播突触连接给定的神经元
        /// </summary>
        /// <param name="sourceNeuron">
        /// 源神经元
        /// </param>
        /// <param name="targetNeuron">
        /// 目标神经元
        /// </param>
        /// <param name="parent">
        /// 包含此突触的父连接
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果任何参数是<c> null </ c>。
        /// </exception>
        public BackpropagationSynapse(
            ActivationNeuron sourceNeuron, ActivationNeuron targetNeuron, BackpropagationConnector parent)
        {
            Helper.ValidateNotNull(sourceNeuron, "sourceNeuron");
            Helper.ValidateNotNull(targetNeuron, "targetNeuron");
            Helper.ValidateNotNull(parent, "parent");

            this.weight = 1f;
            this.delta = 0f;

            sourceNeuron.TargetSynapses.Add(this);
            targetNeuron.SourceSynapses.Add(this);

            this.sourceNeuron = sourceNeuron;
            this.targetNeuron = targetNeuron;
            this.parent = parent;
        }

        /// <summary>
        /// 将信息从源神经元传播到目标神经元
        /// </summary>
        public void Propagate()
        {
            targetNeuron.input += sourceNeuron.output * this.weight;
        }

        /// <summary>
        /// 使用反向传播算法优化权重，以最小化误差
        /// </summary>
        /// <param name="learningFactor">
        /// 有效学习因素（学习率，培训进度等参数的函数）
        /// </param>
        public void OptimizeWeight(double learningFactor)
        {
            delta = delta * parent.momentum + learningFactor * targetNeuron.error * sourceNeuron.output;
            weight += delta;
        }

        /// <summary>
        /// 反向传播从目标神经元到源神经元的错误
        /// </summary>
        public void Backpropagate()
        {
            sourceNeuron.error += targetNeuron.error * this.weight;
        }

        /// <summary>
        /// 添加小随机噪声到这个突触的权重，以便网络偏离其局部最优位置（对进一步学习没用的局部平衡状态）
        /// </summary>
        /// <param name="jitterNoiseLimit">
        /// 对随机噪声的最大绝对限制
        /// </param>
        public void Jitter(double jitterNoiseLimit)
        {
            weight += Helper.GetRandom(-jitterNoiseLimit, jitterNoiseLimit);
        }
    }
}