using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteWebApi.BL.Model
{
    public class UserApiDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public UserApiDto() { }

        public UserApiDto(UserDbDTO user)
        {
            UserName = user.UserName;
            Email = user.Email;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
        }

        public UserApiDto(UserDbDTO user, IEnumerable<string> roles) : this(user)
        {
            Roles = roles;
        }

        public void Update(UserDbDTO user)
        {
            user.Email = Email;
            user.FirstName = FirstName;
            user.MiddleName = MiddleName;
            user.LastName = LastName;
        }

        public UserDbDTO Create()
        {
            return new UserDbDTO()
            {
                UserName = UserName,
                Email = Email,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName
            };
        }
    }

    public class UserCreateApiDto: UserApiDto
    {
        public string Password { get; set; }
    }
}
