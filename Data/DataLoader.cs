using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T40_klasyfikacja.Data
{
    public class DataLoader
    {
        public double[,,] Data;
        public int[,] Labels;
        public int FeatureCount;
        public int InstanceCount;
        public int InstancesPerFold;
        public int FoldCount;

        public DataLoader(int folds, string dataPath = @"Data\data.csv")
        {
            FoldCount = folds;
            int k = 0;
            int lineCount = File.ReadLines(dataPath).Count();
            var csvParser = new TextFieldParser(dataPath);
            csvParser.SetDelimiters(new string[] { "," });
            int featureCount = csvParser.ReadFields().Length - 1;
            FeatureCount = featureCount;
            InstanceCount = lineCount - 1;
            InstancesPerFold = InstanceCount / folds;
            Data = new double[folds, (lineCount - 1) / folds, featureCount];
            Labels = new int[folds, (lineCount - 1) / folds];
            int i = 0;
            while (!csvParser.EndOfData)
            {
                string[] fields = csvParser.ReadFields();
                Labels[k, i] = int.Parse(fields[0]);
                for (int j = 1; j < fields.Length; j++)
                {
                    Data[k, i, j - 1] = double.Parse(fields[j]);
                }
                k++;
                if (k >= folds)
                {
                    k = 0;
                    i++;
                }
            }
        }

    }
}
