using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpConsole
{
    class HealthCare
    {
        public HealthCare() { }

        public static void DemoHealth()
        {
            var rd = new Random();
            var callSub = 1;
            while (true)
            {
                var mmHg=rd.Next(50,130);
                var heartbeat=rd.Next(50, 130);
                var age=rd.Next(18, 40);

                Console.ForegroundColor = ConsoleColor.Green;
                if ((mmHg > 128 || mmHg < 52 )&& callSub == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("mmHg:{0}, heartbeat:{1}, age:{2}",mmHg,heartbeat,age));
                    Console.WriteLine("Call Azure Logic let manager care about potential employee !!! ");
                    // call other project console
                    //System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                    //pProcess.StartInfo.FileName = @"D:\Program\CSharpConsole\CSharpConsole\bin\Debug\HealthCareConsole.exe";
                    //pProcess.Start();
                    // limited open one time.
                    //callSub++;
                    //pProcess.StartInfo.Arguments = inputPath + " " + outputPath;
                    //pProcess.WaitForExit();
                    //pProcess.Close();
                    //if ()
                    //{
                    //}
                }
                else
                {
                    Console.WriteLine(string.Format("mmHg:{0}, heartbeat:{1}, age:{2}",mmHg,heartbeat,age));
                }

                Thread.Sleep(100);
            }
        }
    }
}
