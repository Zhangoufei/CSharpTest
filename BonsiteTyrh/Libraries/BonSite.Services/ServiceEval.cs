using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BonSite.Core;

namespace BonSite.Services
{
    public class ServiceEval
    {

        #region 读写删
        /// <summary>
        /// 新加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create(ServiceEvalInfo model)
        {
            return BonSite.Data.ServiceEval.Create(model);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceEvalInfo GetModelByID(int id)
        {
            if (id > 0)
                return BonSite.Data.ServiceEval.GetModelById(id);
            return null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(ServiceEvalInfo model)
        {
            return BonSite.Data.ServiceEval.Update(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public static bool Del(int[] idlist)
        {
            if (idlist != null && idlist.Length > 0)
            {
                bool isSucces = BonSite.Data.ServiceEval.Delete(CommonHelper.IntArrayToString(idlist));
                return isSucces;
            }
            else
            {
                return false;
            }
        }
        #endregion



        public static DataTable AdminGetServiceEvalList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BonSite.Data.ServiceEval.AdminGetServiceEvalList(pageSize, pageNumber, condition, sort);
        }


        public static int AdminGetServiceEvalCount(string condition)
        {
            return BonSite.Data.ServiceEval.AdminGetServiceEvalCount(condition);
        }

        public static string AdminGetServiceEvalListSort(string sortColumn, string sortDirection)
        {
            return BonSite.Data.ServiceEval.AdminGetServiceEvalListSort(sortColumn, sortDirection);
        } 

        public static string AdminGetServiceEvalListCondition(string condition)
        {
            return BonSite.Data.ServiceEval.AdminGetServiceEvalListCondition(condition);
        }
    }
}
