using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VoteModel
{
    public class Vote 
    {
        public long Id { get; set; }
        public Question Question { get; } = new Question();
        public List<Answer> Answers { get; } = new List<Answer>();
        public List<Tag> Tags { get; } = new List<Tag>();
        public DateTime Created { get; } = DateTime.Now;
        public DateTime? Published { get; private set; }
        public int NumberOfVotersForVote { get; private set; } = 0;

        ////private double voteRating;
        //public double VoteRating
        //{
        //    get => VoteRating;
        //    set
        //    {
        //        TimeSpan ts = DateTime.Now - Created;
        //        double day = Math.Ceiling(ts.TotalDays);
        //        VoteRating = NumberOfVotersForVote / day;
        //    }
        //}

        public Vote(string question,  List<Answer> answers, List<Tag> tags, string note = null)
        {
            Question.Text = question;
            Question.Note = note;
            Answers = answers.ToList();
            Tags = tags.ToList();
        }

        public Vote(string question, List<Answer> answers, List<Tag> tags, VoteStatus status, string note = null)
        {
            Question.Text = question;
            Question.Note = note;
            Answers = answers.ToList();
            Tags = tags.ToList();
            if (status == VoteStatus.Published || status == VoteStatus.Closed) 
            {
                FinishPreparation();
                if (status == VoteStatus.Closed) Close();
            }
            else
            {
                Status = status;
            }
        }

        public Vote(string question, List<Answer> answers, List<Tag> tags, VoteStatus status, DateTime created, DateTime? published, int number, long id, string note = null)
        {
            Question.Text = question;
            Question.Note = note;
            Answers = answers.ToList();
            Tags = tags.ToList();
            Status = status;
            Created = created;
            if(status != VoteStatus.Preparation) Published = published;
            NumberOfVotersForVote = number;
            Id = id;
        }

        public double VoteRating()
        {
            TimeSpan ts = DateTime.Now - Created;
            double day = Math.Ceiling(ts.TotalDays);
            return NumberOfVotersForVote / day;
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
            if (Published != null) sb.Append("Дата публикации: " + Published?.ToLocalTime() + "\n");
            sb.Append("Рейтинг данного голосования: " + VoteRating() + "\n");
            sb.Append("Статус: " + VoteStatusRus.Names[Status] + "\n");
            sb.Append(Question.Text + "\n");
            if (Question.Note != null) sb.Append("(Примечание) " + Question.Note + "\n");
            for (int i = 0; i < Answers.Count; i++)
            {
                sb.Append(string.Format("{0}. {1} {2,10} \n",i+1, Answers[i], AnswerRating(i)));
            }
            return sb.ToString();
        }

        public void ChangeAnswers(List<Answer> list)
        {
            Answers.Clear();
            Answers.InsertRange(0, list);
        }

        public void ChangeTags(List<Tag> list)
        {
            Tags.Clear();
            Tags.InsertRange(0, list);
        }

        public VoteStatus Status { get; private set; } = VoteStatus.Preparation;

        public void FinishPreparation()
        {
            Status = VoteStatus.Published;
            Published = DateTime.Now;
        }
        public void Close()
        {
            Status = VoteStatus.Closed;
        }

    }
}