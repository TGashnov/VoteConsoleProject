using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model.Repositories
{
    public class VoteStatusRepository : VoteRepositoryBase
    {
        public VoteStatusRepository(VoteContext context) : base(context) { }

        public async Task<IEnumerable<VoteStatusDbDTO>> GetAllAsync()
        {
            return await context.VoteStatuses.ToListAsync();
        }

        public async Task<VoteStatusDbDTO> GetAsync(int id)
        {
            return await context.VoteStatuses.FindAsync(id);
        }
    }
}
