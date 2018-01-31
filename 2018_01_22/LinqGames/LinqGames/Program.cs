using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqGames
{
    class Program
    {
        static void Main(string[] args)
        {
            //var months = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames;
            var months = GetMonthsLazy();

            var monthsNotR = months.Where(m => !m.EndsWith("r"));

            var sortedMonths = monthsNotR.OrderBy(m => m);

            var list = sortedMonths.ToList();
        }

        private static IEnumerable<string> GetMonthsLikeBefore()
        {
            return new List<string>
            {
               "Januar"        ,
               "Februar"       ,
               "März"          ,
               "April"         ,
               "Mai"           ,
               "Juni"          ,
               "Juli"          ,
               "August"        ,
               "September"     ,
               "Oktober"       ,
                "November"     ,
                "Dezember"
            };
        }

        private static IEnumerable<string> GetMonthsLazy()
        {
            yield return "Januar";
            yield return "Februar";
            yield return "März";
            if (DateTime.Now.Month < 4) yield break;
            yield return "April";
            yield return "Mai";
            yield return "Juni";
            if (DateTime.Now.Month < 7) yield break;
            yield return "Juli";
            yield return "August";
            yield return "September";
            yield return "Oktober";
            yield return "November";
            yield return "Dezember";
        }
    }
}
