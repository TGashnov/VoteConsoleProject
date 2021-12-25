using System;
using System.Collections.Generic;
using System.Text;

namespace VoteDbContext.Model.DTO
{
    public class VoteStatusDbDTO
    {
        public int VSId { get; set; }
        public string Name { get; set; }

        public ICollection<VoteDbDTO> Votes { get; set; }
    }
}
