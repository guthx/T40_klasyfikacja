using System;
using System.Collections.Generic;
using System.Globalization;
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
            var SVM = new SVMClassifier(10);
            Console.WriteLine(SVM.CheckAccuracy());
            Console.ReadKey();
        }
    }
}
