using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProject._02._Time
{
    internal class TimeMain
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy MM dd"+"/"+"dd")}");
            Console.WriteLine($"{DateTime.Now.ToString("//")}");
        }
    }
}
