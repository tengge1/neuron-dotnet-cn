using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.LearningRateFunctions;

namespace BackPropNnTrainer
{
    /// <summary>
    /// 学习率工厂
    /// </summary>
	public static class LearningRateFactory
    {
        /// <summary>
        /// 获取学习率函数
        /// </summary>
        /// <param name="lrf"></param>
        /// <param name="initialLR"></param>
        /// <param name="finalLR"></param>
        /// <returns></returns>
		public static ILearningRateFunction GetLearningRateFunction(LearningRateFunction lrf, double initialLR, double finalLR)
        {
            switch (lrf)
            {
                case LearningRateFunction.Exponential: // 指数型
                    return new ExponentialFunction(initialLR, finalLR);
                case LearningRateFunction.Hyperbolic: // 双曲线
                    return new HyperbolicFunction(initialLR, finalLR);
                case LearningRateFunction.Linear: // 线性
                    return new LinearFunction(initialLR, finalLR);
                default:
                    throw new Exception("Learning Rate Function not handled.");
            }
        }
    }
}
