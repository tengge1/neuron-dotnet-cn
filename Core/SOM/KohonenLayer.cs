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
using System.Drawing;
using System.Runtime.Serialization;
using NeuronDotNet.Core.SOM.NeighborhoodFunctions;

namespace NeuronDotNet.Core.SOM
{
    /// <summary>
    /// Kohonen层是包含位置神经元的层。
    /// </summary>
    [Serializable]
    public class KohonenLayer : Layer<PositionNeuron>
    {
        private readonly Size size;
        private readonly LatticeTopology topology;
        private bool isRowCircular;
        private bool isColumnCircular;
        private PositionNeuron winner;
        private INeighborhoodFunction neighborhoodFunction;

        /// <summary>
        /// 获取图层大小
        /// </summary>
        /// <value>
        /// 层的大小（宽度是列数，高度是行数）（换句话说，宽度是行中的神经元的数量，高度是列中的神经元的数量）
        /// </value>
        public Size Size
        {
            get { return size; }
        }

        /// <summary>
        /// 获取网格拓扑
        /// </summary>
        /// <value>
        /// 层中神经元的网格拓扑
        /// </value>
        public LatticeTopology Topology
        {
            get { return topology; }
        }

        /// <summary>
        /// 获取或设置表示神经元行是否是圆形的布尔值
        /// </summary>
        /// <value>
        /// 表示神经元行是否为圆形的布尔值
        /// </value>
        public bool IsRowCircular
        {
            get { return isRowCircular; }
            set { isRowCircular = value; }
        }

        /// <summary>
        /// 获取或设置表示神经元列是否为圆形的布尔值
        /// </summary>
        /// <value>
        /// 表示神经元列是否为圆形的布尔值
        /// </value>
        public bool IsColumnCircular
        {
            get { return isColumnCircular; }
            set { isColumnCircular = value; }
        }

        /// <summary>
        /// 获取图层的优胜者神经元
        /// </summary>
        /// <value>
        /// 优胜者神经元
        /// </value>
        public PositionNeuron Winner
        {
            get { return winner; }
        }

        /// <summary>
        /// 获取或设置邻域函数
        /// </summary>
        /// <value>
        /// 邻域函数
        /// </value>
        public INeighborhoodFunction NeighborhoodFunction
        {
            get { return neighborhoodFunction; }
            set { neighborhoodFunction = value; }
        }

        /// <summary>
        /// 位置神经元分度器
        /// </summary>
        /// <param name="x">
        /// X - 神经元的坐标
        /// </param>
        /// <param name="y">
        /// Y-神经元的坐标
        /// </param>
        /// <returns>
        /// 在给定坐标的神经元
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// 如果任何索引超出范围
        /// </exception>
        public PositionNeuron this[int x, int y]
        {
            get { return neurons[x + y * size.Width]; }
        }

        /// <summary>
        /// 创建线性Kohonen图层
        /// </summary>
        /// <param name="neuronCount">
        /// 图层中的神经元数量
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果<c> neuronCount </ c>为零或负数
        /// </exception>
        public KohonenLayer(int neuronCount)
            : this(new Size(neuronCount, 1))
        {
        }

        /// <summary>
        /// 使用给定的邻域函数创建线性Kohonen图层。
        /// </summary>
        /// <param name="neuronCount">
        /// 图层中的神经元数量
        /// </param>
        /// <param name="neighborhoodFunction">
        /// 邻域函数
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> neighborhoodFunction </ c>是<c> null </ c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果<c> neuronCount </ c>为零或负数
        /// </exception>
        public KohonenLayer(int neuronCount, INeighborhoodFunction neighborhoodFunction)
            : this(new Size(neuronCount, 1), neighborhoodFunction)
        {
        }

        /// <summary>
        /// 创建具有给定大小的Kohonen图层
        /// </summary>
        /// <param name="size">
        /// 层的大小
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果图层宽度或图层高度不为正值
        /// </exception>
        public KohonenLayer(Size size)
            : this(size, new GaussianFunction(Math.Max(size.Width, size.Height) / 2))
        {
        }

        /// <summary>
        /// 创建具有指定大小和邻域函数的Kohonen图层
        /// </summary>
        /// <param name="size">
        /// 层的大小
        /// </param>
        /// <param name="neighborhoodFunction">
        /// 邻居功能使用
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> neighborhoodFunction </ c>是<c> null </ c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果图层宽度或图层高度不为正值
        /// </exception>
        public KohonenLayer(Size size, INeighborhoodFunction neighborhoodFunction)
            : this(size, neighborhoodFunction, LatticeTopology.Rectangular)
        {
        }

        /// <summary>
        /// 创建具有指定大小和拓扑的Kohonen图层
        /// </summary>
        /// <param name="size">
        /// 层的大小
        /// </param>
        /// <param name="topology">
        /// 神经元的网格拓扑
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果图层宽度或图层高度不为正，或如果<c>拓扑</ c>无效
        /// </exception>
        public KohonenLayer(Size size, LatticeTopology topology)
            : this(size, new GaussianFunction(Math.Max(size.Width, size.Height) / 2), topology)
        {
        }

        /// <summary>
        /// 创建具有指定大小，拓扑和邻域函数的Kohonen图层
        /// </summary>
        /// <param name="size">
        /// 层的大小
        /// </param>
        /// <param name="neighborhoodFunction">
        /// 使用的邻域函数
        /// </param>
        /// <param name="topology">
        /// 神经元的网格拓扑
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> neighborhoodFunction </ c>是<c> null </ c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果图层宽度或图层高度不为正，或如果<c>拓扑</ c>无效
        /// </exception>
        public KohonenLayer(Size size, INeighborhoodFunction neighborhoodFunction, LatticeTopology topology)
            : base(size.Width * size.Height)
        {
            // 当宽度和高度都为负时，该乘积可以为正。 所以，我们需要检查一个。
            Helper.ValidatePositive(size.Width, "size.Width");

            Helper.ValidateNotNull(neighborhoodFunction, "neighborhoodFunction");
            Helper.ValidateEnum(typeof(LatticeTopology), topology, "topology");

            this.size = size;
            this.neighborhoodFunction = neighborhoodFunction;
            this.topology = topology;

            int k = 0;
            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    neurons[k++] = new PositionNeuron(new Point(x, y), this);
                }
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
        public KohonenLayer(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.size.Height = info.GetInt32("size.Height");
            this.size.Width = info.GetInt32("size.Width");

            this.topology = (LatticeTopology)info.GetValue("topology", typeof(LatticeTopology));

            this.neighborhoodFunction
                = info.GetValue("neighborhoodFunction", typeof(INeighborhoodFunction))
                as INeighborhoodFunction;

            this.isRowCircular = info.GetBoolean("isRowCircular");
            this.isColumnCircular = info.GetBoolean("isColumnCircular");
        }

        /// <summary>
        /// 使用序列化图层所需的数据填充序列化信息
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
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("topology", topology);
            info.AddValue("size.Height", size.Height);
            info.AddValue("size.Width", size.Width);
            info.AddValue("neighborhoodFunction", neighborhoodFunction, typeof(INeighborhoodFunction));
            info.AddValue("isRowCircular", isRowCircular);
            info.AddValue("isColumnCircular", isColumnCircular);
        }

        /// <summary>
        /// 初始化所有神经元，并使他们准备好接受新鲜的训练。
        /// </summary>
        public override void Initialize()
        {
            //由于在这个层中没有可初始化的参数，这是一个无用的功能
        }

        /// <summary>
        /// 运行图层中的所有神经元，并找到最优解
        /// </summary>
        public override void Run()
        {
            this.winner = neurons[0];
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].Run();
                if (neurons[i].value < winner.value)
                {
                    winner = neurons[i];
                }
            }
        }

        /// <summary>
        /// 允许所有神经元及其源连接器学习。 该方法假定一个学习环境，其中输入，输出和其他参数（如果有的话）是适当的。
        /// </summary>
        /// <param name="currentIteration">
        /// 当前学习迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期的总数
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果<c> trainingEpochs </ c>为零或负值
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 如果<c> currentIteration </ c>为负，或者如果它不小于<c> trainingEpochs </ c>
        /// </exception>
        public override void Learn(int currentIteration, int trainingEpochs)
        {
            // 验证委托
            neighborhoodFunction.EvaluateNeighborhood(this, currentIteration, trainingEpochs);
            base.Learn(currentIteration, trainingEpochs);
        }
    }
}