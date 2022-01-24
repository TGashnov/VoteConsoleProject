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
    [Authorize(Roles ="Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        readonly VotesService service;

        public VoteController(VotesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<ViewVote>> GetRecommended()
        {
            return await service.GetRecommendedAsync();
        }

        // GET: VoteController
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ViewVote>>> Get(int? statusId, string orderBy, string orderAsc,
            string searchBy, string search)
        {
            if (statusId <= 0 || statusId > 3)
                return BadRequest("Введите цифру статуса от 1 до 3");

            bool sortAsc = orderAsc?.ToLower() == "asc";
            bool sortDesc = orderAsc?.ToLower() == "desc";
            return Ok(sortAsc || sortDesc ?
                await service.GetAllAsync(statusId, orderBy?.ToLower(), sortAsc, searchBy?.ToLower(), search)
                : await service.GetAllAsync(statusId, orderBy?.ToLower(), null, searchBy?.ToLower(), search));
        }

        [HttpGet("statuses")]
        public async Task<IEnumerable<VoteStatusApiDto>> GetStatuses()
        {
            return await service.GetAllStatAsync();
        }

        // GET: VoteController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewVote>> Get(int id)
        {
            var vote = await service.GetAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            return Ok(vote);
        }

        [Authorize(Roles = "Admin")]
        // POST: VoteController
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VoteApiDto vote)
        {
            var e = await service.CreateAsync(vote);
            if (e != null)
            {
                return StatusCode(500, e);
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        // PUT: VoteController/5/status/(publish || close)
        [HttpPut("{id}/status/{status}")]
        public async Task<ActionResult> PutStatus(int id, string status)
        {
            int statusToChange;
            switch (status)
            {
                case "publish": statusToChange = 2; break;
                case "close": statusToChange = 3; break;
                default: return BadRequest("Укажите правильно статус");
            }
            (var vote, var e) = await service.ChangeStatusAsync(id, statusToChange);
            if (e is Exception)
            {
                if (e is KeyNotFoundException)
                {
                    return NotFound(e?.Message);
                }
                if (e is SaveChangesException)
                {
                    return StatusCode(500, e.Message);
                }
                if (e is PubliсationException)
                {
                    return StatusCode(500, e.Message);
                }
                if (e is ClosingException)
                {
                    return StatusCode(500, e.Message);
                }
                return StatusCode(500);
            }
            if (vote == null)
            {
                return NotFound(e?.Message);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] VoteApiDto vote)
        {
            var e = await service.UpdateAsync(id, vote);
            if (e != null || e is Exception)
            {
                if (e is ArgumentException)
                {
                    return BadRequest(e.Message);
                }
                if (e is KeyNotFoundException)
                {
                    return NotFound(e.Message);
                }
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPut("dovote/{id}/{answer}")]
        public async Task<ActionResult> DoVote(long id, int answer)
        {
            var e = await service.DoVoteAsync(id, answer);
            if (e != null)
            {
                if (e is SaveChangesException)
                {
                    return StatusCode(500, e.Message);
                }
                if (e is KeyNotFoundException)
                {
                    return NotFound(e.Message);
                }
                if (e is ArgumentOutOfRangeException)
                {
                    return BadRequest(e.Message);
                }
                return StatusCode(500);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        // DELETE: VoteController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VoteApiDto>> Delete(long id)
        {
            (var vote, var e) = await service.DeleteAsync(id);
            if (e != null)
            {
                if (e is DeleteException)
                {
                    return StatusCode(500, e.Message);
                }
                if (e is SaveChangesException)
                {
                    return StatusCode(500, e.Message);
                }
                if (e is KeyNotFoundException)
                {
                    return NotFound(e.Message);
                }
                return StatusCode(500);
            }
            if (vote == null)
            {
                return NotFound(e?.Message);
            }

            return Ok(vote);
        }
    }
}
