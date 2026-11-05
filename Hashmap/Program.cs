using System.Diagnostics;
using System.Globalization;

namespace DateTimeFormating {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            DateTimeExtension timeExtension = new();

            while (true) {
                Console.WriteLine("Input a date");
                string? input = Console.ReadLine();

                string[] splitString = input.Split('-',' ', '.');
                Console.WriteLine("Split string len: " + splitString.Length);
                Console.Clear();
                timeExtension.FormatDate(input);
                Console.ReadKey();
            }
        }

        #region how it started
        public static (DateTime, DateTime) ValidCheck(string[] split, string fullstring) {
            int baseYear = 2200;
            //need a check for managing just year here.
            if(split.Length == 1) {
                fullstring += "-01";
            }
            DateTime dateUpper = new();
            if(!DateTime.TryParse(fullstring, out DateTime dateLower))
                 dateLower = new(0001, 1, 1); 

            DateTime dateUpperExtra;
            Console.WriteLine("Splitstring len: " + split.Length);
            Console.WriteLine(string.Join("    ",split));

            if (split.Length == 1) {
                dateUpper = DateTime.Parse(fullstring);
                dateUpperExtra = new(dateUpper.Year, 12, DateTime.DaysInMonth(12, dateUpper.Month), 23, 59, 59);
                dateUpper = dateUpperExtra;
            }

            else if (split.Length == 2) {
                dateUpper = DateTime.Parse(fullstring);
                dateUpperExtra = new(dateUpper.Year, dateUpper.Month, DateTime.DaysInMonth(dateUpper.Year, dateUpper.Month),23,59,59);
                dateUpper = dateUpperExtra;
                
            }
            else if(split.Length == 3) { // 
                dateUpper = DateTime.Parse(fullstring);
                dateUpperExtra = new(dateUpper.Year, dateUpper.Month, dateUpper.Day, 23, 59, 59);
                dateUpper = dateUpperExtra;
            }
            else if(split.Length == 4) { //Hour specific
                dateUpper = DateTime.Parse(fullstring);
                dateUpperExtra = new(dateUpper.Year, dateUpper.Month, dateUpper.Day, dateUpper.Hour, 59, 59);
                dateUpper = dateUpperExtra;
            }
            else if(split.Length == 4) { // minute specific
                dateUpper = DateTime.Parse(fullstring);
                dateUpperExtra = new(dateUpper.Year, dateUpper.Month, dateUpper.Day, dateUpper.Hour, dateUpper.Hour, 59);
                dateUpper = dateUpperExtra;
            }

           return (dateLower, dateUpper);
        }
        #endregion

        #region Final result
        public class DateTimeExtension {
            char[] separators = { '-', ',', '.', ',', '/', '\\', ' ' };
            string[] formatArray;

            public DateTimeExtension(){
                CreateFormats();
            }

            void CreateFormats() {

                List<string> formatsList = new List<string>();
                formatsList.Add("yyyy");
                formatsList.Add("yy");

                foreach (char separator in separators) {
                    formatsList.Add($"yyyy{separator}MM");
                    formatsList.Add($"yyyy{separator}MM{separator}dd");
                    formatsList.Add($"yyyy{separator}MM{separator}dd{separator}HH");
                    formatsList.Add($"yyyy{separator}MM{separator}dd{separator}HH{separator}mm");
                    formatsList.Add($"yyyy{separator}MM{separator}dd{separator}HH{separator}mm{separator}ss");

                    formatsList.Add($"yyyy{separator}M");
                    formatsList.Add($"yyyy{separator}M{separator}dd");
                    formatsList.Add($"yyyy{separator}M{separator}dd{separator}HH");
                    formatsList.Add($"yyyy{separator}M{separator}dd{separator}HH{separator}mm");
                    formatsList.Add($"yyyy{separator}M{separator}dd{separator}HH{separator}mm{separator}ss");

                    formatsList.Add($"yyyy{separator}M");
                    formatsList.Add($"yyyy{separator}M{separator}d");
                    formatsList.Add($"yyyy{separator}M{separator}d{separator}HH");
                    formatsList.Add($"yyyy{separator}M{separator}d{separator}HH{separator}mm");
                    formatsList.Add($"yyyy{separator}M{separator}d{separator}HH{separator}mm{separator}ss");

                    formatsList.Add($"yyyy{separator}MM");
                    formatsList.Add($"yyyy{separator}MM{separator}d");
                    formatsList.Add($"yyyy{separator}MM{separator}d{separator}HH");
                    formatsList.Add($"yyyy{separator}MM{separator}d{separator}HH{separator}mm");
                    formatsList.Add($"yyyy{separator}MM{separator}d{separator}HH{separator}mm{separator}ss");

                    formatsList.Add($"yy{separator}MM");
                    formatsList.Add($"yy{separator}MM{separator}dd");
                    formatsList.Add($"yy{separator}MM{separator}dd{separator}HH");
                    formatsList.Add($"yy{separator}MM{separator}dd{separator}HH{separator}mm");
                    formatsList.Add($"yy{separator}MM{separator}dd{separator}HH{separator}mm{separator}ss");

                    formatsList.Add($"yy{separator}M");
                    formatsList.Add($"yy{separator}M{separator}dd");
                    formatsList.Add($"yy{separator}M{separator}dd{separator}HH");
                    formatsList.Add($"yy{separator}M{separator}dd{separator}HH{separator}mm");
                    formatsList.Add($"yy{separator}M{separator}dd{separator}HH{separator}mm{separator}ss");

                    formatsList.Add($"yy{separator}M");
                    formatsList.Add($"yy{separator}M{separator}d");
                    formatsList.Add($"yy{separator}M{separator}d{separator}HH");
                    formatsList.Add($"yy{separator}M{separator}d{separator}HH{separator}mm");
                    formatsList.Add($"yy{separator}M{separator}d{separator}HH{separator}mm{separator}ss");

                    formatsList.Add($"yy{separator}MM");
                    formatsList.Add($"yy{separator}MM{separator}d");
                    formatsList.Add($"yy{separator}MM{separator}d{separator}HH");
                    formatsList.Add($"yy{separator}MM{separator}d{separator}HH{separator}mm");
                    formatsList.Add($"yy{separator}MM{separator}d{separator}HH{separator}mm{separator}ss");
                }

                formatArray = formatsList.ToArray();
            }

            public (DateTime, DateTime) FormatDate(string input) { //Some cases will cause this to crash not super safe but for now this will work
                
                DateTime lowerDate = new();
                DateTime upperDate = new();

                if (!DateTime.TryParseExact(input, formatArray, CultureInfo.InvariantCulture, DateTimeStyles.None, out lowerDate)) {
                    Console.WriteLine("Could not format");
                    lowerDate = DateTime.Now;
                }

                Console.WriteLine(lowerDate.ToString());
                upperDate = lowerDate;

                string[] splitString = input.Split(separators);
                if (splitString.Length == 1) { // year
                    upperDate = upperDate.AddSeconds(-1);
                    upperDate = upperDate.AddYears(1);
                }
                else if (splitString.Length == 2) { // month
                    upperDate = upperDate.AddMonths(1);
                    upperDate = upperDate.AddSeconds(-1);
                }
                else if (splitString.Length == 3) { // day
                    upperDate = upperDate.AddDays(1);
                    upperDate = upperDate.AddSeconds(-1);
                }
                else if (splitString.Length == 4) { // hour
                    upperDate = upperDate.AddHours(1);
                    upperDate = upperDate.AddSeconds(-1);
                }
                else if (splitString.Length == 5) { //minute
                    upperDate = upperDate.AddMinutes(1);
                    upperDate = upperDate.AddSeconds(-1);
                }
                else if (splitString.Length == 6) { //Second
                    upperDate = upperDate.AddSeconds(1);
                    upperDate = upperDate.AddMicroseconds(-1);
                }
                else if (splitString.Length == 7) { //milliseconds
                    upperDate = upperDate.AddMilliseconds(1);
                    upperDate = upperDate.AddMicroseconds(-1);
                }
                Console.WriteLine(upperDate.ToString());




                return (lowerDate, upperDate);
            }
        }
        #endregion

    }
}
