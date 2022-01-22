using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteWebApi.BL.Model
{
    public class VoteStatusApiDto
    {
        public int StatusId { get; set; }
        public string Name { get; set; }

        public VoteStatusApiDto() { }

        public VoteStatusApiDto(VoteStatusDbDTO status)
        {
            StatusId = status.VSId;
            Name = status.Name;
        }
    }

    public class ViewStatus
    {
        public string Name { get; set; }

        public ViewStatus(VoteStatusApiDto status)
        {
            Name = status.Name;
        }
    }
}
