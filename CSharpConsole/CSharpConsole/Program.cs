using Autofac;
using HealthCareConsole;
using InterView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //DateTimeFunctions.DtToString();
                //DateTimeFunctions.DtFromString("2019/07/30 15:18:53");
                //StringFunctions.StartWithTesting();
                //DictionaryFunction.FristDefaultTest();
                //HealthCare.DemoHealth();
                //ExceptionFunction.MulityCatchTest();
                //JsonFunctions.ParseTesting();
                //TencentMPTMovement();

                //SortArray();

                //BusinessLogic businessLogic = new BusinessLogic();
                //businessLogic.ProcessData();

                var container = ContainerConfig.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app=scope.Resolve<IApplication>();
                    app.Run();
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            

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

        public static void Collections()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("big", "大");
            map.Add("small", "小");
            map.Add("large", "大");
            map.Add("little", "小");
            List<string> removeList=new List<string>(); 
            foreach (var item in map)
            {
                Console.WriteLine(string.Format("Key: {0}, Value: {1}", item.Key, item.Value));
                if (item.Value == "大")
                {
                    removeList.Add(item.Key);
                }
            }
            foreach (var item in removeList)
            {
                map.Remove(item);
            }            
        }
        public static void SortArray()
        {
            object[] array = new object[6];
            array[0] = "1";
            array[1] = "3";
            array[2] = 2;
            array[3] = 5;
            array[4] = 4.0;
            array[5] = 6.0;
            List<int> orderList= new List<int>();
            int target = 0;
            foreach (var item in array)
            {
                if(int.TryParse(item.ToString(), out target))
                    orderList.Add(target);
            }
            orderList.Sort();
            var result = "";
            foreach (var item in orderList)
            {
                result += item.ToString() + "+";
            }
            Console.WriteLine(result.Substring(0, result.Length-1));
        }
        public static int[] reduce(object[] data, string operate)
        {
            var result = new int[data.Length];
            try
            {
                List<int> orderList = new List<int>();
                int target = 0;
                foreach (var item in data)
                {
                    if (int.TryParse(item.ToString(), out target))
                        orderList.Add(target);
                }
                switch (operate)
                {
                    case "sum":
                        // sum() function ...
                        break;
                    case "mean":
                        // mean() function ...
                        break;
                    case "min":
                        // min() function ...
                        break;
                    case "max":
                        // max() function ...
                        break;
                    case "range":
                        // range() function ...
                        break;
                    case "variance":
                        // variance() function ...
                        break;
                    case "stddev":
                        // stddev() function ...
                        break;
                    default:
                        Console.WriteLine("Unhandeling operator");
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
        
    }    
}
