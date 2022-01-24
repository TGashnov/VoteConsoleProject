using System;
using System.Collections.Generic;
using System.Text;

namespace VoteDbContext.Model.DTO
{
    public class VoteDbDTO
    {
        public long VoteId { get; set; }
        public string Question { get; set; }
        public string? Note { get; set; }
        public int? NumberOfVoters { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Published { get; set; }

        public ICollection<AnswerDbDTO> Answers { get; set; } = new List<AnswerDbDTO>();
        public ICollection<TagDbDTO> Tags { get; set; } = new List<TagDbDTO>();

        public int VoteStatusId { get; set; }
        public VoteStatusDbDTO VoteStatus { get; set; } 

        public string UserId { get; set; }
        public UserDbDTO User { get; set; }
    }
}
