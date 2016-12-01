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
    /// Kohonen Synapse用于将神经元连接到位置神经元。 它将数据从输入神经元传播到输出位置神经元，并自我组织其权重以匹配输入。
    /// </summary>
    public class KohonenSynapse : ISynapse
    {
        private double weight;
        private KohonenConnector parent;
        private INeuron sourceNeuron;
        private PositionNeuron targetNeuron;

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
        public KohonenConnector Parent
        {
            get { return parent; }
        }

        IConnector ISynapse.Parent
        {
            get { return parent; }
        }

        INeuron ISynapse.TargetNeuron
        {
            get { return targetNeuron; }
        }

        /// <summary>
        /// 获取源神经元
        /// </summary>
        /// <value>
        /// 突触的源神经元。 它永远不会<c> null </ c>。
        /// </value>
        public INeuron SourceNeuron
        {
            get { return sourceNeuron; }
        }

        /// <summary>
        /// 获取目标神经元
        /// </summary>
        /// <value>
        /// 突触的目标神经元。 它永远不会<c> null </ c>。
        /// </value>
        public PositionNeuron TargetNeuron
        {
            get { return targetNeuron; }
        }

        /// <summary>
        /// 创建一个新的Kohonen Synapse连接给定的神经元
        /// </summary>
        /// <param name="sourceNeuron">
        /// 源神经元
        /// </param>
        /// <param name="targetNeuron">
        /// 目标神经元
        /// </param>
        /// <param name="parent">
        /// 包含此突触的父连接器
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果任何参数是<c> null </ c>
        /// </exception>
        public KohonenSynapse(INeuron sourceNeuron, PositionNeuron targetNeuron, KohonenConnector parent)
        {
            Helper.ValidateNotNull(sourceNeuron, "sourceNeuron");
            Helper.ValidateNotNull(targetNeuron, "targetNeuron");
            Helper.ValidateNotNull(parent, "parent");

            this.weight = 1d;

            sourceNeuron.TargetSynapses.Add(this);
            targetNeuron.SourceSynapses.Add(this);

            this.sourceNeuron = sourceNeuron;
            this.targetNeuron = targetNeuron;
            this.parent = parent;
        }

        /// <summary>
        /// 将数据从源神经元传播到目标神经元
        /// </summary>
        public void Propagate()
        {
            double similarity = sourceNeuron.Output - weight;
            targetNeuron.value += similarity * similarity;
        }

        /// <summary>
        /// 优化权重以匹配输入
        /// </summary>
        /// <param name="learningFactor">
        /// 有效学习因素。 这是训练进度，学习速率和目标神经元的邻域值的函数。
        /// </param>
        public void OptimizeWeight(double learningFactor)
        {
            weight += learningFactor * (sourceNeuron.Output - weight);
        }

        /// <summary>
        /// 添加小随机噪声到这个突触的权重，以便网络偏离其局部最优位置（对进一步学习无用的局部平衡状态）
        /// </summary>
        /// <param name="jitterNoiseLimit">
        /// 对随机噪声的最大绝对限制
        /// </param>
        public void Jitter(double jitterNoiseLimit)
        {
            this.weight += Helper.GetRandom(-jitterNoiseLimit, jitterNoiseLimit);
        }
    }
}
