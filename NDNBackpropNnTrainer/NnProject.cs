using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;

namespace BackPropNnTrainer
{
    public class NnProject
    {
        public string ProjectName
        {
            get;
            set;
        }
        public string SaveFolder
        {
            get;
            set;
        }

        public BackpropagationNetwork Network
        {
            get;
            set;
        }

        public TrainingSet TrainingSet { get; set; }
        public TrainingSet CrossValidationSet { get; set; }

        public NnProject.NnLearningParameters LearningParameters { get; set; }

        public static void Save(NnProject nnProject, string saveFolder)
        {
            // 使用默认或传递的文件夹？
            if (saveFolder.Length > 0) nnProject.SaveFolder = saveFolder;

            // 保存网络
            string path = System.IO.Path.Combine(nnProject.SaveFolder, nnProject.ProjectName + ".ndn");
            SaveNeuralNetwork(nnProject.Network, path);

            // 保存数据集
            path = System.IO.Path.Combine(nnProject.SaveFolder, nnProject.ProjectName + ".trn");
            SaveTrainingSet(nnProject.TrainingSet, path);
            if (nnProject.CrossValidationSet != null)
            {
                path = System.IO.Path.Combine(nnProject.SaveFolder, nnProject.ProjectName + ".crv");
                SaveTrainingSet(nnProject.CrossValidationSet, path);
            }

            // 保存学习参数
            path = System.IO.Path.Combine(nnProject.SaveFolder, nnProject.ProjectName + ".xml");
            NnLearningParameters.Serialize(path, nnProject.LearningParameters);

        }
        public static NnProject Load(string path)
        {
            NnProject nnProject = new NnProject();
            nnProject.SaveFolder = System.IO.Path.GetDirectoryName(path);
            nnProject.ProjectName = System.IO.Path.GetFileName(path);
            nnProject.ProjectName = nnProject.ProjectName.Remove(nnProject.ProjectName.IndexOf('.'));

            // 加载网络
            string path2 = System.IO.Path.Combine(nnProject.SaveFolder, nnProject.ProjectName + ".ndn");
            nnProject.Network = (BackpropagationNetwork)LoadNeuralNetwork(path2);

            // 加载数据集
            path2 = System.IO.Path.Combine(nnProject.SaveFolder, nnProject.ProjectName + ".trn");
            nnProject.TrainingSet = LoadTrainingSet(path2);
            path2 = System.IO.Path.Combine(nnProject.SaveFolder, nnProject.ProjectName + ".crv");
            if (File.Exists(path2))
            {
                nnProject.CrossValidationSet = LoadTrainingSet(path2);
            }

            // 保存学习参数
            path2 = System.IO.Path.Combine(nnProject.SaveFolder, nnProject.ProjectName + ".xml");
            nnProject.LearningParameters = NnLearningParameters.Deserialize(path2);

            return nnProject;
        }

        public static INetwork LoadNeuralNetwork(string path)
        {
            INetwork network;
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                IFormatter formatter = new BinaryFormatter();
                network = (INetwork)formatter.Deserialize(stream);
            }
            return network;
        }
        public static void SaveNeuralNetwork(INetwork network, string path)
        {
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, network);
            }
        }

        public static TrainingSet LoadTrainingSet(string path)
        {
            TrainingSet ts;
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                IFormatter formatter = new BinaryFormatter();
                ts = (TrainingSet)formatter.Deserialize(stream);
            }
            return ts;
        }
        public static void SaveTrainingSet(TrainingSet ts, string path)
        {
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, ts);
            }
        }

        public class NnLearningParameters
        {
            // 序列化
            public static NnLearningParameters Deserialize(string path)
            {
                NnProject.NnLearningParameters pms;
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(NnProject.NnLearningParameters));
                using (StreamReader sr = new StreamReader(path))
                {
                    pms = (NnProject.NnLearningParameters)xs.Deserialize(sr);
                }
                return pms;
            }
            public static void Serialize(string path, NnLearningParameters pms)
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(NnProject.NnLearningParameters));
                using (StreamWriter sw = new StreamWriter(path))
                {
                    xs.Serialize(sw, pms);
                }
            }

            public double InitialLearningRate
            {
                get;
                set;
            }
            public double? FinalLearningRate
            {
                get;
                set;
            }
            public double? Momentum { get; set; }
            public LearningRateFunction? LearningRateFunction
            {
                get;
                set;
            }
            public int MaxTrainingCycles
            {
                get;
                set;
            }
        }
    }
}
