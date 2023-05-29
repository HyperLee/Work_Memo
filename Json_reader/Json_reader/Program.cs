using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_reader
{
    class Program
    {
        static void Main(string[] args)
        {
            string _GetWebData_Decrypt = "";
            _GetWebData_Decrypt = "{\"data\":[{\"keyword\":\"阿\",\"rules\":[\"1\",\"2\"],\"sid\":4,\"status\":\"1\"},{\"keyword\":\"零售\",\"rules\":[\"1\",\"2\"],\"sid\":60,\"status\":\"1\"},{\"keyword\":\"產銷班\",\"rules\":[\"2\"],\"sid\":65,\"status\":\"1\"},{\"keyword\":\"推廣\",\"rules\":[\"2\"],\"sid\":66,\"status\":\"1\"},{\"keyword\":\"行銷\",\"rules\":[\"2\"],\"sid\":67,\"status\":\"1\"},{\"keyword\":\"實驗\",\"rules\":[\"2\"],\"sid\":68,\"status\":\"1\"},{\"keyword\":\"X\",\"rules\":[\"2\"],\"sid\":69,\"status\":\"0\"},{\"keyword\":\"小\",\"rules\":[\"1\"],\"sid\":70,\"status\":\"1\"}],\"status\":\"成功\"}";


            JObject obj = JsonConvert.DeserializeObject<JObject>(_GetWebData_Decrypt);

            string data1 = obj["data"].ToString();
            string status = obj["status"].ToString();

            if(status == "成功")
            {

            }

            var dynamic_obj = JObject.Parse(_GetWebData_Decrypt);
            JArray data = dynamic_obj.GetValue("data") as JArray;
            JArray data_status = dynamic_obj.GetValue("status") as JArray;
            foreach (JObject dataItem in data)
            {
                var _keyword = dataItem.GetValue("keyword");
                string _keyword2 = _keyword.ToString();
                _keyword2 = _keyword2.Replace("{", "");
                _keyword2 = _keyword2.Replace("}", "");
                _keyword2 = _keyword2.Replace("[", "");
                _keyword2 = _keyword2.Replace("]", "");
                _keyword2 = _keyword2.Replace("\n", "");
                _keyword2 = _keyword2.Replace(" ", "");

                var _rules = dataItem.GetValue("rules");
                string _rules2 = _rules.ToString();
                _rules2 = _rules2.Replace("{", "");
                _rules2 = _rules2.Replace("}", "");
                _rules2 = _rules2.Replace("[", "");
                _rules2 = _rules2.Replace("]", "");
                _rules2 = _rules2.Replace("\\", "");
                _rules2 = _rules2.Replace("\r", "");
                _rules2 = _rules2.Replace("\n", "");
                _rules2 = _rules2.Replace(" ", "");
                _rules2 = _rules2.Replace("\r\n", "");
                _rules2 = _rules2.Replace("\\", "");
                _rules2 = _rules2.Replace("\\ ", "");
                _rules2 = _rules2.Replace(" \\ ", "");

                var _sid = dataItem.GetValue("sid");



                var _status1 = dataItem.GetValue("status");
            }


            Console.WriteLine(status);
            Console.ReadKey();


        }
    }
}
