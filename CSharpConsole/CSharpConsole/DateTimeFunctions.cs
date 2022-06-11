using System;

namespace CSharpConsole
{
    public static class DateTimeFunctions
    {
        static DateTime _gtm = new DateTime(1970, 1, 1);//宣告一個GTM時間出來
        // DateTime _utc = DateTime.UtcNow.AddHours(8);//宣告一個目前的時間
        
        public static int ConvertDateTime2TimeStamp(DateTime utc)
        {
            return Convert.ToInt32(((TimeSpan)utc.Subtract(_gtm)).TotalSeconds);
        }

        public static DateTime ConvertTimeStamp2DateTime(int timeStamp)
        {
            return (_gtm).AddSeconds(Convert.ToInt32(timeStamp));
        }

        public static void DtToString()
        {
            DateTime dt = DateTime.Now;
            Console.WriteLine(dt.ToString("yyyy/MM/dd HH:mm:ss"));            
            Console.WriteLine(dt.ToString("yyyy/MM/dd 00:00:00"));            

            Console.ReadLine();
        }

        public static void DtFromString(string stringDt)
        {
            DateTime dt;
            if (stringDt.Length == 14)
            {
                stringDt = string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                    stringDt.Substring(0, 4), stringDt.Substring(4, 2), stringDt.Substring(6, 2),
                    stringDt.Substring(8, 2), stringDt.Substring(10, 2), stringDt.Substring(12, 2)
                );
                if (DateTime.TryParse(stringDt, out dt))
                {
                    Console.WriteLine(dt.ToString("yyyy/MM/dd HH:mm:ss"));
                }
                else
                {
                    Console.WriteLine("DateTime convert fail. (Format error) ");
                }
            }
            else if (stringDt.Length == 19)
            {
                if (DateTime.TryParse(stringDt, out dt))
                {
                    Console.WriteLine(dt.ToString("yyyy/MM/dd HH:mm:ss"));
                }
                else
                {
                    Console.WriteLine("DateTime convert fail. (Format error) ");
                }
            }
            else
            {
                Console.WriteLine("DateTime convert fail. (Length error) ");
            }

            Console.ReadLine();
        }
    }
}
