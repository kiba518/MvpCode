using Accord.Controls;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using AForge.Neuro.Learning;
using System.IO;
using Accord.Neuro;
using Accord.Neuro.ActivationFunctions;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using Accord.Math;

namespace Accord.NETTest
{
    public class Test2
    {
        public static void Excute()
        {
            double[][] inputs;
            double[][] outputs;
            double[][] testInputs;
            double[][] testOutputs;

            // Load ascii digits dataset.
            inputs = DataManager.Load(@"data.txt", out outputs);

            // The first 500 data rows will be for training. The rest will be for testing. 第一个500数据用来训练，剩下的用来测试
            testInputs = inputs.Skip(500).ToArray();
            testOutputs = outputs.Skip(500).ToArray();
            inputs = inputs.Take(500).ToArray();
            outputs = outputs.Take(500).ToArray();

            // Setup the deep belief network and initialize with random weights.
            DeepBeliefNetwork network = new DeepBeliefNetwork(inputs.First().Length, 10, 10);
            new GaussianWeights(network, 0.1).Randomize();
            network.UpdateVisibleWeights();

            // Setup the learning algorithm.
            DeepBeliefNetworkLearning teacher = new DeepBeliefNetworkLearning(network)
            {
                Algorithm = (h, v, i) => new ContrastiveDivergenceLearning(h, v)
                {
                    LearningRate = 0.1,
                    Momentum = 0.5,
                    Decay = 0.001,
                }
            };

            // Setup batches of input for learning.
            int batchCount = System.Math.Max(1, inputs.Length / 100);
            // Create mini-batches to speed learning.
            int[] groups = Accord.Statistics.Tools.RandomGroups(inputs.Length, batchCount);
            double[][][] batches = inputs.Subgroups(groups);
            // Learning data for the specified layer.
            double[][][] layerData;

            // Unsupervised learning on each hidden layer, except for the output layer.
            for (int layerIndex = 0; layerIndex < network.Machines.Count - 1; layerIndex++)
            {
                teacher.LayerIndex = layerIndex;
                layerData = teacher.GetLayerInput(batches);
                for (int i = 0; i < 200; i++)
                {
                    double error = teacher.RunEpoch(layerData) / inputs.Length;
                    if (i % 10 == 0)
                    {
                        Console.WriteLine(i + ", Error = " + error);
                    }
                }
            }

            // Supervised learning on entire network, to provide output classification.
            var teacher2 = new Neuro.Learning.BackPropagationLearning(network)
            {
                LearningRate = 0.1,
                Momentum = 0.5
            };

            // Run supervised learning.
            for (int i = 0; i < 500; i++)
            {
                double error = teacher2.RunEpoch(inputs, outputs) / inputs.Length;
                if (i % 10 == 0)
                {
                    Console.WriteLine(i + ", Error = " + error);
                }
            }

            // Test the resulting accuracy.
            int correct = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] outputValues = network.Compute(testInputs[i]);
                if (DataManager.FormatOutputResult(outputValues) == DataManager.FormatOutputResult(testOutputs[i]))
                {
                    correct++;
                }
            }

            Console.WriteLine("Correct " + correct + "/" + inputs.Length + ", " + System.Math.Round(((double)correct / (double)inputs.Length * 100), 2) + "%");
            Console.Write("Press any key to quit ..");
            Console.ReadKey();
        }

        public static void Excute2()
        {
            double[][] inputs;
            double[][] outputs;
            double[][] testInputs;
            double[][] testOutputs;

            // Load ascii digits dataset.
            inputs = DataManager.Load(@"data.txt", out outputs);

            // The first 500 data rows will be for training. The rest will be for testing. 第一个500数据用来训练，剩下的用来测试
            testInputs = inputs.Skip(500).ToArray();
            testOutputs = outputs.Skip(500).ToArray();
            inputs = inputs.Take(500).ToArray();
            outputs = outputs.Take(500).ToArray();

            // Setup the deep belief network and initialize with random weights. 设置深度神经网络和初始化随机砝码
            DeepBeliefNetwork network = new DeepBeliefNetwork(inputs.First().Length, 10, 10);//网络的输入数量Length  每个层中隐藏的神经元的数量10, 10
            new GaussianWeights(network, 0.1).Randomize();//高斯砝码 使用标准偏差。一般值在0.001—0.1范围内。 默认值为0.1。 
            //Randomize 使用高斯分布的网络的权重
            network.UpdateVisibleWeights();//通过复制隐藏层中权重的反向来更新可见层的权重。

            // Setup the learning algorithm. 设置学习法则
            DeepBeliefNetworkLearning teacher = new DeepBeliefNetworkLearning(network);
            //自定义神经网络法则
            // 设置用于指定和创建深度网络的每个层的学习算法的配置函数。Algorithm
            teacher.Algorithm = (h, v, i) => {
                return new ContrastiveDivergenceLearning(h, v)
                {
                    LearningRate = 0.1,//学习速率
                    Momentum = 0.5,//动力
                    Decay = 0.001,//腐烂
                };
            };

            //teacher.Algorithm = (h, v, i) => new ContrastiveDivergenceLearning(h, v)
            //{
            //    LearningRate = 0.1,
            //    Momentum = 0.5,
            //    Decay = 0.001,
            //};
            // Setup batches of input for learning.
            int batchCount = System.Math.Max(1, inputs.Length / 100);//设置学习次数
            // Create mini-batches to speed learning.
            int[] groups = Accord.Statistics.Classes.Random(inputs.Length, batchCount);//创建小批量 速度学习
            double[][][] batches = inputs.Separate(groups);//分离
            // Learning data for the specified layer.
            double[][][] layerData;//为指定层 学习数据

            // Unsupervised learning on each hidden layer, except for the output layer.除了输出层之外，在每个隐藏层上进行无监督学习。
            //network.Machines.Count 在这个深网络的每一层上得到受限制的玻尔兹曼机器。
            for (int layerIndex = 0; layerIndex < network.Machines.Count - 1; layerIndex++)
            {
                teacher.LayerIndex = layerIndex;
                /*
                 获取训练数据所需的学习数据。
                这个函数的返回应该被传递给No.Posial.SurvivyFieldWorksPr.RunEpoch（System，Pouth[2][]）。
                去实践一个学习时代。
                 */
                layerData = teacher.GetLayerInput(batches);
                for (int i = 0; i < 200; i++)//200次学习
                {
                    var learningResult = teacher.RunEpoch(layerData);
                    double error = learningResult / inputs.Length;//RunEpoch运行纪元  Returns sum of learning errors.
                    if (i % 10 == 0)
                    {
                        Console.WriteLine(i + ", Error = " + error);
                    }
                }
            }

            // Supervised learning on entire network, to provide output classification.对整个网络进行监督学习，提供输出分类。
            var teacher2 = new Neuro.Learning.BackPropagationLearning(network)
            {
                LearningRate = 0.1,//学习速率
                Momentum = 0.5//动力
            };

            // Run supervised learning.运行监督学习。
            for (int i = 0; i < 500; i++)//500次学习
            {
                double error = teacher2.RunEpoch(inputs, outputs) / inputs.Length;
                if (i % 10 == 0)
                {
                    Console.WriteLine(i + ", Error = " + error);
                }
            }

            // Test the resulting accuracy. 测试结果的准确性
            int correct = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                var cp = testInputs[i].ToList();
                var cpn = testInputs[i+1];
                foreach(var item in cpn)
                {
                    cp.Add(item);
                }
                double[] outputValues = network.Compute(cp.ToArray());

                //double[] outputValues = network.Compute(testInputs[i]);
                if (DataManager.FormatOutputResult(outputValues) == DataManager.FormatOutputResult(testOutputs[i]))
                {
                    correct++;
                }
            }

            Console.WriteLine("Correct " + correct + "/" + inputs.Length + ", " + System.Math.Round(((double)correct / (double)inputs.Length * 100), 2) + "%");
            Console.Write("Press any key to quit ..");
            Console.ReadKey();
        }
    }
}
