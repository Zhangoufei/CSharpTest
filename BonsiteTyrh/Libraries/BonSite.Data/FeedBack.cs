using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using BonSite.Core;

namespace BonSite.Data
{
    public class FeedBack
    {
        #region 辅助方法
        public static FeedBackTypeInfo BuildFeedBackTypeFromReader(IDataReader reader)
        {
            FeedBackTypeInfo feedBackTypeInfo = new FeedBackTypeInfo
            {
                FeedBackTypeId = TypeHelper.ObjectToInt(reader["FeedBackTypeId"]),
                TypeName = reader["TypeName"].ToString(),
                IsShowList = TypeHelper.ObjectToInt(reader["IsShowList"]),
                Body = reader["Body"].ToString(),
                Tags = reader["Tags"].ToString()
            };
            return feedBackTypeInfo;
        }

        public static FeedBackInfo BuildFeedBackFromReader(IDataReader reader)
        {
            FeedBackInfo feedBackInfo = new FeedBackInfo
            {
                Id = TypeHelper.ObjectToInt(reader["id"]),
                FeedBackTypeId = TypeHelper.ObjectToInt(reader["feedbacktypeid"]),
                Tag = reader["tag"].ToString(),
                LinkMan = reader["linkman"].ToString(),
                Tel = reader["tel"].ToString(),
                Mobile = reader["Mobile"].ToString(),
                Email = reader["email"].ToString(),
                Title = reader["title"].ToString(),
                Body = reader["Body"].ToString(),
                AddTime = TypeHelper.ObjectToDateTime(reader["AddTime"]),
                Reply = reader["reply"].ToString(),
                ReplyTime = TypeHelper.ObjectToDateTime(reader["ReplyTime"]),
                State = TypeHelper.ObjectToInt(reader["State"]),
                IsOut = TypeHelper.ObjectToInt(reader["isOut"]),
                Ip = reader["IP"].ToString(),
                SearchKey = reader["SearchKey"].ToString()
            };
            return feedBackInfo;
        }
        #endregion

        public static FeedBackTypeInfo GetFeedBackTypeById(int Id)
        {
            FeedBackTypeInfo feedbackTypeInfo = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetFeedBackType(Id);
            if (reader.Read())
            {
                feedbackTypeInfo = BuildFeedBackTypeFromReader(reader);
            }
            reader.Close();
            return feedbackTypeInfo;
        }

        public static FeedBackInfo GetFeedBackById(int Id)
        {
            FeedBackInfo feedbackInfo = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetFeedBack(Id);
            if (reader.Read())
            {
                feedbackInfo = BuildFeedBackFromReader(reader);
            }
            reader.Close();
            return feedbackInfo;
        }


        public static FeedBackInfo GetFeedBackBySearchKey(string searchKey)
        {
            FeedBackInfo feedbackInfo = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetFeedBackBySearchKey(searchKey);
            if (reader.Read())
            {
                feedbackInfo = BuildFeedBackFromReader(reader);
            }
            reader.Close();
            return feedbackInfo;
            
        }
          



        public static void CreateFeedBackType(FeedBackTypeInfo feedbackTypeInfo)
        {
            BonSite.Core.BSData.RDBS.CreateFeedBackType(feedbackTypeInfo);
        }

        public static void UpdateFeedBackType(FeedBackTypeInfo feedbackTypeInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateFeedBackType(feedbackTypeInfo);
        }

        public static void DeleteFeedBackType(int id)
        {
            BonSite.Core.BSData.RDBS.DeleteFeedBackType(id);
        }

        public static void CreateFeedBackInfo(FeedBackInfo feedbackInfo)
        {
            BonSite.Core.BSData.RDBS.CreateFeedBack(feedbackInfo);
        }

        public static void UpdateFeedBackInfo(FeedBackInfo feedbackInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateFeedBack(feedbackInfo);
        }

        public static void DeleteFeedBackInfo(int Id)
        {
            BonSite.Core.BSData.RDBS.DeleteFeedBack(Id);
        }


        public static List<FeedBackTypeInfo> GetFeedBackTypeList()
        {
            List<FeedBackTypeInfo> feedBackTypeList = new List<FeedBackTypeInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetFeedBackTypeList();
            while (reader.Read())
            {
                FeedBackTypeInfo feedBackTypeInfo = BuildFeedBackTypeFromReader(reader);
                feedBackTypeList.Add(feedBackTypeInfo);
            }
            reader.Close();
            return feedBackTypeList;
        }

        public static int GetFeedBackListCount(int typeId)
        {
            return BonSite.Core.BSData.RDBS.GetFeedBackListCount(typeId);
        }

        public static DataTable GetFeedBackList(int PageSize, int pageNumber, int typeId)
        {
            return BonSite.Core.BSData.RDBS.GetFeedBackList(PageSize, pageNumber, typeId);
        }


        public static int AdminGetFeedBackListCount(int typeId)
        {
            return BonSite.Core.BSData.RDBS.AdminGetFeedBackListCount(typeId);
        }

        public static DataTable AdminGetFeedBackList(int pageSize, int pageNumber, int TypeID)
        {
            return BonSite.Core.BSData.RDBS.AdminGetFeedBackList(pageSize, pageNumber, TypeID);
        }
    }
}
