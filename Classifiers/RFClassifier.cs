using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees;
using T40_klasyfikacja.Data;

namespace T40_klasyfikacja.Classifiers
{
    public class RFClassifier
    {
        public DataLoader Data;
        public int Folds;
        public string DataFile;
        public double[][][] TrainInput;
        public int[][] TrainOutput;
        public double[][][] TestInput;
        public int[][] TestOutput;

        public RFClassifier(int folds)
        {
            Folds = folds;
            DataFile = @"Data\data.csv";
            Data = new DataLoader(Folds, DataFile);

            TrainInput = new double[Folds][][];
            TrainOutput = new int[Folds][];
            TestInput = new double[Folds][][];
            TestOutput = new int[Folds][];
            for (int i = 0; i < Folds; i++)
            {
                TrainInput[i] = new double[Data.InstancesPerFold * (Folds - 1)][];
                TrainOutput[i] = new int[Data.InstancesPerFold * (Folds - 1)];
                TestInput[i] = new double[Data.InstancesPerFold][];
                TestOutput[i] = new int[Data.InstancesPerFold];
                for (int j = 0; j < Data.InstancesPerFold * (Folds - 1); j++)
                {
                    TrainInput[i][j] = new double[Data.FeatureCount];
                }
                for (int j = 0; j < Data.InstancesPerFold; j++)
                {
                    TestInput[i][j] = new double[Data.FeatureCount];
                }
            }

            for (int i = 0; i < folds; i++)
            {
                int trainCount = 0;
                for (int j = 0; j < folds; j++)
                {
                    for (int k = 0; k < Data.InstancesPerFold; k++)
                    {
                        if (i != j)
                        {
                            for (int x = 0; x < Data.FeatureCount; x++)
                            {
                                TrainInput[i][trainCount][x] = Data.Data[j, k, x];
                            }
                            TrainOutput[i][trainCount] = Data.Labels[j, k];
                            trainCount++;
                        }
                        else
                        {
                            for (int x = 0; x < Data.FeatureCount; x++)
                            {
                                TestInput[i][k][x] = Data.Data[j, k, x];
                            }
                            TestOutput[i][k] = Data.Labels[j, k];
                        }
                    }
                }
            }

        }

        public double CheckAccuracy(int trees, double ratio)
        {
            var variables = new DecisionVariable[Data.FeatureCount];
            for (int i=0; i<Data.FeatureCount; i++)
            {
                variables[i] = new DecisionVariable(i.ToString(), DecisionVariableKind.Continuous);
            }
            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 4;
            double accuracy = 0;
            for(int k=0; k<Folds; k++)
            {
                RandomForestLearning teacher = new RandomForestLearning(variables);
                //teacher.ParallelOptions = options;
                teacher.SampleRatio = ratio;
                teacher.NumberOfTrees = trees;
                teacher.
                var model = teacher.Learn(TrainInput[k], TrainOutput[k]);
                int correct = 0;
                for (int i = 0; i < Data.InstancesPerFold; i++)
                {
                    var label = model.Decide(TestInput[k][i]);
                    if (label == TestOutput[k][i])
                        correct++;
                }
                accuracy += (double)correct / Data.InstancesPerFold;
            }

            return accuracy;
        }

    }
}
