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

namespace NeuronDotNet.Core
{
    /// <summary>
    /// 此静态类包含此项目中使用的所有帮助函数。
    /// </summary>
    internal static class Helper
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// 验证值不为null。
        /// </summary>
        /// <param name="value">
        /// 要验证的值
        /// </param>
        /// <param name="name">
        /// 参数的名称
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果值为null
        /// </exception>
        internal static void ValidateNotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// 验证是否定义了枚举实例
        /// </summary>
        /// <param name="value">
        /// 要验证的值
        /// </param>
        /// <param name="enumType">
        /// 枚举类型
        /// </param>
        /// <param name="name">
        /// 枚举对象的名称
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果值未定义
        /// </exception>
        internal static void ValidateEnum(Type enumType, object value, string name)
        {
            if (!Enum.IsDefined(enumType, value))
            {
                throw new ArgumentException("The argument should be a valid enumerator", name);
            }
        }

        /// <summary>
        /// 验证数值参数不为负数
        /// </summary>
        /// <param name="value">
        /// 要验证的数值
        /// </param>
        /// <param name="name">
        /// 参数的名称
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果值为负数
        /// </exception>
        internal static void ValidateNotNegative(double value, string name)
        {
            if (value < 0)
            {
                throw new ArgumentException("The argument should be non-negative", name);
            }
        }

        /// <summary>
        /// 验证数值参数是正数
        /// </summary>
        /// <param name="value">
        /// 要验证的数值
        /// </param>
        /// <param name="name">
        /// 参数的名称
        /// </param>
        /// <exception cref="ArgumentException">
        /// 如果值为零或负值
        /// </exception>
        internal static void ValidatePositive(double value, string name)
        {
            if (value <= 0)
            {
                throw new ArgumentException("The argument should be non-zero positive", name);
            }
        }

        /// <summary>
        /// 验证数值参数是否在给定范围内
        /// </summary>
        /// <param name="value">
        /// 要验证的值
        /// </param>
        /// <param name="min">
        /// 最小可接受值
        /// </param>
        /// <param name="max">
        /// 最大可接受值
        /// </param>
        /// <param name="name">
        /// 参数的名称
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 如果值不在指定范围内
        /// </exception>
        internal static void ValidateWithinRange(double value, double min, double max, string name)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        /// <summary>
        /// 随机生成器。 返回0和1之间的随机双精度值
        /// </summary>
        /// <returns>
        /// 0和1之间的随机双精度
        /// </returns>
        internal static double GetRandom()
        {
            return random.NextDouble();
        }

        /// <summary>
        /// 随机生成器。 返回指定的最小值和最大值之间的随机双精度值
        /// </summary>
        /// <returns>
        /// 最小和最大之间的随机双精度
        /// </returns>
        internal static double GetRandom(double min, double max)
        {
            if (min > max)
            {
                return GetRandom(max, min);
            }
            return (min + (max - min) * random.NextDouble());
        }

        /// <summary>
        /// 生成包含从0到'size-1'的随机顺序的给定大小的数组
        /// </summary>
        /// <param name="size">
        /// 要生成的数组的大小。
        /// </param>
        /// <returns>
        /// 生成的数组。
        /// </returns>
        internal static int[] GetRandomOrder(int size)
        {
            int[] randomOrder = new int[size];

            //串联初始化数组
            for (int i = 0; i < size; i++)
            {
                randomOrder[i] = i;
            }

            //用所有位置i的随机元素交换第i个元素。
            for (int i = 0; i < size; i++)
            {
                int randomPosition = random.Next(size);
                int temp = randomOrder[i];
                randomOrder[i] = randomOrder[randomPosition];
                randomOrder[randomPosition] = temp;
            }
            return randomOrder;
        }

        /// <summary>
        /// 规范化双精度向量
        /// </summary>
        /// <param name="vector">
        /// 向量归一化。 此数组不由函数修改。
        /// </param>
        /// <returns>
        /// 标准化输出
        /// </returns>
        internal static double[] Normalize(double[] vector)
        {
            return Normalize(vector, 1d);
        }

        /// <summary>
        /// 规范化双精度向量
        /// </summary>
        /// <param name="vector">
        /// 向量归一化。 此数组不由函数修改。
        /// </param>
        /// <param name="magnitude">
        /// 大小
        /// </param>
        /// <returns>
        /// 标准化输出
        /// </returns>
        internal static double[] Normalize(double[] vector, double magnitude)
        {
            // 计算平方和的根
            double factor = 0d;
            for (int i = 0; i < vector.Length; i++)
            {
                factor += vector[i] * vector[i];
            }

            // 将每个值与平方和的根相除
            double[] normalizedVector = new double[vector.Length];
            if (factor != 0)
            {
                factor = Math.Sqrt(magnitude / factor);
                for (int i = 0; i < normalizedVector.Length; i++)
                {
                    normalizedVector[i] = vector[i] * factor;
                }
            }
            return normalizedVector;
        }

        /// <summary>
        /// 帮助获得随机正态值
        /// </summary>
        /// <param name="count">
        /// 要获取的值数
        /// </param>
        /// <param name="magnitude">
        /// 矢量的大小
        /// </param>
        /// <returns>
        /// 包含指定数量的标准化随机双精度数组
        /// </returns>
        internal static double[] GetRandomVector(int count, double magnitude)
        {
            double[] result = new double[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Helper.GetRandom();
            }
            return Normalize(result, magnitude);
        }
    }
}