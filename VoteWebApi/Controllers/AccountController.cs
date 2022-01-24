using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;
using VoteWebApi.BL.Exceptions;
using VoteWebApi.BL.Model;
using VoteWebApi.BL.Services;

namespace VoteWebApi.Controllers
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class ChangePasswordRequest
    {
        public string NewPassword { get; set; }
    }

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UsersService usersService;
        private readonly SignInManager<UserDbDTO> signInManager;

        public AccountController(UsersService usersService, 
            SignInManager<UserDbDTO> signInManager)
        {
            this.usersService = usersService;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password,
                request.RememberMe, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        [HttpGet]
        public UserApiDto Get()
        {
            var identity = (ClaimsIdentity) HttpContext.User.Identity;
            var user = new UserApiDto()
            {
                UserName = identity.Name,
                Email = identity.FindFirst(ClaimTypes.Email)?.Value,
                FirstName = identity.FindFirst("FirstName")?.Value,
                MiddleName = identity.FindFirst("MiddleName")?.Value,
                LastName = identity.FindFirst("LastName")?.Value
            };
            return user;
        }

        [HttpGet("roles")]
        public string GetRoles()
        {
            var identity = (ClaimsIdentity) HttpContext.User.Identity;
            return identity.FindFirst(ClaimTypes.Role)?.Value;
        }

        [HttpPut]
        public async Task<ActionResult> PutProfile(UserApiDto user)
        {
            if (user.UserName != HttpContext.User.Identity.Name)
            {
                return BadRequest("Имя пользователя некорректно.");
            }
            var result = await usersService.UpdateUser(user);
            if (result is KeyNotFoundException)
            {
                return NotFound(result.Message);
            }
            if (result != null)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpPost("password")]
        public async Task<ActionResult> PostPassword([FromForm] string password)
        {
            var result = await usersService.ResetPassword(HttpContext.User.Identity.Name, password);
            if (result is KeyNotFoundException)
            {
                return NotFound(result.Message);
            }
            if (result is SaveChangesException)
            {
                return BadRequest(result.Message);
            }
            if (result != null)
            {
                return StatusCode(500, result.Message);
            }
            return Ok();
        }
    }
}
