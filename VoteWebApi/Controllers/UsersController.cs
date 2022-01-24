using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteWebApi.BL.Exceptions;
using VoteWebApi.BL.Model;
using VoteWebApi.BL.Services;

namespace VoteWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        // GET: UsersController
        [HttpGet]
        public async Task<IEnumerable<UserApiDto>> Get()
        {
            return await usersService.GetUsers();
        }

        // GET: UsersController/5
        [HttpGet("{userName}")]
        public async Task<ActionResult<UserApiDto>> Get(string userName)
        {
            var user = await usersService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: UsersController
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserCreateApiDto user)
        {
            var result = await usersService.Create(user);
            if (result is KeyNotFoundException)
            {
                return NotFound(result.Message);
            }
            if (result is AlreadyExistsException)
            {
                return Conflict(result.Message);
            }
            if (result != null)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserCreateApiDto user)
        {
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

        [HttpDelete("{userName}")]
        public async Task<ActionResult> Delete(string userName)
        {
            var result = await usersService.Delete(userName);
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

        [HttpPost("{userName}/role/{role}")]
        public async Task<ActionResult> PostRole(string userName, string role)
        {
            var result = await usersService.AssignRole(userName, role);
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

        [HttpDelete("{userName}/role/{role}")]
        public async Task<ActionResult> DeleteRole(string userName, string role)
        {
            var result = await usersService.RemoveFromRole(userName, role);
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
    }
}
