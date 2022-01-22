using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteWebApi.BL.Model
{
    public class VoteApiDto
    {
        public long VoteId { get; set; }
        public string Question { get; set; }
        public string? Note { get; set; }
        public int? NumberOfVoters { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Published { get; set; }
        public VoteStatusApiDto Status { get; set; }
        public IEnumerable<TagApiDto> Tags { get; set; }
        public IEnumerable<AnswerApiDto> Answers { get; set; }
        public double Rating { get; set; }

        public VoteApiDto() { }

        public VoteApiDto(VoteDbDTO vote)
        {
            VoteId = vote.VoteId;
            Question = vote.Question;
            Note = vote.Note;
            NumberOfVoters = vote.NumberOfVoters;
            Created = vote.Created;
            Published = vote.Published;
            Status = new VoteStatusApiDto(vote.VoteStatus);
            Answers = vote.Answers.Select(ans => new AnswerApiDto(ans, (int)NumberOfVoters));
            Tags = vote.Tags.Select(t => new TagApiDto(t));
            Rating = VoteRating(vote);
        }

        public VoteDbDTO Create()
        {
            return new VoteDbDTO()
            {
                Question = Question,
                Note = Note,
                NumberOfVoters = 0,
                Created = DateTime.Now,
                VoteStatusId = 1,
                Tags = Tags.Select(t => t.Create()).ToList(),
                Answers = Answers.Select(ans => ans.Create()).ToList()
            };
        }

        public void Update(VoteDbDTO vote)
        {
            vote.Question = Question;
            vote.Note = Note;
        }

        public static void Publish(VoteDbDTO vote)
        {
            vote.VoteStatusId = 2;
            vote.Published = DateTime.Now;
        }

        public static void Close(VoteDbDTO vote)
        {
            vote.VoteStatusId = 3;
        }

        private double VoteRating(VoteDbDTO vote)
        {
            TimeSpan ts = DateTime.Now - vote.Created;
            double day = Math.Ceiling(ts.TotalDays);
            return (double)(vote.NumberOfVoters / day);
        }
    }

    public class ViewVote
    {
        public long VoteId { get; set; }
        public string Question { get; set; }
        public string? Note { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Published { get; set; }
        public double Rating { get; set; }
        public ViewStatus Status { get; set; }
        public IEnumerable<ViewTag> Tags { get; set; }
        public IEnumerable<ViewAnswer> Answers { get; set; }

        public ViewVote(VoteApiDto vote)
        {
            VoteId = vote.VoteId;
            Question = vote.Question;
            Note = vote.Note;
            Created = vote.Created;
            Published = vote.Published;
            Status = new ViewStatus(vote.Status);
            Rating = vote.Rating;
            Answers = vote.Answers.Select(ans => new ViewAnswer(ans));
            Tags = vote.Tags.Select(t => new ViewTag(t));
        }
    }
}
