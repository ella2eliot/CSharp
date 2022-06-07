using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSharpConsole
{
    class JsonFunctions
    {
        public JsonFunctions() { }
        public static void ParseTesting()
        {
            //var str = "{'SerialNumber':'207549400001','AssetTag':'15616968','BoardSerialNumber':'B92960313008100A','MAC':'B4A9FCBCDB3A','ChassisVendorSerialNumber':'B4A9FCBCDB3A','ModuleVendorSerialNumber':''}";
            //var str = "[{'SerialNumber':'207549400001','AssetTag':'15616968','BoardSerialNumber':'B92960313008100A','MAC':'B4A9FCBCDB3A','ChassisVendorSerialNumber':'B4A9FCBCDB3A','ModuleVendorSerialNumber':''},{'SerialNumber':'207549400002','AssetTag':'15616969','BoardSerialNumber':'B92960313008100B','MAC':'B4A9FCBCDB3B','ChassisVendorSerialNumber':'B4A9FCBCDB3B','ModuleVendorSerialNumber':''}]";
            var str = "[{'ServerSerialNo':'202011920015','PartNumber':'M1186594-001','VendorPartNumber':'SR-02251-001','VendorName':'','LotTrackingNumber':'','ProcessName':'Manufacture','ProductLifecycle':'MP','DeviceEventTime':'2021-12-12 22:36:07','AssetTag':'15955807','BoardSerialNumber':'B92961376020800A','MAC':'C01850536871','ChassisVendorSerialNumber':'P98761385020700A','ModuleendorSerialNumber':'','Components':[{'AssemblyPartNumber':'M1186594-001','AssemblySerialNumber':'202011920015','ComponentSerialNumber':'9JK0348M10104_100-000000056','ComponentPartNumber':'M1108447-001','ComponentVendorPartNumber':'100-000000056-00','ComponentVendorName':'ADVANCED MICRO DEVICES','ComponentLotTrackingNumber':'',			'IndexSno':'9JK0348M10104_100-000000056'},{'AssemblyPartNumber':'M1186594-001','AssemblySerialNumber':'202011920015','ComponentSerialNumber':'9JK0348M10115_100-000000056','ComponentPartNumber':'M1108447-001','ComponentVendorPartNumber':'100-000000056-00','ComponentVendorName':'ADVANCED MICRO DEVICES','ComponentLotTrackingNumber':'','IndexSno':'9JK0348M10115_100-000000056'}]},{'ServerSerialNo':'202011920147','PartNumber':'M1186594-001','VendorPartNumber':'SR-02251-001','VendorName':'','LotTrackingNumber':'','ProcessName':'Manufacture','ProductLifecycle':'MP','DeviceEventTime':'2021-12-12 22:36:07','AssetTag':'15962702','BoardSerialNumber':'B92961353001200A','MAC':'C01850418360','ChassisVendorSerialNumber':'P98761364035600A','ModuleendorSerialNumber':'','Components':[{'AssemblyPartNumber':'M1186594-001','AssemblySerialNumber':'202011920147','ComponentSerialNumber':'P380413531954010','ComponentPartNumber':'M1103804-001','ComponentVendorPartNumber':'M1103804-001','ComponentVendorName':'WIWYNN','ComponentLotTrackingNumber':'','IndexSno':'P380413531954010'},{'AssemblyPartNumber':'M1186594-001','AssemblySerialNumber':'202011920147','ComponentSerialNumber':'P73801121007300C','ComponentPartNumber':'M1037380-006','ComponentVendorPartNumber':'M1037380-006','ComponentVendorName':'INGRASYS','ComponentLotTrackingNumber':'','IndexSno':'P73801121007300C'}]}]";
            //JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            var jo = JsonConvert.DeserializeObject<List<SNHInfo>>(str);
            
            foreach (var sr in jo)
            {
                //Console.WriteLine(string.Format("SerialNo:{0}, AssetTag:{1}, BorderSerialNo:{2}, MAC:{3}, ChassisVendorSerialNo:{4}, ModuleVendorSerialNumber:{5}",
                //    sr.IndexSno, sr.AssetTag, sr.BoardSerialNumber, sr.MAC, sr.ChassisVendorSerialNumber, sr.ModuleendorSerialNumber));
            }
                        

            //var jo = JsonConvert.DeserializeObject<SRInfo>(str);

            //foreach (var item in jo)
            //{

            //}
            

        }
    }
    public class SNHInfo
    {
        public string ServerSerialNo { get; set; }
        public string PartNumber { get; set; }
        public string VendorPartNumber { get; set; }
        public string VendorName { get; set; }
        public string LotTrackingNumber { get; set; }
        public string ProcessName { get; set; }
        public string ProductLifecycle { get; set; }
        public string DeviceEventTime { get; set; }
        public string AssetTag { get; set; }
        public string BoardSerialNumber { get; set; }
        public string MAC { get; set; }
        public string ChassisVendorSerialNumber { get; set; }
        public string ModuleendorSerialNumber { get; set; }
        public List<Component> Components { get; set; }
    }
    public class Component
    {
        public string AssemblyPartNumber { get; set; }
        public string AssemblySerialNumber { get; set; }
        public string ComponentSerialNumber { get; set; }
        public string ComponentPartNumber { get; set; }
        public string ComponentVendorPartNumber { get; set; }
        public string ComponentVendorName { get; set; }
        public string ComponentLotTrackingNumber { get; set; }
        public string IndexSno { get; set; }
    }
}
