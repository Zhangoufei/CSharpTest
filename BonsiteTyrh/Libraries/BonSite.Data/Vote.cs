using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BonSite.Core;

namespace BonSite.Data
{
    public class Vote
    {
        #region 辅助方法
        public static VoteInfo BuildVoteFromReader(IDataReader reader)
        {
            VoteInfo voteInfo = new VoteInfo
            {
                Id = TypeHelper.ObjectToInt(reader["Id"]),
                Title = reader["Title"].ToString(),
                StartTime = TypeHelper.ObjectToDateTime(reader["StartTime"]),
                EndTime = TypeHelper.ObjectToDateTime(reader["EndTime"]),
                State = TypeHelper.ObjectToInt(reader["State"]),
                Type = TypeHelper.ObjectToInt(reader["Type"]),
                ResultList = GetVoteResultsList(TypeHelper.ObjectToInt(reader["Id"]))
            };
            return voteInfo;
        }

        public static VoteResultInfo BuildVoteResultFromReader(IDataReader reader)
        {
            VoteResultInfo voteResultInfo = new VoteResultInfo
            {
                Id = TypeHelper.ObjectToInt(reader["Id"]),
                VoteId = TypeHelper.ObjectToInt(reader["VoteId"]),
                DisplayOrder = TypeHelper.ObjectToInt(reader["DisplayOrder"]),
                Result = reader["Result"].ToString(),
                Count = TypeHelper.ObjectToInt(reader["Count"])
            };
            return voteResultInfo;
        }
        #endregion


        public static VoteInfo GetVote(int Id)
        {
            VoteInfo voteInfo = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetVote(Id);
            if (reader.Read())
            {
                voteInfo = BuildVoteFromReader(reader);
            }
            reader.Close();
            return voteInfo;
        }

        public static List<VoteResultInfo> GetVoteResultsList(int VoteId)
        {
            List<VoteResultInfo> voteResultInfoList = new List<VoteResultInfo>();
            IDataReader reader = BonSite.Core.BSData.RDBS.GetVoteResultsList(VoteId);
            while (reader.Read())
            {
                VoteResultInfo voteResultInfo = BuildVoteResultFromReader(reader);
                voteResultInfoList.Add(voteResultInfo);
            }
            reader.Close();
            return voteResultInfoList;
            
        }

        public static VoteResultInfo GetVoteResult(int Id)
        {
            VoteResultInfo voteResultInfo = null;
            IDataReader reader = BonSite.Core.BSData.RDBS.GetVoteResult(Id);
            if (reader.Read())
            {
                voteResultInfo = BuildVoteResultFromReader(reader);
            }
            reader.Close();
            return voteResultInfo;
        }

        public static int AdminGetVoteCount()
        {
            return BonSite.Core.BSData.RDBS.AdminGetVoteCount();
        }

        public static DataTable AdminGetVoteList(int pageSize, int pageNumber)
        {
            return BonSite.Core.BSData.RDBS.AdminGetVoteList(pageSize, pageNumber);
        }

        //public VoteResultInfo GetVoteResults(int Id)
        //{
        //    VoteResultInfo voteResultInfo = null;
        //    IDataReader reader = BonSite.Core.BSData.RDBS.GetVoteResults(Id);
        //    if (reader.Read())
        //    {
        //        voteResultInfo = BuildVoteResultFromReader(reader);
        //    }
        //    reader.Close();
        //    return voteResultInfo;

        //}


        public static int CreateVote(VoteInfo voteInfo)
        {
            return BonSite.Core.BSData.RDBS.CreateVote(voteInfo);
        }

        public static void UpdateVote(VoteInfo voteInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateVote(voteInfo);
        }

        public static void DeleteVote(int Id)
        {
            BonSite.Core.BSData.RDBS.DeleteVote(Id);
        }

        public static void CreateVoteResults(VoteResultInfo voteResultInfo)
        {
            BonSite.Core.BSData.RDBS.CreateVoteResults(voteResultInfo);
        }

        public static void UpdateVoteResults(VoteResultInfo voteResultInfo)
        {
            BonSite.Core.BSData.RDBS.UpdateVoteResults(voteResultInfo);
        }

        public static void DeleteVoteResults(int Id)
        {
            BonSite.Core.BSData.RDBS.DeleteVoteResult(Id);
        }

        public static void VoteResults(int ResultsId)
        {
            BonSite.Core.BSData.RDBS.VoteResults(ResultsId);
        }
    }
}
