using System;
using System.Collections.Generic;
using System.Globalization;
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
            
            Console.WriteLine($"{DateTime.Now.DayOfWeek}");
            Console.WriteLine("---");
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            foreach (var item in dtfi.DayNames)
            {
                Console.WriteLine(item.Substring(0, 3));
            }
            Console.WriteLine("---");
            Console.WriteLine(dtfi.DayNames[0]);
            Console.WriteLine($"{DateTime.Now}");
            Console.WriteLine($"{DateTime.Now.AddDays(30).ToString("yyyy/MM/dd")}");
            Console.WriteLine("---");

            Console.WriteLine((int)Enum.Parse(typeof(DayOfWeek), "Monday"));

            // 특정 요일을 주어졌을 때, 해당 요일에 해당하는 날짜 구하기
            DateTime tmpDate = new DateTime(2023,8,28,3,3,4);
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(tmpDate.ToString("ddd HH:mm"));

            Console.ReadLine();
        }
    }
}
