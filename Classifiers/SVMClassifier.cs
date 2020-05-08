using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibSVMsharp;
using LibSVMsharp.Extensions;
using Microsoft.VisualBasic.FileIO;
using T40_klasyfikacja.Data;

namespace T40_klasyfikacja.Classifiers
{
    public class SVMClassifier
    {
        public string DataFile;
        public DataLoader Data;
        private SVMProblem[] TrainProblems;
        private SVMProblem[] TestProblems;
        public int Folds;
        public SVMParameter Parameters;

        public SVMClassifier(int folds = 5)
        {
            Folds = folds;
            DataFile = @"Data\data.csv";
            Data = new DataLoader(Folds, DataFile);
            TrainProblems = new SVMProblem[folds];
            TestProblems = new SVMProblem[folds];

            for(int i=0; i<folds; i++)
            {
                TrainProblems[i] = new SVMProblem();
                TestProblems[i] = new SVMProblem();
            }

            for(int i=0; i<folds; i++)
            {
                for(int j=0; j<folds; j++)
                {
                    for(int k=0; k<Data.InstancesPerFold; k++)
                    {
                        var nodes = new SVMNode[Data.FeatureCount];
                        var label = (double)Data.Labels[j, k];
                        for(int x=0; x<Data.FeatureCount; x++)
                        {
                            nodes[x] = new SVMNode(x + 1, Data.Data[j, k, x]);
                        }
                        if(i != j)
                        {
                            TrainProblems[i].Add(nodes, label);
                        }
                        else
                        {
                            TestProblems[i].Add(nodes, label);
                        }
                    }
                }
            }
            Parameters = new SVMParameter();
            Parameters.Type = SVMType.C_SVC;
            Parameters.Kernel = SVMKernelType.RBF;
            Parameters.C = 1000000;
            Parameters.Degree = 3;
            Parameters.Coef0 = 0;
            Parameters.Gamma = 0.001;

        }

        public double CheckAccuracy()
        {
            double accuracy = 0;
            for (int i=0; i<Folds; i++)
            {
                var model = TrainProblems[i].Train(Parameters);
                var target = TestProblems[i].Predict(model);
                accuracy += TestProblems[i].EvaluateClassificationProblem(target);
            }
            return accuracy / Folds;
        }
    }
}
