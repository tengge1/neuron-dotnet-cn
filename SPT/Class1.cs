using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPT
{
    /// <summary>
    /// 线性回归
    /// </summary>
    public class SPTHelper
    {
        /// <summary>
        ///  一元线性回归分析
        /// </summary>
        /// <param name="x"> 存放自变量x的n个取值</param>
        /// <param name="y">存放与自变量x的n个取值相对应的随机变量y的观察值</param>
        /// <param name="n"> 观察点数</param>
        /// <param name="a"> a(0) 返回回归系数b ,a(1)返回回归系数a</param>
        /// <param name="dt"> dt(0) 返回偏差平方和q ,dt(1)返回平均标准偏差s ,dt(2)返回回归平方和p,dt(3)返回最大偏差umax,dt(4)返回最小偏差umin,dt(5)返回偏差平均值u</param>
        public static void SPT1(double[] x, double[] y, int n, double[] a, double[] dt)
        {
            int i;
            double xx, yy, e, f, q, u, p, umax, umin, s;
            xx = 0.0;
            yy = 0.0;
            for (i = 0; i <= n - 1; i++)
            {
                xx = xx + x[i] / n;//x的平均值
                yy = yy + y[i] / n;//y的平均值
            }
            double Sx = 0.0;
            double Sxy = 0.0;
            double Sy = 0.0;
            double Sxx = 0.0;
            for (i = 0; i < n; i++)
            {
                Sx += x[i];
                Sxy += x[i] * y[i];
                Sy += y[i];
                Sxx += x[i] * x[i];
            }
            a[0] = (Sx * Sxy - Sy * Sxx) / (Sx * Sx - n * Sxx);
            a[1] = (Sx * Sy - n * Sxy) / (Sx * Sx - n * Sxx);


            q = 0.0;
            u = 0.0;
            p = 0.0;
            umax = 0.0;
            umin = 1.0e+30;
            for (i = 0; i <= n - 1; i++)
            {
                s = a[1] * x[i] + a[0];
                q = q + (y[i] - s) * (y[i] - s);
                p = p + (s - yy) * (s - yy);
                e = Math.Abs(y[i] - s);
                if (e > umax)
                    umax = e;
                if (e < umin)
                    umin = e;
                u = u + e / n;
            }
            dt[1] = Math.Sqrt(q / n);//平均标准偏差
            dt[0] = q;//偏差平方和
            dt[2] = p;//回归平方和
            dt[3] = umax;//最大偏差
            dt[4] = umin;//最小偏差
            dt[5] = u;//偏差平均值
        }

        /// <summary>
        /// 多元线性回归分析
        /// </summary>
        /// <param name="x">每一列存放m个自变量的观察值</param>
        /// <param name="y">存放随即变量y的n个观察值</param>
        /// <param name="m">自变量的个数</param>
        /// <param name="n"> 观察数据的组数</param>
        /// <param name="a">返回回归系数a0,...,am</param>
        /// <param name="dt"> dt[0]偏差平方和q,dt[1] 平均标准偏差s dt[2]返回复相关系数r dt[3]返回回归平方和u</param>
        /// <param name="v">返回m个自变量的偏相关系数</param>
        public static void sqt2(double[][] x, double[] y, int m, int n, double[] a, double[] dt, double[] v)
        {
            int i, j, k, mm;
            double q, e, u, p, yy, s, r, pp;
            double[] b = new double[(m + 1) * (m + 1)];
            mm = m + 1;
            b[mm * mm - 1] = n;
            for (j = 0; j <= m - 1; j++)
            {
                p = 0.0;
                for (i = 0; i <= n - 1; i++)
                    p = p + x[j][i];
                b[m * mm + j] = p;
                b[j * mm + m] = p;
            }
            for (i = 0; i <= m - 1; i++)
                for (j = i; j <= m - 1; j++)
                {
                    p = 0.0;
                    for (k = 0; k <= n - 1; k++)
                        p = p + x[i][k] * x[j][k];
                    b[j * mm + i] = p;
                    b[i * mm + j] = p;
                }
            a[m] = 0.0;
            for (i = 0; i <= n - 1; i++)
                a[m] = a[m] + y[i];
            for (i = 0; i <= m - 1; i++)
            {
                a[i] = 0.0;
                for (j = 0; j <= n - 1; j++)
                    a[i] = a[i] + x[i][j] * y[j];
            }
            chlk(b, mm, 1, a);
            yy = 0.0;
            for (i = 0; i <= n - 1; i++)
                yy = yy + y[i] / n;
            q = 0.0;
            e = 0.0;
            u = 0.0;
            for (i = 0; i <= n - 1; i++)
            {
                p = a[m];
                for (j = 0; j <= m - 1; j++)
                    p = p + a[j] * x[j][i];
                q = q + (y[i] - p) * (y[i] - p);
                e = e + (y[i] - yy) * (y[i] - yy);
                u = u + (yy - p) * (yy - p);
            }
            s = Math.Sqrt(q / n);
            r = Math.Sqrt(1.0 - q / e);
            for (j = 0; j <= m - 1; j++)
            {
                p = 0.0;
                for (i = 0; i <= n - 1; i++)
                {
                    pp = a[m];
                    for (k = 0; k <= m - 1; k++)
                        if (k != j)
                            pp = pp + a[k] * x[k][i];
                    p = p + (y[i] - pp) * (y[i] - pp);
                }
                v[j] = Math.Sqrt(1.0 - q / p);
            }
            dt[0] = q;
            dt[1] = s;
            dt[2] = r;
            dt[3] = u;
        }

        private static int chlk(double[] a, int n, int m, double[] d)
        {
            int i, j, k, u, v;
            if ((a[0] + 1.0 == 1.0) || (a[0] < 0.0))
            {
                return (-2);
            }
            a[0] = Math.Sqrt(a[0]);
            for (j = 1; j <= n - 1; j++)
                a[j] = a[j] / a[0];
            for (i = 1; i <= n - 1; i++)
            {
                u = i * n + i;
                for (j = 1; j <= i; j++)
                {
                    v = (j - 1) * n + i;
                    a[u] = a[u] - a[v] * a[v];
                }
                if ((a[u] + 1.0 == 1.0) || (a[u] < 0.0))
                {
                    return (-2);
                }
                a[u] = Math.Sqrt(a[u]);
                if (i != (n - 1))
                {
                    for (j = i + 1; j <= n - 1; j++)
                    {
                        v = i * n + j;
                        for (k = 1; k <= i; k++)
                            a[v] = a[v] - a[(k - 1) * n + i] * a[(k - 1) * n + j];
                        a[v] = a[v] / a[u];
                    }
                }
            }
            for (j = 0; j <= m - 1; j++)
            {
                d[j] = d[j] / a[0];
                for (i = 1; i <= n - 1; i++)
                {
                    u = i * n + i;
                    v = i * m + j;
                    for (k = 1; k <= i; k++)
                        d[v] = d[v] - a[(k - 1) * n + i] * d[(k - 1) * m + j];
                    d[v] = d[v] / a[u];
                }
            }
            for (j = 0; j <= m - 1; j++)
            {
                u = (n - 1) * m + j;
                d[u] = d[u] / a[n * n - 1];
                for (k = n - 1; k >= 1; k--)
                {
                    u = (k - 1) * m + j;
                    for (i = k; i <= n - 1; i++)
                    {
                        v = (k - 1) * n + i;
                        d[u] = d[u] - a[v] * d[i * m + j];
                    }
                    v = (k - 1) * n + k - 1;
                    d[u] = d[u] / a[v];
                }
            }
            return (2);
        }
    }
}
