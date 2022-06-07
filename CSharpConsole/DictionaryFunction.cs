using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsole
{
    class DictionaryFunction
    {
        public static void FristDefaultTest()
{
            Dictionary<string,int> users = new Dictionary<string, int>();
            users.Add("Patrick", 1);
            users.Add("Casper", 2);
            users.Add("Andy", 3);


            var output = users.Where(x => x.Key == "Patric").FirstOrDefault();

            Console.WriteLine(output.Value);
            Console.ReadLine();

        }
    }
}
