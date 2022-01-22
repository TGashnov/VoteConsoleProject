using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteWebApi.BL.Model
{
    public class AnswerApiDto
    {
        public long AnsId { get; set; }
        public string Text { get; set; }
        public int NumberOfVoters { get; set; }
        public double Rating { get; set; }

        public AnswerApiDto() { }

        public AnswerApiDto(AnswerDbDTO ans, int totalNumberOfVoters)
        {
            AnsId = ans.AnsId;
            Text = ans.Text;
            Rating = AnswerRating(ans, totalNumberOfVoters);
        }

        public AnswerDbDTO Create()
        {
            return new AnswerDbDTO()
            {
                AnsId = AnsId,
                NumberOfVoters = NumberOfVoters,
                Text = Text
            };
        }

        public void Update(AnswerDbDTO ans)
        {
            ans.Text = Text;
        }

        private double AnswerRating(AnswerDbDTO answer, int totalNumberOfVoters)
        {
            if (answer.NumberOfVoters == 0) return 0;
            return Math.Round((double)answer.NumberOfVoters / totalNumberOfVoters, 2);
        }
    }

    public class ViewAnswer
    {
        public string Text { get; set; }
        public double Rating { get; set; }

        public ViewAnswer(AnswerApiDto ans)
        {
            Text = ans.Text;
            Rating = ans.Rating;
        }
    }
}
