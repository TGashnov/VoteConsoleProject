using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoteDbContext.Model.DTO
{
    public class UserDbDTO: IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public ICollection<VoteDbDTO> Votes { get; set; }
    }
}
