using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpNetwork
{
    /// <summary>
    /// Bp神经网络
    /// </summary>
    public class BpNet
    {
        #region 属性
        public int inNum;//输入节点数
        int hideNum;//隐层节点数
        public int outNum;//输出层节点数
        public int sampleNum;//样本总数

        Random R;
        double[] x;//输入节点的输入数据
        double[] x1;//隐层节点的输出
        double[] x2;//输出节点的输出 
        double[] o1;//隐层的输入
        double[] o2;//输出层的输入
        public double[,] w;//权值矩阵w
        public double[,] v;//权值矩阵V
        public double[,] dw;//权值矩阵w
        public double[,] dv;//权值矩阵V

        public double rate;//学习率
        public double[] b1;//隐层阈值矩阵
        public double[] b2;//输出层阈值矩阵
        public double[] db1;//隐层阈值矩阵
        public double[] db2;//输出层阈值矩阵

        double[] pp;//输出层的误差
        double[] qq;//隐层的误差
        double[] yd;//输出层的教师数据
        public double e;//均方误差
        public double in_rate;//归一化比例系数
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inNum">输入节点数</param>
        /// <param name="outNum">输出层节点数</param>
        /// <param name="hideNum">隐层节点数</param>
        public BpNet(int inNum, int outNum, int hideNum)
        {
            R = new Random();

            this.inNum = inNum;
            this.outNum = outNum;
            this.hideNum = hideNum;

            x = new double[inNum];
            x1 = new double[hideNum];
            x2 = new double[outNum];

            o1 = new double[hideNum];
            o2 = new double[outNum];

            w = new double[inNum, hideNum];
            v = new double[hideNum, outNum];
            dw = new double[inNum, hideNum];
            dv = new double[hideNum, outNum];

            b1 = new double[hideNum];
            b2 = new double[outNum];
            db1 = new double[hideNum];
            db2 = new double[outNum];

            pp = new double[hideNum];
            qq = new double[outNum];
            yd = new double[outNum];

            //初始化w
            for (int i = 0; i < inNum; i++)
            {
                for (int j = 0; j < hideNum; j++)
                {
                    w[i, j] = (R.NextDouble() * 2 - 1.0) / 2;
                }
            }

            //初始化v
            for (int i = 0; i < hideNum; i++)
            {
                for (int j = 0; j < outNum; j++)
                {
                    v[i, j] = (R.NextDouble() * 2 - 1.0) / 2;
                }
            }

            rate = 0.8;
            e = 0.0;
            in_rate = 1.0;
        }

        /// <summary>
        /// 计算隐层节点数
        /// </summary>
        /// <param name="m">输入节点数</param>
        /// <param name="n">输出层节点数</param>
        /// <returns>隐层节点数</returns>
        public static int computeHideNum(int m, int n)
        {
            double s = Math.Sqrt(0.43 * m * n + 0.12 * n * n + 2.54 * m + 0.77 * n + 0.35) + 0.51;
            int ss = Convert.ToInt32(s);
            return ((s - (double)ss) > 0.5) ? ss + 1 : ss;

        }

        /// <summary>
        /// 训练函数
        /// </summary>
        /// <param name="p">自变量矩阵</param>
        /// <param name="t">因变量矩阵</param>
        public void train(double[,] p, double[,] t)
        {
            e = 0.0;
            //求p，t中的最大值
            double pMax = 0.0;
            for (int isamp = 0; isamp < sampleNum; isamp++)
            {
                for (int i = 0; i < inNum; i++)
                {
                    if (Math.Abs(p[isamp, i]) > pMax)
                    {
                        pMax = Math.Abs(p[isamp, i]);
                    }
                }

                for (int j = 0; j < outNum; j++)
                {
                    if (Math.Abs(t[isamp, j]) > pMax)
                    {
                        pMax = Math.Abs(t[isamp, j]);
                    }
                }

                in_rate = pMax;
            }//end isamp
            for (int isamp = 0; isamp < sampleNum; isamp++)
            {
                //数据归一化
                for (int i = 0; i < inNum; i++)
                {
                    x[i] = p[isamp, i] / in_rate;
                }
                for (int i = 0; i < outNum; i++)
                {
                    yd[i] = t[isamp, i] / in_rate;
                }

                //计算隐层的输入和输出
                for (int j = 0; j < hideNum; j++)
                {
                    o1[j] = 0.0;
                    for (int i = 0; i < inNum; i++)
                    {
                        o1[j] += w[i, j] * x[i];
                    }
                    x1[j] = 1.0 / (1.0 + Math.Exp(-o1[j] - b1[j]));
                }

                //计算输出层的输入和输出
                for (int k = 0; k < outNum; k++)
                {
                    o2[k] = 0.0;
                    for (int j = 0; j < hideNum; j++)
                    {
                        o2[k] += v[j, k] * x1[j];
                    }
                    x2[k] = 1.0 / (1.0 + Math.Exp(-o2[k] - b2[k]));
                }

                //计算输出层误差和均方差

                for (int k = 0; k < outNum; k++)
                {
                    qq[k] = (yd[k] - x2[k]) * x2[k] * (1.0 - x2[k]);
                    e += (yd[k] - x2[k]) * (yd[k] - x2[k]);
                    //更新V
                    for (int j = 0; j < hideNum; j++)
                    {
                        v[j, k] += rate * qq[k] * x1[j];
                    }
                }

                //计算隐层误差

                for (int j = 0; j < hideNum; j++)
                {
                    pp[j] = 0.0;
                    for (int k = 0; k < outNum; k++)
                    {
                        pp[j] += qq[k] * v[j, k];
                    }
                    pp[j] = pp[j] * x1[j] * (1 - x1[j]);

                    //更新W

                    for (int i = 0; i < inNum; i++)
                    {
                        w[i, j] += rate * pp[j] * x[i];
                    }
                }

                //更新b2
                for (int k = 0; k < outNum; k++)
                {
                    b2[k] += rate * qq[k];
                }

                //更新b1
                for (int j = 0; j < hideNum; j++)
                {
                    b1[j] += rate * pp[j];
                }

            }
            e = Math.Sqrt(e);
        }

        /// <summary>
        /// 调整权值矩阵w
        /// </summary>
        /// <param name="w">权值矩阵w</param>
        /// <param name="dw">矩阵变化矩阵</param>
        public void adjustWV(double[,] w, double[,] dw)
        {
            for (int i = 0; i < w.GetLength(0); i++)
            {
                for (int j = 0; j < w.GetLength(1); j++)
                {
                    w[i, j] += dw[i, j];
                }
            }
        }

        /// <summary>
        /// 调整权值矩阵w
        /// </summary>
        /// <param name="w">权值矩阵w</param>
        /// <param name="dw">矩阵变化数组</param>
        public void adjustWV(double[] w, double[] dw)
        {
            for (int i = 0; i < w.Length; i++)
            {
                w[i] += dw[i];
            }
        }

        /// <summary>
        /// 数据仿真函数
        /// </summary>
        /// <param name="psim">自变量</param>
        /// <returns>因变量</returns>
        public double[] sim(double[] psim)
        {
            for (int i = 0; i < inNum; i++)
                x[i] = psim[i] / in_rate;

            for (int j = 0; j < hideNum; j++)
            {
                o1[j] = 0.0;
                for (int i = 0; i < inNum; i++)
                    o1[j] = o1[j] + w[i, j] * x[i];
                x1[j] = 1.0 / (1.0 + Math.Exp(-o1[j] - b1[j]));
            }
            for (int k = 0; k < outNum; k++)
            {
                o2[k] = 0.0;
                for (int j = 0; j < hideNum; j++)
                    o2[k] = o2[k] + v[j, k] * x1[j];
                x2[k] = 1.0 / (1.0 + Math.Exp(-o2[k] - b2[k]));
                x2[k] = in_rate * x2[k];
            }
            return x2;
        }

        /// <summary>
        /// 保存矩阵w,v
        /// </summary>
        /// <param name="w">矩阵w和矩阵v</param>
        /// <param name="filename">文件路径</param>
        public void saveMatrix(double[,] w, string filename)
        {

            StreamWriter sw = File.CreateText(filename);
            for (int i = 0; i < w.GetLength(0); i++)
            {
                for (int j = 0; j < w.GetLength(1); j++)
                {
                    sw.Write(w[i, j] + " ");
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        /// <summary>
        /// 保存矩阵b1,b2
        /// </summary>
        /// <param name="b">矩阵b1和矩阵b2</param>
        /// <param name="filename">文件路径</param>
        public void saveMatrix(double[] b, string filename)
        {
            StreamWriter sw = File.CreateText(filename);
            for (int i = 0; i < b.Length; i++)
            {
                sw.WriteLine(b[i] + " ");
            }
            sw.Close();
        }

        /// <summary>
        /// 读取矩阵W,V
        /// </summary>
        /// <param name="w">矩阵W和矩阵V</param>
        /// <param name="filename">文件路径</param>
        public void readMatrixW(double[,] w, string filename)
        {

            StreamReader sr;
            try
            {
                sr = new StreamReader(filename, Encoding.GetEncoding("gb2312"));
                String line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {

                    string[] s1 = line.Trim().Split(' ');
                    for (int j = 0; j < s1.Length; j++)
                    {
                        w[i, j] = Convert.ToDouble(s1[j]);
                    }
                    i++;
                }
                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 读取矩阵b1,b2
        /// </summary>
        /// <param name="b">矩阵b1和矩阵b2</param>
        /// <param name="filename"></param>
        public void readMatrixB(double[] b, string filename)
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(filename, Encoding.GetEncoding("gb2312"));

                String line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    b[i] = Convert.ToDouble(line);
                    i++;
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 获取矩阵W,V的最大行数和最大列数
        /// </summary>
        /// <param name="rows">最大行数</param>
        /// <param name="columns">最大列数</param>
        /// <param name="filename">文件路径</param>
        public void getMatrixWMax(out int rows, out int columns, string filename)
        {
            StreamReader sr;
            rows = 0;
            columns = 0;
            try
            {
                sr = new StreamReader(filename, Encoding.GetEncoding("gb2312"));
                String line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] s1 = line.Trim().Split(' ');
                    if (s1.Length > columns)
                    {
                        columns = s1.Length;
                    }
                    rows++;
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 获取矩阵b1,b2的最大宽度
        /// </summary>
        /// <param name="rows">最大宽度</param>
        /// <param name="filename">文件路径</param>
        public void GetMatrixBMax(out int rows, string filename)
        {
            StreamReader sr;
            rows = 0;
            try
            {
                sr = new StreamReader(filename, Encoding.GetEncoding("gb2312"));
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    rows++;
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
