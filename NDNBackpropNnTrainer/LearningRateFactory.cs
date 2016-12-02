using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.LearningRateFunctions;

namespace BackPropNnTrainer
{
	public static class LearningRateFactory
	{
		public static ILearningRateFunction GetLearningRateFunction(LearningRateFunction lrf, double initialLR, double finalLR)
		{
			switch (lrf)
			{
				case LearningRateFunction.Exponential:
					return new ExponentialFunction(initialLR, finalLR);
				case LearningRateFunction.Hyperbolic:
					return new HyperbolicFunction(initialLR, finalLR);
				case LearningRateFunction.Linear:
					return new LinearFunction(initialLR, finalLR);
				default:
					throw new Exception("Learning Rate Function not handled.");
			}
		}
	}
}
