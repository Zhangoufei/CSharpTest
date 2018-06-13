using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace CyPhone.Common.Json
{
    public static class Json
    {
        public static object ToJson(this string sJson)
        {
            return sJson == null ? null : JsonConvert.DeserializeObject(sJson);
        }
        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static string ToJson(this object obj, string datetimeformats)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static T ToObject<T>(this string sJson)
        {
            return sJson == null ? default(T) : JsonConvert.DeserializeObject<T>(sJson);
        }
        public static List<T> ToList<T>(this string sJson)
        {
            return sJson == null ? null : JsonConvert.DeserializeObject<List<T>>(sJson);
        }
        public static DataTable ToTable(this string sJson)
        {
            return sJson == null ? null : JsonConvert.DeserializeObject<DataTable>(sJson);
        }
        public static JObject ToJObject(this string sJson)
        {
            return sJson == null ? JObject.Parse("{}") : JObject.Parse(sJson.Replace("&nbsp;", ""));
        }
    }
}
