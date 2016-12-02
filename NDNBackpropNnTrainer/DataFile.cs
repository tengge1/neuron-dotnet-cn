using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.LearningRateFunctions;

namespace BackPropNnTrainer
{
	public class DataFile
	{
		public static TrainingSet CsvFileToTrainingSet(string path, ref int inputCount, ref int outputCount)
		{
			Regex split = new Regex(",");
			string line;
			string[] cols;
			bool headingPresent = false;
			double dbl;


			//
			// Default outputCount to 1
			//
			if (outputCount == 0) outputCount = 1;


			//
			// If inputCount is 0 then parse file to get last column.
			// Also determine if heading is present.
			//
			using (StreamReader sr = new StreamReader(path))
			{
				line = sr.ReadLine();
				cols = split.Split(line);
				if (!double.TryParse(cols[0], out dbl)) headingPresent = true;
				if (inputCount == 0) inputCount = cols.Length - 1;
			}


			//
			// Declare a TrainingSet buffer
			//			
			TrainingSet ts = new TrainingSet(inputCount, outputCount);


			//
			// Recurse the contents and load into the TrainingSet
			//
			double[] inputVector = new double[inputCount];
			double[] outputVector = new double[outputCount];
			using (StreamReader sr = new StreamReader(path))
			{
				// Read heading out if present
				if (headingPresent) line = sr.ReadLine();

				// Recurse and fill TrainingSet
				while ((line = sr.ReadLine()) != null)
				{
					cols = split.Split(line);

					// Check we parsed enough columns
					if (cols.Length < inputCount + outputCount)
						throw new Exception("Input line has insufficient columns.");

					// Move string columns to vectors
					for (int index = 0; index < inputCount; index++)
						inputVector[index] = double.Parse(cols[index]);
					for (int index = 0; index < outputCount; index++)
						outputVector[index] = double.Parse(cols[inputCount + index]);

					// Add to training set as new TrainingSample
					ts.Add(new TrainingSample(inputVector, outputVector));
				}
			}

			return ts;
		}

		public static void SaveTrainingSet(TrainingSet ts, string path, BackpropagationNetwork network)
		{
			StringBuilder sb = new StringBuilder();

			using (StreamWriter sw = new StreamWriter(path))
			{
				double[] forecastVector;

				// Write the heading
				for (int index = 1; index <= ts.InputVectorLength; index++)
					sb.Append("Input" + index.ToString() + ",");
				for (int index = 1; index <= ts.OutputVectorLength; index++)
					sb.Append("Output" + index.ToString() + ",");
				for (int index = 1; index <= ts.OutputVectorLength - 1; index++)
					sb.Append("Forecast" + index.ToString() + ",");
				sb.Append("Forecast" + ts.OutputVectorLength.ToString());
				sw.WriteLine(sb.ToString());

				//  Recurse the samples
				TrainingSample tsamp;
				for (int row = 0; row < ts.TrainingSampleCount; row++)
				{
					tsamp = ts[row];
					sb.Clear();

					// Append the inputs
					for (int index = 0; index < ts.InputVectorLength; index++)
						sb.Append(tsamp.InputVector[index].ToString("0.0000") + ",");
					// Append the outputs
					for (int index = 0; index < ts.OutputVectorLength; index++)
						sb.Append(tsamp.OutputVector[index].ToString("0.0000") + ",");

					//
					// Get and append the forecast
					//

					// Not Normalized
					forecastVector = network.Run(tsamp.InputVector);
					// Normalized - NOOOO
					//forecastVector = network.Run(tsamp.NormalizedInputVector);

					for (int index = 0; index < ts.OutputVectorLength - 1; index++)
						sb.Append(forecastVector[index].ToString("0.0000") + ",");
					sb.Append(forecastVector[ts.OutputVectorLength - 1].ToString("0.0000"));

					// Write this line
					sw.WriteLine(sb.ToString());
				}
			}

		}
	}
}
