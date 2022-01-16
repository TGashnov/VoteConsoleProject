using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Model.DTO;
using VoteModel;

namespace VoteConsoleProject.DB.Mapping
{
    static class AnswerMapper
    {
        public static Answer Map(AnswerDbDTO ans)
        {
            if (ans == null)
                return null;

            int num;
            if (ans.NumberOfVoters == null)
                num = 0;
            else
                num = (int)ans.NumberOfVoters;

            return new Answer(
                ans.AnsId,
                ans.Text,
                num
                );
        }

        public static AnswerDbDTO Map(Answer ans)
        {
            if (ans == null)
                return null;

            return new AnswerDbDTO()
            {
                AnsId = ans.AnsId,
                Text = ans.AnswerText,
                NumberOfVoters = ans.NumberOfVoters
            };
        }
    }
}
