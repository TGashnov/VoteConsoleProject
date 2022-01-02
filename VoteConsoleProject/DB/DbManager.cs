using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Model;
using Microsoft.EntityFrameworkCore;
using VoteModel;
using System.Linq;
using VoteConsoleProject.DB.Mapping;

namespace VoteConsoleProject.DB
{
    static class DbManager
    {
        static string connectionString = ConnectionManager.GetConnectionString();

        static VoteContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VoteContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return new VoteContext(options);
        }

        public static List<Vote> GetVotes()
        {
            using(var context = CreateContext())
            {
                return context.Votes
                    .Include(vote => vote.Answers)
                    .Include(vote => vote.Tags)
                    .Include(vote => vote.VoteStatus)
                    .Select(vote => VoteMapper.Map(vote))
                    .ToList();
            }
        } 

        public static void UpdateVotes(IEnumerable<Vote> votes)
        {
            using (var context = CreateContext())
            {
                using var trans = context.Database.BeginTransaction();
                try
                {
                    var votesInDb = context.Votes.AsNoTracking();
                    foreach (var voteInDb in votesInDb)
                    {
                        var vote = votes.FirstOrDefault(vModel => vModel.Id == voteInDb.VoteId);
                        if (vote != null)
                        {
                            context.Update(VoteMapper.Map(vote));
                        }
                        else
                            context.Remove(voteInDb);
                    }
                    context.SaveChanges();

                    var votesToAdd = votes
                        .Where(v => context.Votes.Find(v.Id) == null)
                        .Select(v => VoteMapper.Map(v))
                        .ToList();
                    context.AddRange(votesToAdd);
                    context.SaveChanges();

                    trans.Commit();
                } 
                catch(Exception e)
                {
                    Console.Error.WriteLine("Произошла ошибка при сохранении голосований в базу: " + e.Message);
                    trans.Rollback();
                }
            }
        }
    }
}
