using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoteDbContext.Model.DTO;
using VoteModel;

namespace VoteConsoleProject.DB.Mapping
{
    static class VoteMapper
    {
        public static Vote Map(VoteDbDTO vote)
        {
            if (vote == null)
                return null;

            VoteStatus status;
            if (vote.VoteStatusId == 1) status = VoteStatus.Preparation;
            else if (vote.VoteStatusId == 2) status = VoteStatus.Published;
            else status = VoteStatus.Closed;

            int num;
            if (vote.NumberOfVoters == null) num = 0;
            else num = (int)vote.NumberOfVoters;

            return new Vote(
                vote.Question,
                vote.Answers.Select(ans => AnswerMapper.Map(ans)).ToList(),
                vote.Tags.Select(t => TagMapper.Map(t)).ToList(),
                status,
                vote.Created,
                vote.Published,
                num,
                vote.VoteId,
                vote.Note
                );
        }

        public static VoteDbDTO Map(Vote vote)
        {
            if (vote == null)
                return null;

            return new VoteDbDTO()
            {
                VoteId = vote.Id,
                Question = vote.Question.Text,
                Answers = vote.Answers.Select(ans => AnswerMapper.Map(ans)).ToList(),
                Tags = vote.Tags.Select(t => TagMapper.Map(t)).ToList(),
                Created = vote.Created,
                Published = vote.Published,
                Note = vote.Question.Note,
                NumberOfVoters = vote.NumberOfVotersForVote,
                VoteStatusId = (int)vote.Status+1
            };
        }
    }
}
