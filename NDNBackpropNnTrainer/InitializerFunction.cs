using System;

namespace BackPropNnTrainer
{
    /// <summary>
    /// 初始化函数
    /// </summary>
	public enum InitializerFunction
    {
        /// <summary>
        /// 常数
        /// </summary>
		Constant,

        /// <summary>
        /// Nguyen-Widrow函数
        /// </summary>
        NguyenWidrow,

        /// <summary>
        /// 正态分布
        /// </summary>
        NormRand,

        /// <summary>
        /// 随机数
        /// </summary>
        Random,

        /// <summary>
        /// 常数零
        /// </summary>
        Zero
    }
}
