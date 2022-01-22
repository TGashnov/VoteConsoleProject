using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.Repositories;
using VoteWebApi.BL.Services;

namespace VoteWebApi.BL
{
    public static class ServiceCollectionDataRepositoryExtension
    {
        public static void AddVoteServices(this IServiceCollection services)
        {
            services.AddTransient<AnswersService>();
            services.AddTransient<VotesService>();
        }

        public static void AddVoteRepositories(this IServiceCollection services)
        {
            services.AddTransient<VoteRepository>();
            services.AddTransient<AnswerRepository>();
            services.AddTransient<VoteStatusRepository>();
            services.AddTransient<TagRepository>();
        }
    }
}
