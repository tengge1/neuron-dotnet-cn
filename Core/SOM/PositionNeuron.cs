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
using System.Drawing;

namespace NeuronDotNet.Core.SOM
{
    /// <summary>
    /// 位置神经元是Kohonen网络中使用的二维空间中的神经元。
    /// </summary>
    public class PositionNeuron : INeuron
    {
        private readonly KohonenLayer parent;
        private readonly Point coordinate;
        private readonly IList<ISynapse> sourceSynapses = new List<ISynapse>();
        private readonly IList<ISynapse> targetSynapses = new List<ISynapse>();

        internal double value;
        internal double neighborhoodValue;

        /// <summary>
        /// 获取包含此神经元的父层
        /// </summary>
        /// <value>
        /// 包含这个神经元的父层。 它永远不会<c> null </ c>
        /// </value>
        public KohonenLayer Parent
        {
            get { return parent; }
        }

        /// <summary>
        /// 获取神经元值
        /// </summary>
        /// <value>
        /// 神经元值
        /// </value>
        public double Value
        {
            get { return value; }
        }

        double INeuron.Input
        {
            get { return value; }
            set { this.value = value; }
        }

        double INeuron.Output
        {
            get { return value; }
        }

        ILayer INeuron.Parent
        {
            get { return parent; }
        }

        /// <summary>
        /// 获取神经元的位置
        /// </summary>
        /// <value>
        /// 神经元坐标
        /// </value>
        public Point Coordinate
        {
            get { return coordinate; }
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
        /// 获得这个神经元的相对于该层中的获胜者神经元的邻域
        /// </summary>
        /// <value>
        /// 邻域值
        /// </value>
        public double NeighborhoodValue
        {
            get { return neighborhoodValue; }
        }

        /// <summary>
        /// 创建新的位置神经元
        /// </summary>
        /// <param name="x">
        /// X - 神经元位置的坐标
        /// </param>
        /// <param name="y">
        /// Y-神经元位置的坐标
        /// </param>
        /// <param name="parent">
        /// 包含此神经元的父层
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c>父</ c>是<c> null </ c>
        /// </exception>
        public PositionNeuron(int x, int y, KohonenLayer parent)
            : this(new Point(x, y), parent)
        {
        }

        /// <summary>
        /// 创建新的位置神经元
        /// </summary>
        /// <param name="coordinate">
        /// 神经元位置
        /// </param>
        /// <param name="parent">
        /// 包含这个神经元的父神经元
        /// </param>
        /// <value>
        /// 如果<c>父</ c>是<c> null </ c>
        /// </value>
        public PositionNeuron(Point coordinate, KohonenLayer parent)
        {
            Helper.ValidateNotNull(parent, "parent");

            this.coordinate = coordinate;
            this.parent = parent;
            this.value = 0d;
        }

        /// <summary>
        /// 运行神经元。 （传播源突触并更新输入和输出值）
        /// </summary>
        public void Run()
        {
            if (sourceSynapses.Count > 0)
            {
                value = 0d;
                for (int i = 0; i < sourceSynapses.Count; i++)
                {
                    sourceSynapses[i].Propagate();
                }
                value = Math.Sqrt(value);
            }
        }

        /// <summary>
        /// 训练相关源突触的权重。
        /// </summary>
        /// <param name="learningRate">
        /// 当前学习率（这取决于培训进度）
        /// </param>
        public void Learn(double learningRate)
        {
            double learningFactor = learningRate * neighborhoodValue;
            for (int i = 0; i < sourceSynapses.Count; i++)
            {
                sourceSynapses[i].OptimizeWeight(learningFactor);
            }
        }
    }
}