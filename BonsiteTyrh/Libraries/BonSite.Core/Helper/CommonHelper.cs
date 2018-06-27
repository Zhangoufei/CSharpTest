using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Web;

namespace BonSite.Core
{
    /// <summary>
    /// 普通帮助类
    /// </summary>
    public class CommonHelper
    {
        //星期数组
        private static string[] _weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        //空格、回车、换行符、制表符正则表达式
        private static Regex _tbbrRegex = new Regex(@"\s*|\t|\r|\n", RegexOptions.IgnoreCase);

        #region 时间操作

        /// <summary>
        /// 获得当前时间的""yyyy-MM-dd HH:mm:ss:fffffff""格式字符串
        /// </summary>
        public static string GetDateTimeMS()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 获得当前时间的""yyyy年MM月dd日 HH:mm:ss""格式字符串
        /// </summary>
        public static string GetDateTimeU()
        {
            return string.Format("{0:U}", DateTime.Now);
        }

        /// <summary>
        /// 获得当前时间的""yyyy-MM-dd HH:mm:ss""格式字符串
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获得当前日期
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获得中文当前日期
        /// </summary>
        public static string GetChineseDate()
        {
            return DateTime.Now.ToString("yyyy月MM日dd");
        }

        /// <summary>
        /// 获得当前时间(不含日期部分)
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 获得当前小时
        /// </summary>
        public static string GetHour()
        {
            return DateTime.Now.Hour.ToString("00");
        }

        /// <summary>
        /// 获得当前天
        /// </summary>
        public static string GetDay()
        {
            return DateTime.Now.Day.ToString("00");
        }

        /// <summary>
        /// 获得当前月
        /// </summary>
        public static string GetMonth()
        {
            return DateTime.Now.Month.ToString("00");
        }

        /// <summary>
        /// 获得当前年
        /// </summary>
        public static string GetYear()
        {
            return DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 获得当前星期(数字)
        /// </summary>
        public static string GetDayOfWeek()
        {
            return ((int)DateTime.Now.DayOfWeek).ToString();
        }

        /// <summary>
        /// 获得当前星期(汉字)
        /// </summary>
        public static string GetWeek()
        {
            return _weekdays[(int)DateTime.Now.DayOfWeek];
        }

        #endregion

        #region 数组操作

        /// <summary>
        /// 获得字符串在字符串数组中的位置
        /// </summary>
        public static int GetIndexInArray(string s, string[] array, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(s) || array == null || array.Length == 0)
                return -1;

            int index = 0;
            string temp = null;

            if (ignoreCase)
                s = s.ToLower();

            foreach (string item in array)
            {
                if (ignoreCase)
                    temp = item.ToLower();
                else
                    temp = item;

                if (s == temp)
                    return index;
                else
                    index++;
            }

            return -1;
        }

        /// <summary>
        /// 获得字符串在字符串数组中的位置
        /// </summary>
        public static int GetIndexInArray(string s, string[] array)
        {
            return GetIndexInArray(s, array, false);
        }

        /// <summary>
        /// 判断字符串是否在字符串数组中
        /// </summary>
        public static bool IsInArray(string s, string[] array, bool ignoreCase)
        {
            return GetIndexInArray(s, array, ignoreCase) > -1;
        }

        /// <summary>
        /// 判断字符串是否在字符串数组中
        /// </summary>
        public static bool IsInArray(string s, string[] array)
        {
            return IsInArray(s, array, false);
        }

        /// <summary>
        /// 判断字符串是否在字符串中
        /// </summary>
        public static bool IsInArray(string s, string array, string splitStr, bool ignoreCase)
        {
            return IsInArray(s, StringHelper.SplitString(array, splitStr), ignoreCase);
        }

        /// <summary>
        /// 判断字符串是否在字符串中
        /// </summary>
        public static bool IsInArray(string s, string array, string splitStr)
        {
            return IsInArray(s, StringHelper.SplitString(array, splitStr), false);
        }

        /// <summary>
        /// 判断字符串是否在字符串中
        /// </summary>
        public static bool IsInArray(string s, string array)
        {
            return IsInArray(s, StringHelper.SplitString(array, ","), false);
        }



        /// <summary>
        /// 将对象数组拼接成字符串
        /// </summary>
        public static string ObjectArrayToString(object[] array, string splitStr)
        {
            if (array == null || array.Length == 0)
                return "";

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
                result.AppendFormat("{0}{1}", array[i], splitStr);

            return result.Remove(result.Length - splitStr.Length, splitStr.Length).ToString();
        }

        /// <summary>
        /// 将对象数组拼接成字符串
        /// </summary>
        public static string ObjectArrayToString(object[] array)
        {
            return ObjectArrayToString(array, ",");
        }

        /// <summary>
        /// 将字符串数组拼接成字符串
        /// </summary>
        public static string StringArrayToString(string[] array, string splitStr)
        {
            return ObjectArrayToString(array, splitStr);
        }

        /// <summary>
        /// 将字符串数组拼接成字符串
        /// </summary>
        public static string StringArrayToString(string[] array)
        {
            return StringArrayToString(array, ",");
        }

        /// <summary>
        /// 将整数数组拼接成字符串
        /// </summary>
        public static string IntArrayToString(int[] array, string splitStr)
        {
            if (array == null || array.Length == 0)
                return "";

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
                result.AppendFormat("{0}{1}", array[i], splitStr);

            return result.Remove(result.Length - splitStr.Length, splitStr.Length).ToString();
        }

        /// <summary>
        /// 将整数数组拼接成字符串
        /// </summary>
        public static string IntArrayToString(int[] array)
        {
            return IntArrayToString(array, ",");
        }



        /// <summary>
        /// 移除数组中的指定项
        /// </summary>
        /// <param name="array">源数组</param>
        /// <param name="removeItem">要移除的项</param>
        /// <param name="removeBackspace">是否移除空格</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static string[] RemoveArrayItem(string[] array, string removeItem, bool removeBackspace, bool ignoreCase)
        {
            if (array != null && array.Length > 0)
            {
                StringBuilder arrayStr = new StringBuilder();
                if (ignoreCase)
                    removeItem = removeItem.ToLower();
                string temp = "";
                foreach (string item in array)
                {
                    if (ignoreCase)
                        temp = item.ToLower();
                    else
                        temp = item;

                    if (temp != removeItem)
                        arrayStr.AppendFormat("{0}_", removeBackspace ? item.Trim() : item);
                }

                return StringHelper.SplitString(arrayStr.Remove(arrayStr.Length - 1, 1).ToString(), "_");
            }

            return array;
        }

        /// <summary>
        /// 移除数组中的指定项
        /// </summary>
        /// <param name="array">源数组</param>
        /// <returns></returns>
        public static string[] RemoveArrayItem(string[] array)
        {
            return RemoveArrayItem(array, "", true, false);
        }

        /// <summary>
        /// 移除字符串中的指定项
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="splitStr">分割字符串</param>
        /// <returns></returns>
        public static string[] RemoveStringItem(string s, string splitStr)
        {
            return RemoveArrayItem(StringHelper.SplitString(s, splitStr), "", true, false);
        }

        /// <summary>
        /// 移除字符串中的指定项
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns></returns>
        public static string[] RemoveStringItem(string s)
        {
            return RemoveArrayItem(StringHelper.SplitString(s), "", true, false);
        }



        /// <summary>
        /// 移除数组中的重复项
        /// </summary>
        /// <returns></returns>
        public static int[] RemoveRepeaterItem(int[] array)
        {
            if (array == null || array.Length < 2)
                return array;

            Array.Sort(array);

            int length = 1;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] != array[i - 1])
                    length++;
            }

            int[] uniqueArray = new int[length];
            uniqueArray[0] = array[0];
            int j = 1;
            for (int i = 1; i < array.Length; i++)
                if (array[i] != array[i - 1])
                    uniqueArray[j++] = array[i];

            return uniqueArray;
        }

        /// <summary>
        /// 移除数组中的重复项
        /// </summary>
        /// <returns></returns>
        public static string[] RemoveRepeaterItem(string[] array)
        {
            if (array == null || array.Length < 2)
                return array;

            Array.Sort(array);

            int length = 1;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] != array[i - 1])
                    length++;
            }

            string[] uniqueArray = new string[length];
            uniqueArray[0] = array[0];
            int j = 1;
            for (int i = 1; i < array.Length; i++)
                if (array[i] != array[i - 1])
                    uniqueArray[j++] = array[i];

            return uniqueArray;
        }

        /// <summary>
        /// 去除字符串中的重复元素
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueString(string s)
        {
            return GetUniqueString(s, ",");
        }

        /// <summary>
        /// 去除字符串中的重复元素
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueString(string s, string splitStr)
        {
            return ObjectArrayToString(RemoveRepeaterItem(StringHelper.SplitString(s, splitStr)), splitStr);
        }

        #endregion

        /// <summary>
        /// 去除字符串首尾处的空格、回车、换行符、制表符
        /// </summary>
        public static string TBBRTrim(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return str.Trim().Trim('\r').Trim('\n').Trim('\t');
            return string.Empty;
        }

        /// <summary>
        /// 去除字符串中的空格、回车、换行符、制表符
        /// </summary>
        public static string ClearTBBR(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return _tbbrRegex.Replace(str, "");

            return string.Empty;
        }

        /// <summary>
        /// 删除字符串中的空行
        /// </summary>
        /// <returns></returns>
        public static string DeleteNullOrSpaceRow(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";

            string[] tempArray = StringHelper.SplitString(s, "\r\n");
            StringBuilder result = new StringBuilder();
            foreach (string item in tempArray)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    result.AppendFormat("{0}\r\n", item);
            }
            if (result.Length > 0)
                result.Remove(result.Length - 2, 2);
            return result.ToString();
        }

        /// <summary>
        /// 获得指定数量的html空格
        /// </summary>
        /// <returns></returns>
        public static string GetHtmlBS(int count)
        {
            if (count == 1)
                return "&nbsp;&nbsp;&nbsp;&nbsp;";
            else if (count == 2)
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            else if (count == 3)
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            else
            {
                StringBuilder result = new StringBuilder();

                for (int i = 0; i < count; i++)
                    result.Append("&nbsp;&nbsp;&nbsp;&nbsp;");

                return result.ToString();
            }
        }

        /// <summary>
        /// 获得指定数量的htmlSpan元素
        /// </summary>
        /// <returns></returns>
        public static string GetHtmlSpan(int count)
        {
            if (count <= 0)
                return "";

            if (count == 1)
                return "<span></span>";
            else if (count == 2)
                return "<span></span><span></span>";
            else if (count == 3)
                return "<span></span><span></span><span></span>";
            else
            {
                StringBuilder result = new StringBuilder();

                for (int i = 0; i < count; i++)
                    result.Append("<span></span>");

                return result.ToString();
            }
        }

        /// <summary>
        ///获得邮箱提供者
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public static string GetEmailProvider(string email)
        {
            int index = email.LastIndexOf('@');
            if (index > 0)
                return email.Substring(index + 1);
            return string.Empty;
        }

        /// <summary>
        /// 转义正则表达式
        /// </summary>
        public static string EscapeRegex(string s)
        {
            string[] oList = { "\\", ".", "+", "*", "?", "{", "}", "[", "^", "]", "$", "(", ")", "=", "!", "<", ">", "|", ":" };
            string[] eList = { "\\\\", "\\.", "\\+", "\\*", "\\?", "\\{", "\\}", "\\[", "\\^", "\\]", "\\$", "\\(", "\\)", "\\=", "\\!", "\\<", "\\>", "\\|", "\\:" };
            for (int i = 0; i < oList.Length; i++)
                s = s.Replace(oList[i], eList[i]);
            return s;
        }

        /// <summary>
        /// 将ip地址转换成Int64类型
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static Int64 ConvertIPToInt64(string ip)
        {
            return Convert.ToInt64(ip.Replace(".", string.Empty));
        }

        /// <summary>
        /// 隐藏邮箱
        /// </summary>
        public static string HideEmail(string email)
        {
            int index = email.LastIndexOf('@');

            if (index == 1)
                return "*" + email.Substring(index);
            if (index == 2)
                return email[0] + "*" + email.Substring(index);

            StringBuilder sb = new StringBuilder();
            sb.Append(email.Substring(0, 2));
            int count = index - 2;
            while (count > 0)
            {
                sb.Append("*");
                count--;
            }
            sb.Append(email.Substring(index));
            return sb.ToString();
        }

        /// <summary>
        /// 隐藏手机
        /// </summary>
        public static string HideMobile(string mobile)
        {
            return mobile.Substring(0, 3) + "*****" + mobile.Substring(8);
        }

        /// <summary>
        /// 数据转换为列表
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static List<T> ArrayToList<T>(T[] array)
        {
            List<T> list = new List<T>(array.Length);
            foreach (T item in array)
                list.Add(item);
            return list;
        }
        /// <summary> 
        /// 取得HTML中所有图片的 URL。 
        /// </summary> 
        /// <param name="sHtmlText">HTML代码</param> 
        /// <returns>图片的URL列表</returns> 
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }
        /// <summary>
        /// 去除HTML格式
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string DelHTML(string Htmlstring)//将HTML去除
        {
            #region
            //删除脚本
            if (string.IsNullOrEmpty(Htmlstring))
            {
                return "";
            }
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //删除HTML

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"-->", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<!--.*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<A>.*</A>","");

            //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<[a-zA-Z]*=\.[a-zA-Z]*\?[a-zA-Z]+=\d&\w=%[a-zA-Z]*|[A-Z0-9]","");



            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(amp|#38);", "&", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(lt|#60);", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(gt|#62);", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&#(\d+);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);


            Htmlstring.Replace("<", "");

            Htmlstring.Replace(">", "");

            Htmlstring.Replace("\r\n", "");

            //Htmlstring=HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            #endregion


            return Htmlstring;

        }


        /// <summary>
        /// 本地保存路径
        /// </summary>
        /// <param name="url">网址图片路径</param>
        /// <param name="path">要保存的本地路径</param>
        public static int DownloadImage(string url, string path)
        {

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            //myReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";
            //myReq.Method = "GET";
            req.Method = "GET";
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.Headers.Add("Accept-Language", "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
            req.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:12.0) Gecko/20100101 Firefox/12.0";

            req.Timeout = 10000;
            req.Credentials = CredentialCache.DefaultCredentials;
            req.Referer = "http://image.baidu.com/";//欺骗网站服务器这是从百度图片发出的  
            req.KeepAlive = true;
            req.ContentType = "image/*";
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            System.IO.Stream stream = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                Image.FromStream(stream).Save(path);
            }
            catch (Exception ee)
            {
                return -1;
            }
            finally
            {
                // 释放资源
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
            return 1;
        }


        public static string DownloadImageConvertURL(string html)
        {

            string dirPath = IOHelper.GetMapPath("/upload/downloadImg/");

            html = html.Replace("amp;", "").Replace(@"\", "");

            //  temp = Regex.Replace(temp, @"(?i)(?<=<img\b[^>]*?src=\s*(['""]?))([^'""]*/)+(?=[^'""/]+\1)", "/Images/");

            // 定义正则表达式用来匹配 img 标签             
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串             
            MatchCollection matches = regImg.Matches(html);

            int i = 0;
            string[] UrlList = new string[matches.Count];

            int sum = 1;
            // 取得匹配项列表             
            foreach (Match match in matches)
            {
                string replacePach = "/upload/downloadImg/";
                sum++;
                UrlList[i++] = match.Groups["imgUrl"].Value;

                string datetime = DateTime.Now.ToString("yyyyMMddHHmmss") + sum + ".jpeg";

                string pathDate = dirPath + datetime;

                replacePach = replacePach + datetime;  //添加文件名称

                string sourceURL = match.Groups["imgUrl"].Value;
                //下载图片
                if (sourceURL.Contains("http"))
                {
                    int result = DownloadImage(sourceURL, pathDate);
                    if (result < 0)
                    {
                        result = DownloadImage(sourceURL, pathDate);
                        //替换标签
                        html.Replace(sourceURL, replacePach);
                    }
                    else
                    {
                        html = html.Replace(sourceURL, replacePach);
                    }
                    Thread.Sleep(100);
                }
            }
            return html;
        }

        /// <summary>
        /// 大文件下载
        /// </summary>
        /// <param name="tempFilePath"></param>
        /// <param name="ZSFilePath"></param>
        /// <param name="fileData"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool SaveFileToServer(string tempFilePath, string ZSFilePath, FileDataInfo fileData, HttpPostedFileBase file,ref bool  result)
        {
            if (!System.IO.File.Exists(tempFilePath))//如果文件不存在则保存一个
            {
                if (fileData.fileSize == fileData.fileEnd)
                {
                    file.SaveAs(ZSFilePath);
                }
                else
                {
                    IsFileInUse(tempFilePath);
                    file.SaveAs(tempFilePath);
                }
                return true;
            }
            try
            {
                IsFileInUse(tempFilePath);
                using (FileStream fStream = new FileStream(tempFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    //偏移指针
                    fStream.Seek(fileData.fileStart, SeekOrigin.Begin);
                    //从客户端的请求中获取文件流
                    using (BinaryReader bReader = new BinaryReader(file.InputStream))
                    {
                        long upLoadLength = file.InputStream.Length;//文件的长度
                        byte[] data = new byte[upLoadLength];
                        bReader.Read(data, 0, (int)upLoadLength);//读取上传文件
                        fStream.Write(data, 0, (int)upLoadLength);//将读取的文件写入服务器   
                        bReader.Dispose();
                        bReader.Close();
                    }
                    fStream.Dispose();
                    fStream.Close();
                }
            }
            catch (Exception)
            {
                System.IO.FileInfo fileInfo = new FileInfo(tempFilePath);
                fileInfo.Delete();//删除源文件
                throw;
            }
            //转移文件
            if (fileData.fileSize == fileData.fileEnd)//判断文件大小是不是等于文件结束大小
            {
                System.IO.FileInfo fileInfo = new FileInfo(tempFilePath);
                fileInfo.CopyTo(ZSFilePath);//另存为正式目录
                fileInfo.Delete();//删除源文件
                result = true;
            }
            return true;
        }



        private static void IsFileInUse(string fileName)
        {
            bool inUse = true;
            for (int i = 0; i >= 0; i++)
            {
                FileStream fs = null;
                try
                {
                    if (System.IO.File.Exists(fileName))//如果文件不存在则保存一个
                    {
                        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    }
                    inUse = false;
                }
                catch { }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
                if (inUse)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    return;
                }
            }
        }
        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }



    }
}
