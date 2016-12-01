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

namespace NeuronDotNet.Core.SOM.NeighborhoodFunctions
{
    /// <summary>
    /// Gaussian Neighborhood Function. It is a continuous bell shaped curve centered at winner neuron.
    /// </summary>
    [Serializable]
    public sealed class GaussianFunction : INeighborhoodFunction
    {
        /* 
         *  高斯函数 = a * Exp( - ((x-b)^2) / 2 (c^2))
         *
         *  参数“a”是曲线峰的高度，“b”是峰中心的位置，“c”控制钟形的宽度。
         *
         *  对于高斯邻域函数，
         *  a = 统一（最优解的邻域）
         *  b = 最优位置
         *  c = 依赖训练过程
         *
         *  c的初始值从用户获得（作为学习半径）
         *  注意，（x-b）方形表示获胜神经元'b'和神经元'x'之间的欧氏距离
         *
         *                     _._
         *                    /   \
         *                   |     |
         *                  /       \
         *             ___-           -___
         *                      .
         *                  最优位置
         */

        private readonly double sigma = 0d;

        /// <summary>
        /// 获取sigma的初始值
        /// </summary>
        /// <value>
        /// sigma的初始值
        /// </value>
        public double Sigma
        {
            get { return sigma; }
        }

        /// <summary>
        /// 创建一个新的高斯邻域函数
        /// </summary>
        /// <param name="learningRadius">
        /// 初始学习半径
        /// </param>
        public GaussianFunction(int learningRadius)
        {
            // 高斯曲线的半高全宽
            //        = sigma * Math.Sqrt(2 * ln(2)) = sigma * 2.35482

            // 半高全宽（FWHM）只是学习直径
            // 所以， 学习半径 = 1.17741 * sigma

            this.sigma = learningRadius / 1.17741d;
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
        public GaussianFunction(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");
            this.sigma = info.GetDouble("sigma");
        }

        /// <summary>
        /// 用序列化邻域函数所需的数据填充序列化信息
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
            info.AddValue("sigma", sigma);
        }

        /// <summary>
        /// 使用高斯函数确定给定的Kohonen层中相对于获胜者神经元的每个神经元的邻域
        /// </summary>
        /// <param name="layer">
        /// 含有神经元的Kohonen层
        /// </param>
        /// <param name="currentIteration">
        /// 当前训练迭代
        /// </param>
        /// <param name="trainingEpochs">
        /// 训练时期的总数
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> layer </ c>为<c> null </ c>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 如果<c> trainingEpochs </ c>为零或负值
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 如果<c> currentIteration </ c>为负，或者如果它不小于<c> trainingEpochs </ c>
        /// </exception>
        public void EvaluateNeighborhood(KohonenLayer layer, int currentIteration, int trainingEpochs)
        {
            Helper.ValidateNotNull(layer, "layer");
            Helper.ValidatePositive(trainingEpochs, "trainingEpochs");
            Helper.ValidateWithinRange(currentIteration, 0, trainingEpochs - 1, "currentIteration");

            // 优胜者坐标
            int winnerX = layer.Winner.Coordinate.X;
            int winnerY = layer.Winner.Coordinate.Y;

            // 图层宽度和高度
            int layerWidth = layer.Size.Width;
            int layerHeight = layer.Size.Height;

            // 随着训练的进行，Sigma值均匀地减小到零
            double currentSigma = sigma - ((sigma * currentIteration) / trainingEpochs);

            // 优化措施：预计算的2-Sigma-Square
            double twoSigmaSquare = 2 * currentSigma * currentSigma;

            // 评估和更新每个神经元的邻域值
            foreach (PositionNeuron neuron in layer.Neurons)
            {
                int dx = Math.Abs(winnerX - neuron.Coordinate.X);
                int dy = Math.Abs(winnerY - neuron.Coordinate.Y);

                if (layer.IsRowCircular)
                {
                    dx = Math.Min(dx, layerWidth - dx);
                }
                if (layer.IsColumnCircular)
                {
                    dy = Math.Min(dy, layerHeight - dy);
                }

                double dxSquare = dx * dx;
                double dySquare = dy * dy;
                if (layer.Topology == LatticeTopology.Hexagonal)
                {
                    if (dy % 2 == 1)
                    {
                        dxSquare += 0.25 + (((neuron.Coordinate.X > winnerX) == (winnerY % 2 == 0)) ? dx : -dx);
                    }
                    dySquare *= 0.75;
                }
                neuron.neighborhoodValue = Math.Exp(-(dxSquare + dySquare) / twoSigmaSquare);
            }
        }
    }
}