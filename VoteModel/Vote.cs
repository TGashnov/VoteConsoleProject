using System;
using System.Collections.Generic;
using System.Text;

namespace VoteModel
{
    public class Vote
    {
        public Question Question { get; } = new Question();

        public List<Answer> Answers { get; } = new List<Answer>();

        public List<Tag> Tags { get; } = new List<Tag>();

        public DateTime Created { get; } = DateTime.Now;

        public int NumberOfVotersForVote { get; private set; } = 0;

        public double VoteRating()
        {
            TimeSpan ts = DateTime.Now - Created;
            double day = ts.Days + 1;
            return NumberOfVotersForVote / day;
        }

        public bool AnswersContain(Answer answer)
        {
            for (int i = 0; i < Answers.Count; i++)
            {
                if (Answers.Contains(answer))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TagsContain(Tag tag)
        {
            for (int i = 0; i < Tags.Count; i++)
            {
                if (Tags.Contains(tag))
                {
                    return true;
                }
            }
            return false;
        }

        public double AnswerRating(int index)
        {
            if (NumberOfVotersForVote == 0) return 0;
            return Math.Round((double)Answers[index].NumberOfVoters / NumberOfVotersForVote, 2);
        }

        public void AcceptAnswer(int index)
        {
            Answers[index].NumberOfVoters++;
            NumberOfVotersForVote++;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Дата создания: " + Created.ToLocalTime() + "\n");
            sb.Append("Рейтинг данного голосования: " + VoteRating() + "\n");
            sb.Append(Question + "\n");
            for (int i = 0; i < Answers.Count; i++)
            {
                sb.Append(Answers[i] + "\t" + AnswerRating(i) + "\n");
            }
            return sb.ToString();
        }

        public VoteStatus Status { get; private set; } = VoteStatus.Preparation;

        public void FinishPreparation()
        {
            Status = VoteStatus.Published;
        }
        public void Close()
        {
            Status = VoteStatus.Closed;
        }
    }
}