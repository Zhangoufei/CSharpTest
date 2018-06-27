using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BonSite.Core.Domain.Site
{
     public  class RoleMenuInfo
    {
         private int _id;//ID
         private int _ArticleClassId;//分类ID
         private int _roleId;//角色ID

         /// <summary>
         /// ID
         /// </summary>
         public int Id
         {
             get { return _id; }
             set { _id = value; }
         }

         /// <summary>
         /// 分类ID
         /// </summary>
         public int ArticleClassID
         {
             get { return _ArticleClassId; }
             set { _ArticleClassId = value; }
         }

         /// <summary>
         /// 用户ID
         /// </summary>
         public int RoleId
         {
             get { return _roleId; }
             set { _roleId = value; }
         }
    
    }
}
