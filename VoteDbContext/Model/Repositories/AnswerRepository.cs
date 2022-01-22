using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model.Repositories
{
    public class AnswerRepository : VoteRepositoryBase
    {
        public AnswerRepository(VoteContext context) : base(context) { }

        public async Task<IEnumerable<AnswerDbDTO>> GetAllAsync()
        {
            return await context.Answers.ToListAsync();
        }
    }
}
