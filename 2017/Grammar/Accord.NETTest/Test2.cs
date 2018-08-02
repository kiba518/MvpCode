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
                    LearningRate = 0.1,
                    Momentum = 0.5,
                    Decay = 0.001,
                };
            };

            //teacher.Algorithm = (h, v, i) => new ContrastiveDivergenceLearning(h, v)
            //{
            //    LearningRate = 0.1,
            //    Momentum = 0.5,
            //    Decay = 0.001,
            //};
            // Setup batches of input for learning.
            int batchCount = System.Math.Max(1, inputs.Length / 100);
            // Create mini-batches to speed learning.
            int[] groups = Accord.Statistics.Classes.Random(inputs.Length, batchCount);
            double[][][] batches = inputs.Separate(groups);
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
    }
}
