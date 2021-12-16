using System;
using System.Collections.Generic;

#nullable disable

namespace VoteDbFirst.DAL
{
    public sealed class VoteTag
    {
        public int Vote { get; set; }
        public int Tag { get; set; }

        public Tag TagNavigation { get; set; }
        public Vote VoteNavigation { get; set; }
    }
}
