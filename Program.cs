using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T40_klasyfikacja.Classifiers;

namespace T40_klasyfikacja
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            /*
            var SVM = new SVMClassifier(10);
            var CGrid = new double[5] { 1000, 5000, 10000, 50000, 100000 };
            var GammaGrid = new double[5] { 0.001, 0.0005, 0.0001, 0.00005, 0.00001 }; 
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
            
            var results = new StreamWriter(@"Data\results2.csv");
            var times = new StreamWriter(@"Data\times2.csv");
            for(int i=0; i<CGrid.Length; i++)
            {
                var line = new StringBuilder();
                var line2 = new StringBuilder();
                SVM.Parameters.C = CGrid[i];
                for(int j=0; j<CGrid.Length; j++)
                {
                    SVM.Parameters.Gamma = GammaGrid[j];
                    var timer = new Stopwatch();
                    timer.Start();
                    double accuracy = SVM.CheckAccuracy();
                    timer.Stop();
                    line2.Append(timer.Elapsed.TotalSeconds.ToString() + ';');
                    line.Append(accuracy.ToString() + ';');
                }
                results.WriteLine(line.ToString().Trim(';'));
                times.WriteLine(line2.ToString().Trim(';'));
            }
            results.Close();
            times.Close();
            //Console.WriteLine(SVM.CheckAccuracy());
            */

            /*
            var ANN = new ANNClassifier(5);
            var lrGrid = new double[5] { 0.05, 0.1, 0.15, 0.2, 0.25 };
            var mGrid = new double[5] { 0.05, 0.1, 0.15, 0.2, 0.25 };
            var neuronGrid = new int[5] { 50, 75, 100, 125, 150 };
            */

            /*
            for (int i=0; i<lrGrid.Length; i++)
            {
                var line = new StringBuilder();
                var line2 = new StringBuilder();
                for(int j=0; j<mGrid.Length; j++)
                {
                    var timer = new Stopwatch();
                    timer.Start();
                    var accuracy = ANN.CheckAccuracy(lrGrid[i], mGrid[j]);
                    timer.Stop();
                    line.Append(accuracy.ToString() + ';');
                    line2.Append(timer.Elapsed.TotalSeconds.ToString() + ';');
                }
                results.WriteLine(line.ToString().Trim(';'));
                times.WriteLine(line2.ToString().Trim(';'));
            }
            */
            /*
            var line = new StringBuilder();
            var line2 = new StringBuilder();
            for (int i=0; i<neuronGrid.Length; i++)
            {
                var timer = new Stopwatch();
                timer.Start();
                var accuracy = ANN.CheckAccuracy(0.1, 0.1, neuronGrid[i]);
                timer.Stop();
                line.Append(accuracy.ToString() + ';');
                line2.Append(timer.Elapsed.TotalSeconds.ToString() + ';');
                Console.WriteLine($"Iteration {i} finished and took {timer.Elapsed.TotalMinutes} minutes.");
                Console.WriteLine($"Estimated time left: {timer.Elapsed.TotalMinutes * (neuronGrid.Length - (i + 1))}");
            }
            results.WriteLine(line.ToString().Trim(';'));
            times.WriteLine(line2.ToString().Trim(';'));
            results.Close();
            times.Close();
            */
            /*
            var results = new StreamWriter(@"Data\RFresults.csv");
            var times = new StreamWriter(@"Data\RFtimes.csv");
            var RF = new RFClassifier(5);
            var treeGrid = new int[] { 50, 75, 100, 125, 150 };
            var ratioGrid = new double[] { 0.5, 0.55, 0.6, 0.65, 0.7 };
            for(int i=0; i<treeGrid.Length; i++)
            {
                var line = new StringBuilder();
                var line2 = new StringBuilder();
                for(int j=0; j<ratioGrid.Length; j++)
                {
                    var timer = new Stopwatch();
                    timer.Start();
                    var accuracy = RF.CheckAccuracy(treeGrid[i], ratioGrid[j]);
                    timer.Stop();
                    line.Append(accuracy.ToString() + ';');
                    line2.Append(timer.Elapsed.TotalSeconds.ToString() + ';');
                    Console.WriteLine($"Iteration {i} finished and took {timer.Elapsed.TotalMinutes} minutes.");
                    Console.WriteLine($"Estimated time left: {timer.Elapsed.TotalMinutes * (treeGrid.Length - (i + 1))}");
                }
                results.WriteLine(line.ToString().Trim(';'));
                times.WriteLine(line2.ToString().Trim(';'));
            }
            
            results.Close();
            times.Close();
            */
            
            Console.ReadKey();
        
        }
    }
}
