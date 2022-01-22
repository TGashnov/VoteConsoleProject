using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.Repositories;
using VoteWebApi.BL.Model;

namespace VoteWebApi.BL.Services
{
    public class AnswersService
    {
        protected AnswerRepository repository;

        public AnswersService(AnswerRepository repository)
        {
            this.repository = repository;
        }

    }
}
