<%@ WebHandler Language="C#" Class="imageManager" %>
/**
 * Created by visual studio2010
 * User: xuheng
 * Date: 12-3-7
 * Time: 下午16:29
 * To change this template use File | Settings | File Templates.
 */
using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using BonSite.Core;
public class imageManager : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string[] paths = { "upload" }; //需要遍历的目录列表，最好使用缩略图地址，否则当网速慢时可能会造成严重的延时
        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };                //文件允许格式

        string action = context.Server.HtmlEncode(context.Request["action"]);

        if (action == "get")
        {
            String str = String.Empty;
            string temp = IOHelper.GetMapPath(BSConfig.SiteConfig.OnlineImage);//临时保存路径
            foreach (string path in paths)
            {
                DirectoryInfo info = new DirectoryInfo(temp);
                //目录验证
                if (info.Exists)
                {
                         int sum = 0;
                    /**
                     * 不再检索下面是否有子目录
                    DirectoryInfo[] infoArr = info.GetDirectories();
                    foreach (DirectoryInfo tmpInfo in infoArr)
                    {
                        foreach (FileInfo fi in tmpInfo.GetFiles())
                        {
                            if (Array.IndexOf(filetype, fi.Extension) != -1)
                            {
                                if (sum == 20) break;
                                sum++;
                                //str += path + "/" + tmpInfo.Name + "/" + fi.Name + "ue_separate_ue";
                                str += fi.Name + "ue_separate_ue";
                            }
                        }
                    }
                     */
                    /**
                     * 直接检索文件
                     **/
                    foreach (FileInfo fi in info.GetFiles())
                    {
                        if (Array.IndexOf(filetype, fi.Extension) != -1)
                        {
                            if (sum == 50) break;
                            sum++;
                            str += fi.Name + "ue_separate_ue";
                        }
                    }
                }
            }
            context.Response.Write(str);
        }
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}