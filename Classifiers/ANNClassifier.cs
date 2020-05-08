using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Neuro;
using AForge.Neuro.Learning;
using T40_klasyfikacja.Data;

namespace T40_klasyfikacja.Classifiers
{
    public class ANNClassifier
    {
        public string DataFile;
        public DataLoader Data;
        public int Folds;
        public double[][][] TrainInput;
        public double[][][] TrainOutput;
        public double[][][] TestInput;
        public double[][][] TestOutput;
        public ANNClassifier(int folds)
        {
            Folds = folds;
            DataFile = @"Data\data.csv";
            Data = new DataLoader(Folds, DataFile);
            TrainInput = new double[Folds][][];
            TrainOutput = new double[Folds][][];
            TestInput = new double[Folds][][];
            TestOutput = new double[Folds][][];
            for(int i=0; i<Folds; i++)
            {
                TrainInput[i] = new double[Data.InstancesPerFold*(Folds-1)][];
                TrainOutput[i] = new double[Data.InstancesPerFold*(Folds-1)][];
                TestInput[i] = new double[Data.InstancesPerFold][];
                TestOutput[i] = new double[Data.InstancesPerFold][];
                for(int j=0; j<Data.InstancesPerFold*(Folds-1); j++)
                {
                    TrainInput[i][j] = new double[Data.FeatureCount];
                    TrainOutput[i][j] = new double[1];
                }
                for(int j=0; j<Data.InstancesPerFold; j++)
                {
                    TestInput[i][j] = new double[Data.FeatureCount];
                    TestOutput[i][j] = new double[1];
                }
            }

            for (int i = 0; i < folds; i++)
            {
                int trainCount = 0;
                for (int j = 0; j < folds; j++)
                {
                    for (int k = 0; k < Data.InstancesPerFold; k++)
                    {
                        if(i != j)
                        {
                            for(int x = 0; x < Data.FeatureCount; x++)
                            {
                                TrainInput[i][trainCount][x] = Data.Data[j, k, x];
                            }
                            TrainOutput[i][trainCount][0] = Data.Labels[j, k];
                            trainCount++;
                        }
                        else
                        {
                            for(int x=0; x<Data.FeatureCount; x++)
                            {
                                TestInput[i][k][x] = Data.Data[j, k, x];
                            }
                            TestOutput[i][k][0] = Data.Labels[j, k];
                        }
                    }
                }
            }

        }

        public double CheckAccuracy(double lr, double m, int neurons)
        {
            double accuracy = 0;
            for(int k = 0; k < Folds; k++)
            {
                var network = new ActivationNetwork(new SigmoidFunction(), Data.FeatureCount, new int[] { neurons, 1 });
                var teacher = new BackPropagationLearning(network);
                teacher.LearningRate = lr;
                teacher.Momentum = m;
                var needToStop = false;
                int epochCount = 0;
                while (!needToStop)
                {
                    double error = teacher.RunEpoch(TrainInput[k], TrainOutput[k]);
                    epochCount++;
                    if (epochCount > 5000 || error < 0.01)
                        needToStop = true;
                }
                int correct = 0;
                for (int i = 0; i < TestInput[k].Length; i++)
                {
                    var label = Math.Round(network.Compute(TestInput[k][i])[0]);
                    if (label == (TestOutput[k][i][0]))
                    {
                        correct++;
                    }
                }
                accuracy += (double)correct / (TestInput[k].Length);
            }
            
            return accuracy / Folds;
            

        }
    }
}
