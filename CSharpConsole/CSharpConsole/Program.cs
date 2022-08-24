using System;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            //DateTimeFunctions.DtToString();
            //DateTimeFunctions.DtFromString("2019/07/30 15:18:53");
            StringFunctions.StringHeaderKanban();
            //DictionaryFunction.FristDefaultTest();
            //HealthCare.DemoHealth();
            //ExceptionFunction.MulityCatchTest();
            //JsonFunctions.ParseTesting();
            Console.ReadLine();
            //TencentMPTMovement();
        }

        public static void TencentMPTMovement()
        {
            var dt = DateTime.UtcNow;
            Console.WriteLine(dt);
            var ts = DateTimeFunctions.ConvertDateTime2TimeStamp(dt);
            var clientId = "inventec-201910-achn";
            var secretKey = "ffc44369-fcb6-o2dj";
            var SHA256 = new SHA256Functions();

            var obj = new HttpWebRequestFunctions();
            //var result=obj.TencentMPTHello(clientId, SHA256.TencentMPTSHA256(clientId, secretKey, ts), ts);
            /*
            var result=obj.TencentMPTUpload(clientId, SHA256.TencentMPTSHA256(clientId, secretKey, ts), ts);
            Console.WriteLine(string.Format("status: {0} \r\nmessage: {1}", result.status, result.message));
            */
            Console.WriteLine(string.Format("ts: {0} \r\nSHA256: {1}", ts, SHA256.TencentMPTSHA256(clientId, secretKey, ts)));

            Console.ReadLine();
        }

        
    }    
}
