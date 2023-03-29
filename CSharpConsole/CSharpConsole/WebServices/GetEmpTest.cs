using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpConsole.WebServices
{
    class GetEmpTest
    {
        public GetEmpTest()
        {

        }

        public static void TestWebServiceLimited() {
            var ws = new DARFON.GAIAGetEmp.GetEmpWebService();
            var auth = new DARFON.GAIAGetEmp.AuthHeader { UserName="dfITS", Password="ITS@dmin01" };
            ws.AuthHeaderValue = auth;
            int times = 0;
            try
            {
                while (true)
                {                    
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    var data = ws.GetEmpByEmpNo("220900004");
                    sw.Stop();
                    Console.WriteLine($"WS {++times} spendTime:{sw.ElapsedMilliseconds}ms, data result:{data.EnName}, {data.Name}, 分機:{data.ExtNo}, remark:{data.remark}");
                    // Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WS {++times}, {ex.ToString()}");
            }
            
        }

    }
}
