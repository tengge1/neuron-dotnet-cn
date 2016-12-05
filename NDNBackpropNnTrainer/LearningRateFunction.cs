using System;

namespace BackPropNnTrainer
{
    /// <summary>
    /// 学习率函数
    /// </summary>
	public enum LearningRateFunction
    {
        /// <summary>
        /// 指数
        /// </summary>
		Exponential,

        /// <summary>
        /// 双曲线
        /// </summary>
		Hyperbolic,

        /// <summary>
        /// 线性
        /// </summary>
		Linear
    }
}
