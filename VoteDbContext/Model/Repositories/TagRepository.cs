using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model.Repositories
{
    public class TagRepository : VoteRepositoryBase
    {
        public TagRepository(VoteContext context) : base(context) { }

        public async Task<IEnumerable<TagDbDTO>> GetAllAsync()
        {
            return await context.Tags.ToListAsync();
        }
    }
}
