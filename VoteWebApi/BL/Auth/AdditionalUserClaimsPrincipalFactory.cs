﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteWebApi.BL.Auth
{
    public class AdditionalUserClaimsPrincipalFactory:
        UserClaimsPrincipalFactory<UserDbDTO, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(UserManager<UserDbDTO> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(UserDbDTO user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity) principal.Identity;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim("FirstName", user.FirstName ?? ""),
                new Claim("MiddleName", user.MiddleName ?? ""),
                new Claim("LastName", user.LastName ?? ""),
                new Claim(ClaimTypes.Role, string.Join(",", await UserManager.GetRolesAsync(user)))
            };

            identity.AddClaims(claims);
            return principal;
        }
    }
}
