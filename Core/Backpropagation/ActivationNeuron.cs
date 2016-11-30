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

namespace NeuronDotNet.Core.Backpropagation
{
    /// <summary>
    /// 激活神经元是反向传播神经网络的构件。
    /// </summary>
    public class ActivationNeuron : INeuron
    {
        internal double input;
        internal double output;
        internal double error;
        internal double bias;

        private readonly IList<ISynapse> sourceSynapses = new List<ISynapse>();
        private readonly IList<ISynapse> targetSynapses = new List<ISynapse>();

        private ActivationLayer parent;

        /// <summary>
        /// Gets or sets the neuron input.
        /// </summary>
        /// <value>
        /// 输入到神经元。 对于输入神经元，该值由用户指定，而其他神经元将在源突触传播时更新其输入
        /// </value>
        public double Input
        {
            get { return input; }
            set { input = value; }
        }

        /// <summary>
        /// 获取神经元的输出。
        /// </summary>
        /// <value>
        /// 神经元输出
        /// </value>
        public double Output
        {
            get { return output; }
        }

        /// <summary>
        /// 获取神经元错误
        /// </summary>
        /// <value>
        /// 神经元错误
        /// </value>
        public double Error
        {
            get { return error; }
        }

        /// <summary>
        /// 获取神经元偏差
        /// </summary>
        /// <value>
        /// 神经元偏差的当前值
        /// </value>
        public double Bias
        {
            get { return bias; }
        }

        /// <summary>
        /// 获取与这个神经元相关的源突触的列表
        /// </summary>
        /// <value>
        /// 源突触的列表。 它既不能<c> null </ c>，也不能包含<c> null </ c>元素。
        /// </value>
        public IList<ISynapse> SourceSynapses
        {
            get { return sourceSynapses; }
        }

        /// <summary>
        /// 获取与该神经元相关联的目标突触的列表
        /// </summary>
        /// <value>
        /// 目标突触的列表。 它既不能是<c> null </ c>，也不能包含<c> null </ c>元素。
        /// </value>
        public IList<ISynapse> TargetSynapses
        {
            get { return targetSynapses; }
        }

        /// <summary>
        /// 获取包含此神经元的父层
        /// </summary>
        /// <value>
        /// 包含这个神经元的父层。 它永远不会<c> null </ c>
        /// </value>
        public ActivationLayer Parent
        {
            get { return parent; }
        }

        ILayer INeuron.Parent
        {
            get { return parent; }
        }

        /// <summary>
        /// 创建一个新的激活神经元
        /// </summary>
        /// <param name="parent">
        /// 包含此神经元的父层
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// 如果<c>父</ c>是<c> null </ c>
        /// </exception>
        public ActivationNeuron(ActivationLayer parent)
        {
            Helper.ValidateNotNull(parent, "parent");

            this.input = 0d;
            this.output = 0d;
            this.error = 0d;
            this.bias = 0d;
            this.parent = parent;
        }

        /// <summary>
        /// 从源突触获取输入并激活以更新输出
        /// </summary>
        public void Run()
        {
            if (sourceSynapses.Count > 0)
            {
                input = 0d;
                for (int i = 0; i < sourceSynapses.Count; i++)
                {
                    sourceSynapses[i].Propagate();
                }
            }
            output = parent.Activate(bias + input, output);
        }

        /// <summary>
        /// 反向传播目标突触和评估错误
        /// </summary>
        public void EvaluateError()
        {
            if (targetSynapses.Count > 0)
            {
                error = 0d;
                foreach (BackpropagationSynapse synapse in targetSynapses)
                {
                    synapse.Backpropagate();
                }
            }
            error *= parent.Derivative(input, output);
        }

        /// <summary>
        /// 使用反向传播算法优化偏差值（如果不是<c> UseFixedBiasValues </ c>）和所有源突触的权重
        /// </summary>
        /// <param name="learningRate">
        /// 当前学习率（这取决于培训进度）
        /// </param>
        public void Learn(double learningRate)
        {
            if (!parent.useFixedBiasValues)
            {
                bias += learningRate * error;
            }
            for (int i = 0; i < sourceSynapses.Count; i++)
            {
                sourceSynapses[i].OptimizeWeight(learningRate);
            }
        }
    }
}
