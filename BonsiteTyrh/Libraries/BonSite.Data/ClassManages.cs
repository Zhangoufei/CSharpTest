using BonSite.Core;
using BonSite.Core.Domain.Site;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BonSite.Data
{
    public class ClassManages
    {
        /// <summary>
        /// 
        /// </summary>
        public static ClassManageInfo[] GetClassManageList()
        {
            DataTable dt = BonSite.Core.BSData.RDBS.GetClassManageList();
            ClassManageInfo[] classManageList = new ClassManageInfo[dt.Rows.Count];

            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                ClassManageInfo classManageInfo = new ClassManageInfo();
                classManageInfo.ClassID = TypeHelper.ObjectToInt(row["ClassID"]);
                classManageInfo.ClassName = row["ClassName"].ToString();
                classManageList[index] = classManageInfo;
                index++;
            }
            return classManageList;
        }
        /// <summary>
        /// 创建班级
        /// </summary>
        public static void CreateClassManage(ClassManageInfo classManageInfo)
        {
            BonSite.Core.BSData.RDBS.CreateClassManage(classManageInfo);
        }

        /// <summary>
        /// 获得班级
        /// </summary>
        /// <param name="ClassID">班级ClassID</param>
        /// <returns></returns>
        public static ClassManageInfo GetFriendLinkById(int ClassID)
        {
            foreach (ClassManageInfo classManageInfo in GetClassManageList())
            {
                if (classManageInfo.ClassID == ClassID)
                    return classManageInfo;
            }

            return null;
        }

        /// <summary>
        /// 更新班级
        /// </summary>
        public static void UpdateClassManage(ClassManageInfo classManageInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateClassManage(classManageInfo);
        }
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="idList">班级id</param>
        public static void DeleteClassManageById(string idList)
        {
            BonSite.Core.BSData.RDBS.DeleteClassManageById(idList);
        }
    }
}
