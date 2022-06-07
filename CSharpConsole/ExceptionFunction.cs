using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsole
{
    class ExceptionFunction
    {
        public static void MulityCatchTest()
        {
            try
            {
                try
                {
                    string message = "Hello world";
                    Console.WriteLine("Process print string. "+ message);
                    throw new Exception("Inner exception.");
                }
                catch (Exception ex)
                {
                    //var innerMessage = ex.Message;
                    //Console.WriteLine("Catch inner exception. " + innerMessage);
                    throw;
                }
                finally
                {
                    Console.WriteLine("Inner final.");
                }
            }
            catch (Exception ex)
            {
                var outerMessage = ex.Message;
                Console.WriteLine("Catch outer exception. " + outerMessage);                
            }
            finally
            {
                Console.WriteLine("Outer final.");
            }
        }
    }
}
