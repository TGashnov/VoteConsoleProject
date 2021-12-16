using System;
using System.Collections.Generic;

#nullable disable

namespace VoteDbFirst.DAL
{
    public sealed class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int NumberOfVoters { get; set; }
        public int VoteId { get; set; }

        public Vote Vote { get; set; }
    }
}
