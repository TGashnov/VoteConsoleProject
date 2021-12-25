using System;
using System.Collections.Generic;
using System.Text;

namespace VoteDbContext.Model.DTO
{
    public class AnswerDbDTO
    {
        public long AnsId { get; set; }
        public string Text { get; set; }
        public int NumberOfVoters { get; set; }
        
        public VoteDbDTO Vote { get; set; }
    }
}
