using System;
using System.Text.RegularExpressions;

namespace CSharpConsole
{
    class RegexFunctions
    {
        public RegexFunctions() { }

        public static void PhoneRegex()
        {
            
            if(Regex.IsMatch("0912345678", "^09\\d{8}$"))
            {
                Console.WriteLine("Match");
            }
            else
            {
                Console.WriteLine("Not match");
            }

        }
    }
}
