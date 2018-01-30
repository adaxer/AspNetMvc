using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace NorthwindUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            CorrectDates();
            SaveImages();
        }

        private static void SaveImages()
        {
            using (var ctx=new NWContext())
            {
                foreach (var emp in ctx.Employees)
                {
                    byte[] buidl = emp.Photo;
                    string fname = string.Format("Emp{0}.bmp", emp.EmployeeId);
                    File.WriteAllBytes(fname, buidl.Skip(78).ToArray());
                }
                foreach (var cat in ctx.Categories)
                {
                    byte[] buidl = cat.Picture;
                    string fname = string.Format("Cat{0}.bmp", cat.CategoryId);
                    File.WriteAllBytes(fname, buidl.Skip(78).ToArray());
                }
            }
        }

        private static void CorrectDates()
        {
            var nw = File.ReadAllText("instnwnd.sql");
            string datePattern = @"\d{1,2}/\d{1,2}/\d{4}";

            var dates = Regex
                .Matches(nw, datePattern)
                .Cast<Match>()
                .Select(m => DateTime.Parse(m.Value, new CultureInfo("en-US")));
            var oldest = dates.Min();
            var youngest = dates.Max();
            adaptDays = (DateTime.Today - youngest).Days;


            var rm = Regex.Replace(nw, datePattern, AdaptDate);
            File.WriteAllText("instnwnd2014.sql", rm);
        }

        static int adaptDays = 0;

        static string AdaptDate(Match m)
        {
            var oldDate = DateTime.Parse(m.Value, new CultureInfo("en-US"));
            var newDate = oldDate.AddDays(adaptDays).ToString(@"MM/dd/yyyy").Replace(".", "/");

            Console.WriteLine("Converting Date {0} to {1}", m.Value, newDate);
            return newDate;
        }
    }
}
