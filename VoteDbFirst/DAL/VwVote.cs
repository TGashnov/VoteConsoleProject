using System;
using System.Collections.Generic;

#nullable disable

namespace VoteDbFirst.DAL
{
    public partial class VwVote
    {
        public string Question { get; set; }
        public string Note { get; set; }
        public string Answers { get; set; }
        public string Tags { get; set; }
        public int? VoteRating { get; set; }
        public string Status { get; set; }
    }
}
