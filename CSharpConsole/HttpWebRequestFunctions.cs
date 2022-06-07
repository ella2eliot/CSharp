using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CSharpConsole
{
    public class HttpWebRequestFunctions
    {        
        public HttpWebRequestFunctions() { }
        public TencentMPTResult TencentMPTUpload(string clientId, string sign, int ts)
        {
            string _testUrl = "https://tsrm.tencent.com/public-api-test";
            string _prdUrl = "https://tsrm.tencent.com/open-api";
            var result = new TencentMPTResult();
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format("{0}/tscm-service-mpt/vendor/upload?clientId={1}&sign={2}&ts={3}", _testUrl, clientId, sign, ts));
                req.KeepAlive = false;
                req.ProtocolVersion = HttpVersion.Version10;
                req.Method = "POST";
                req.ContentType = "application/json";
                var postData = new Dictionary<string, string>
                {

                    { "Action", "SupplierUploadLog" },
                    { "StartCompany", "" },
                    { "ServerSN", "" },
                    { "VersionId", "" },
                    { "StrModelName", "" },
                    { "StrDeviceClassName", "" },
                    { "StrVersion", "" },
                    { "LogType", "" }, //Stress or MTP
                    { "OrderNo", "" },
                    { "File", "" },
                };

                byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postData));
                req.ContentLength = postBytes.Length;

                Stream requestStream = req.GetRequestStream();
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream resStream = response.GetResponseStream();

                var sr = new StreamReader(response.GetResponseStream());
                string responseText = sr.ReadToEnd();

                JObject jo = (JObject)JsonConvert.DeserializeObject(responseText);
                
                result.status = jo["status"].ToString();
                result.message = jo["message"].ToString();

                return result;
            }
            catch (Exception ex)
            {
                result.status = "internal fail";
                result.message = ex.ToString();
                return result;
            }
        }
        public TencentMPTResult TencentMPTHello(string clientId, string sign, int ts)
        {
            string _testUrl = "https://tsrm.tencent.com/public-api-test";
            string _prdUrl = "https://tsrm.tencent.com/open-api";
            var result = new TencentMPTResult();
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format("{0}/tscm-service-tsrm/hello?clientId={1}&sign={2}&ts={3}", _testUrl, clientId, sign, ts));
                req.KeepAlive = false;
                req.ProtocolVersion = HttpVersion.Version10;
                req.Method = "GET";
                //CookieContainer cc = new CookieContainer();
                //cc.Add(new Cookie { Value = token, Domain = ws.Domain, Name = ws.Remark });
                //req.CookieContainer = cc;
                req.ContentType = "application/json";


                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream resStream = response.GetResponseStream();

                var sr = new StreamReader(response.GetResponseStream());
                string responseText = sr.ReadToEnd();

                JObject jo = (JObject)JsonConvert.DeserializeObject(responseText);

                result.status = jo["status"].ToString();
                result.message = jo["message"].ToString();

                return result;
            }
            catch (Exception ex)
            {
                result.status = "internal fail";
                result.message = ex.ToString();
                return result;
            }
        }
        public class TencentMPTResult
        {
            public string status { get; set; }
            public string message { get; set; }
        }
    }
}
