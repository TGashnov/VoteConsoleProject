using System;
using System.Collections.Generic;

#nullable disable

namespace VoteDbFirst.DAL
{
    public sealed class Vote
    {
        public Vote()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string Note { get; set; }
        public int NumberOfVoters { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Published { get; set; }
        public int Status { get; set; }

        public VoteStatus StatusNavigation { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
