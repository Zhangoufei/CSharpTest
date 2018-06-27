using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BonSite.Core;

namespace BonSite.Services
{
    public class Vote
    {
        public static VoteInfo GetVote(int Id)
        {
            return BonSite.Data.Vote.GetVote(Id);
        }

        public static List<VoteResultInfo> GetVoteResultsList(int VoteId)
        {
            return BonSite.Data.Vote.GetVoteResultsList(VoteId);
        }

        public static int CreateVote(VoteInfo voteInfo)
        {
            return BonSite.Data.Vote.CreateVote(voteInfo);
        }

        public static void UpdateVote(VoteInfo voteInfo)
        {
            BonSite.Data.Vote.UpdateVote(voteInfo);
        }

        public static void DeleteVote(int Id)
        {
            BonSite.Data.Vote.DeleteVote(Id);
        }

        public static void CreateVoteResults(VoteResultInfo voteResultInfo)
        {
            BonSite.Data.Vote.CreateVoteResults(voteResultInfo);
        }

        public static void UpdateVoteResults(VoteResultInfo voteResultInfo)
        {
            BonSite.Data.Vote.UpdateVoteResults(voteResultInfo);
        }

        public static void DeleteVoteResults(int Id)
        {
            BonSite.Data.Vote.DeleteVoteResults(Id);
        }

        public static int AdminGetVoteCount()
        {
            return BonSite.Data.Vote.AdminGetVoteCount();
        }

        public static DataTable AdminGetVoteList(int pageSize, int pageNumber)
        {
            return BonSite.Data.Vote.AdminGetVoteList(pageSize, pageNumber);
        }

        public static VoteResultInfo GetVoteResult(int Id)
        {
            return BonSite.Data.Vote.GetVoteResult(Id);
        }

        public static void VoteResults(int ResultId)
        {
            BonSite.Data.Vote.VoteResults(ResultId);
        }
    }
}
