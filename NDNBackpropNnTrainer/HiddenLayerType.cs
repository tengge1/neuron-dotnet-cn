using System;

namespace BackPropNnTrainer
{
    /// <summary>
    /// 隐层类型
    /// </summary>
	public enum HiddenLayerType
    {
        /// <summary>
        /// 线性
        /// </summary>
		Linear,

        /// <summary>
        /// 对数
        /// </summary>
		Logarithmic,

        /// <summary>
        /// Sigmoid函数
        /// </summary>
		Sigmoid,

        /// <summary>
        /// 正弦函数
        /// </summary>
		Sine,

        /// <summary>
        /// 双曲正切函数
        /// </summary>
		Tanh
    }
}
