using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelRead
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = System.IO.Directory.GetCurrentDirectory() + "/Sheets/Guidelines.xlsx";
            var a = new ExcelReader(@path);
            path = "C:/Dev/BrewFreeFork/src/Brewfree/Data/SeedData.cs";

            Console.WriteLine("Writing to file...");
            CSharpWriter.Instance.WriteStyles(@path, a);
            Console.WriteLine("Done.");

            a.Close();
            Console.ReadKey();
        }
    }
}
