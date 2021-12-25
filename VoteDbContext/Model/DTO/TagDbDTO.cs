using System;
using System.Collections.Generic;
using System.Text;

namespace VoteDbContext.Model.DTO
{
    public class TagDbDTO
    {
        public long TagId { get; set; }
        public string Text { get; set; }

        public ICollection<VoteDbDTO> Votes { get; set; } = new List<VoteDbDTO>();
    }
}
