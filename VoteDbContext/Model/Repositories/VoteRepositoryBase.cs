using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VoteDbContext.Model.Repositories
{
    public class VoteRepositoryBase
    {
        protected VoteContext context;

        public VoteRepositoryBase(VoteContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
