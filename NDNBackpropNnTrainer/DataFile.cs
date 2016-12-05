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
            // 默认输出计数为 1
            //
            if (outputCount == 0) outputCount = 1;


            //
            // 如果inputCount为0，则解析文件以获取最后一列。
            // 还确定是否存在标题。
            //
            using (StreamReader sr = new StreamReader(path))
            {
                line = sr.ReadLine();
                cols = split.Split(line);
                if (!double.TryParse(cols[0], out dbl)) headingPresent = true;
                if (inputCount == 0) inputCount = cols.Length - 1;
            }


            //
            // 声明一个TrainingSet缓冲区
            //			
            TrainingSet ts = new TrainingSet(inputCount, outputCount);


            //
            // 循环内容并加载到TrainingSet中
            //
            double[] inputVector = new double[inputCount];
            double[] outputVector = new double[outputCount];
            using (StreamReader sr = new StreamReader(path))
            {
                // 如果存在，阅读标题
                if (headingPresent) line = sr.ReadLine();

                // Recurse并填充TrainingSet
                while ((line = sr.ReadLine()) != null)
                {
                    cols = split.Split(line);

                    // 检查我们解析了足够多的列
                    if (cols.Length < inputCount + outputCount)
                        throw new Exception("Input line has insufficient columns.");

                    // 将字符串列移到向量
                    for (int index = 0; index < inputCount; index++)
                        inputVector[index] = double.Parse(cols[index]);
                    for (int index = 0; index < outputCount; index++)
                        outputVector[index] = double.Parse(cols[inputCount + index]);

                    // 添加到训练集作为新的TrainingSample
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

                // 写标题
                for (int index = 1; index <= ts.InputVectorLength; index++)
                    sb.Append("Input" + index.ToString() + ",");
                for (int index = 1; index <= ts.OutputVectorLength; index++)
                    sb.Append("Output" + index.ToString() + ",");
                for (int index = 1; index <= ts.OutputVectorLength - 1; index++)
                    sb.Append("Forecast" + index.ToString() + ",");
                sb.Append("Forecast" + ts.OutputVectorLength.ToString());
                sw.WriteLine(sb.ToString());

                //  重复样品
                TrainingSample tsamp;
                for (int row = 0; row < ts.TrainingSampleCount; row++)
                {
                    tsamp = ts[row];
                    sb.Clear();

                    // 附加输入
                    for (int index = 0; index < ts.InputVectorLength; index++)
                        sb.Append(tsamp.InputVector[index].ToString("0.0000") + ",");
                    // 附加输出
                    for (int index = 0; index < ts.OutputVectorLength; index++)
                        sb.Append(tsamp.OutputVector[index].ToString("0.0000") + ",");

                    //
                    // 获取和追加预测
                    //

                    // 未标准化
                    forecastVector = network.Run(tsamp.InputVector);
                    // 标准化 - NOOOO
                    //forecastVector = network.Run(tsamp.NormalizedInputVector);

                    for (int index = 0; index < ts.OutputVectorLength - 1; index++)
                        sb.Append(forecastVector[index].ToString("0.0000") + ",");
                    sb.Append(forecastVector[ts.OutputVectorLength - 1].ToString("0.0000"));

                    // 输出这一行
                    sw.WriteLine(sb.ToString());
                }
            }

        }
    }
}
