using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteDbContext.Model.Repositories
{
    public class VoteRepository : VoteRepositoryBase
    {
        public VoteRepository(VoteContext context) : base(context) { }

        public async Task<IEnumerable<VoteDbDTO>> GetAllAsync(int? statusId, string orderBy, bool? orderAsc, 
            string searchBy, string search)
        {
            var votes = context.Votes.AsQueryable();

            switch (searchBy)
            {
                case "question": votes = votes.Where(v => EF.Functions.Like(v.Question, $"%{search}%")); 
                    break;
                case "answer": votes = votes.Where(v => 
                    v.Answers.Any(ans => EF.Functions.Like(ans.Text, $"%{search}%")));
                    break;
                case "tag": votes = votes.Where(v =>
                    v.Tags.Any(t => EF.Functions.Like(t.Text, $"%{search}%"))); 
                    break;
            }

            if (statusId != null)
            {
                votes = votes.Where(vote => vote.VoteStatusId == statusId);
            }

            switch (orderBy)
            {
                case "date": votes = OrderByDate(orderAsc, votes); break;
                case "rating": votes = OrderByRating(votes); break;
            }

            return await votes
                .Include(v => v.Answers)
                .Include(v => v.VoteStatus)
                .Include(v => v.Tags)
                .ToListAsync();
        }

        public async Task<IEnumerable<VoteDbDTO>> GetRecommendedAsync()
        {
            var votes = await context.Votes.Where(v => v.VoteStatusId == 2)
                .Include(v => v.Answers)
                .Include(v => v.VoteStatus)
                .Include(v => v.Tags)
                .ToListAsync();
            return votes.OrderBy(v => VoteRating(v)).Take(3);
        }

        private IQueryable<VoteDbDTO> OrderByRating(IQueryable<VoteDbDTO> votes)
        {
            return votes.OrderBy(v => VoteRating(v));
        }

        private IQueryable<VoteDbDTO> OrderByDate(bool? orderAsc, IQueryable<VoteDbDTO> votes)
        {
            if (orderAsc == null)
                return votes;
            if ((bool) orderAsc)
            {
                return votes.OrderBy(v => v.Published);
            }
            else
            {
                return votes.OrderByDescending(v => v.Published);
            }
        }

        public async Task<VoteDbDTO> GetAsync(long id)
        {
            return await context.Votes
                .Include(v => v.Answers)
                .Include(v => v.VoteStatus)
                .Include(v => v.Tags)
                .FirstOrDefaultAsync(v => v.VoteId == id);
        }

        public void Create(VoteDbDTO vote)
        {
            context.Votes.Add(vote);
        }

        public void Update(VoteDbDTO vote)
        {
            context.Votes.Update(vote);
        }

        public void Delete(VoteDbDTO vote)
        {
            context.Votes.Remove(vote);
        }

        public async Task<(VoteDbDTO, Exception)> DeleteAsync(long id)
        {
            var vote = await context.Votes.FindAsync(id);
            if (vote.VoteStatusId != 1) return (vote, new Exception());
            if (vote != null) Delete(vote);
            return (vote, null);
        }

        public async Task<bool> Exists(long id)
        {
            return await context.Votes.AnyAsync(v => v.VoteId == id);
        }

        public void AcceptAnswer(VoteDbDTO vote, int index)
        {
            vote.Answers.ElementAt(index).NumberOfVoters++;
            vote.NumberOfVoters++;
        }

        private double VoteRating(VoteDbDTO vote)
        {
            TimeSpan ts = DateTime.Now - vote.Created;
            double day = Math.Ceiling(ts.TotalDays);
            return (double)(vote.NumberOfVoters / day);
        }
    }
}
