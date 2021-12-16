using System;
using System.Collections.Generic;

#nullable disable

namespace VoteDbFirst.DAL
{
    public sealed class VoteStatus
    {
        public VoteStatus()
        {
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
