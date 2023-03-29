using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharpConsole
{
    class StringFunctions
    {
        public StringFunctions() { }

        public static void StringHeaderKanban()
        {
            // http://patorjk.com/software/taag/#p=display&f=Big&t=Hi
            var str = @"
  _   _       _      _____                              _     _____          _____ _     _                        _           _  _    _ _   __/\           _     ____                   __     _ 
 | \ | |     | |    / ____|                            | |   |_   _|        / ____| |   (_)                      | |  ____  _| || |_ | (_) / //\| ___   /\| |/\ / /\ \          _  _____\ \   | |
 |  \| | ___ | |_  | (___  _   _ _ __  _ __   ___  _ __| |_    | |  _ __   | |    | |__  _ _ __   ___  ___  ___  | | / __ \|_  __  _/ __) / /    ( _ )  \ ` ' /| |  | |______ _| ||______\ \  | |
 | . ` |/ _ \| __|  \___ \| | | | '_ \| '_ \ / _ \| '__| __|   | | | '_ \  | |    | '_ \| | '_ \ / _ \/ __|/ _ \ | |/ / _` |_| || |_\__ \/ /     / _ \/\_     _| |  | |______|_   _|_____ \ \ | |
 | |\  | (_) | |_   ____) | |_| | |_) | |_) | (_) | |  | |_   _| |_| | | | | |____| | | | | | | |  __/\__ \  __/ |_| | (_| |_  __  _(   / / _   | (_>  </ , . \| |  | |        |_||______| \ \| |
 |_| \_|\___/ \__| |_____/ \__,_| .__/| .__/ \___/|_|   \__| |_____|_| |_|  \_____|_| |_|_|_| |_|\___||___/\___| (_)\ \__,_| |_||_|  |_/_/ (_)   \___/\/\/|_|\/| |  | |                     \_\ |
                                | |   | |                                                                            \____/                                     \_\/_/_____                   | |
                                |_|   |_|                                                                                                                           |______|                  |_|
            ";
            Console.WriteLine(str);            
        }
        public static void StringSplit()
        {   
            var str = "ST,GS,SN,P11537-001      ,+ 66.700kg";
            var array_str= str.Split(',');
            Console.WriteLine(array_str[3].Trim());
            // get double with REGEX           
            Console.WriteLine(Regex.Match(array_str[4], "[+-]?([0-9]*[.])?[0-9]+"));
            Console.WriteLine(Convert.ToDouble(Regex.Match(array_str[4].Trim(), "[+-]?([0-9]*[.])?[0-9]+").Value));
            Console.WriteLine(array_str.Length);
        }
        
        public static void StartWithTesting()
        {
            var str = "3S3C20-000GQ";
            if (str.StartsWith("3S"))
            {
                Console.WriteLine(str.Substring(2, str.Length-2));
            }
            else
            {
                Console.WriteLine("Not start with 3S");
            }
        }
        public static void SubStringTesting()
        {
            var str = "20201210";

            Console.WriteLine(string.Format("{0}/{1}/{2}", str.Substring(0, 4), str.Substring(4, 2), str.Substring(6, 2)));
            Console.WriteLine(string.Format("{0}", "000000010000000000".Substring(7, 1)));

            Console.ReadLine();
        }
        public static void ReplaceString()
        {
            var str = "40/46/51/81/83";
            string newstr=str.Replace("/81", "/81/8W");
            Console.WriteLine(string.Format("old str: {0}, new str: {1}", str, newstr));
            Console.WriteLine("1598B123sds".Substring(0,5)== "1598B" ? "Y":"N");
            
            Console.ReadLine();
        }

        public static void StringJoin()
        {
            List<string> strs = new List<string> {
                "Apple",
                "Banana",
                "Cat",
                "Dog"
            };
            //var strs = "UserBadgeID	Year	Month	Week	BadgeID	BadgeName	DepID	DepName	ManagerID	ManagerName	TaskInfo	TaskCategory	TaskBu	TaskSite	TaskPercentage	Creator	Editor	Cdt	Udt";
            Console.WriteLine(string.Join(";",strs));
            Console.ReadLine();

        }
        public static void StringExprestion()
        {
            var str = "40/46/51/81/83";
            string newstr = str.Replace("/81", "/81/8W");

            Console.WriteLine(string.Format("old str: {0}, new str: {1}", str, newstr));
            Console.WriteLine($"old str: {str}, new str: {newstr}");
        }

        public static void StringContain()
        {
            //List<string> skus = new List<string> { "WG", "WS", "WC", "XG", "XS", "XC", "YG", "YS", "YC" };
            List<string> skus = new List<string> { "WG1522","WG2002","WG1684","WG1447"};
            var ta = "WG2002000254";
            if(skus.Contains(ta.Substring(0, 6)))
            {
                Console.WriteLine(ta);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Not contain in skus.");
                Console.ReadLine();
            }

        }
        public static void StringLength()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("A1");
            //sb.AppendLine("b2");

            Console.WriteLine(sb.ToString().Length);
            Console.WriteLine(sb.ToString());
            Console.ReadLine();

        }
    }
}
