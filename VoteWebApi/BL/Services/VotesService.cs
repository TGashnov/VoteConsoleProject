using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.Repositories;
using VoteWebApi.BL.Exceptions;
using VoteWebApi.BL.Model;

namespace VoteWebApi.BL.Services
{
    public class VotesService
    {
        protected VoteRepository voteRepository;
        protected VoteStatusRepository statusRepository; 

        public VotesService(VoteRepository voteRepository, VoteStatusRepository statusRepository)
        {
            this.voteRepository = voteRepository;
            this.statusRepository = statusRepository;
        }

        public async Task<IEnumerable<ViewVote>> GetRecommendedAsync()
        {
            var votes = await voteRepository.GetRecommendedAsync();
            return votes.Select(v => new VoteApiDto(v)).Select(v => new ViewVote(v));
        }

        public async Task<IEnumerable<ViewVote>> GetAllAsync(int? statusId, string orderBy, bool? orderAsc,
            string searchBy, string search)
        {
            var votes = await voteRepository.GetAllAsync(statusId, orderBy, orderAsc, searchBy, search);
            return votes.Select(v => new VoteApiDto(v)).Select(v => new ViewVote(v));
        }

        public async Task<IEnumerable<VoteStatusApiDto>> GetAllStatAsync()
        {
            var statuses = await statusRepository.GetAllAsync();
            return statuses.Select(s => new VoteStatusApiDto(s));
        }

        public async Task<ViewVote> GetAsync(long id)
        {
            var vote = await voteRepository.GetAsync(id);
            if (vote == null)
            {
                return null;
            }
            return new ViewVote(new VoteApiDto(vote));
        }

        public async Task<Exception> CreateAsync(VoteApiDto vote)
        {
            var voteToCreate = vote.Create();
            voteRepository.Create(voteToCreate);
            try
            {
                await voteRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return new SaveChangesException(e);
            }
            return null;
        }

        public async Task<(VoteApiDto, Exception)> ChangeStatusAsync(long id, int status)
        {
            var vote = await voteRepository.GetAsync(id);
            if(vote == null)
            {
                return (null, new KeyNotFoundException($"Голосования с Id = {id} не существует."));
            }

            if (vote.VoteStatusId == status)
            {
                return (null, new PubliсationException());
            }

            if (vote.VoteStatusId >= status)
            {
                return (null, new ClosingException());
            }

            if (status == 2)
                VoteApiDto.Publish(vote);
            else if (status == 3)
                VoteApiDto.Close(vote);
            
            try
            {
                await voteRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return (null, new SaveChangesException(e));
            }
            return (new VoteApiDto(vote), null);
        }

        public async Task<Exception> UpdateAsync(long id, VoteApiDto vote)
        {
            if(id != vote.VoteId)
            {
                return new ArgumentException("Id не совпадает", "id");
            }

            var voteToUpdate = await voteRepository.GetAsync(id);
            if (voteToUpdate == null)
            {
                return new KeyNotFoundException($"Голосования с Id = {id} не существует.");
            }

            if (voteToUpdate.VoteStatusId != 1)
            {
                return new UpdateException();
            }

            vote.Update(voteToUpdate);
            if(vote.Answers != null)
            {
                for (int i = 0; i < vote.Answers.Count(); i++)
                {
                    vote.Answers.ElementAt(i).Update(voteToUpdate.Answers.ElementAt(i));
                }
            }
            if (vote.Tags != null)
            {
                for (int i = 0; i < vote.Tags.Count(); i++)
                {
                    vote.Tags.ElementAt(i).Update(voteToUpdate.Tags.ElementAt(i));
                }
            }
            
            voteRepository.Update(voteToUpdate);

            try
            {
                await voteRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return new SaveChangesException(e);
            }
            return null;
        }

        public async Task<(VoteApiDto, Exception)> DeleteAsync(long id)
        {
            (var deletedVote, var ex) = await voteRepository.DeleteAsync(id);
            if(deletedVote == null)
            {
                return (null, new KeyNotFoundException($"Голосования с Id = {id} не существует."));
            }

            if (ex != null)
            {
                return (null, new DeleteException());
            }

            try
            {
                await voteRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return (null, new SaveChangesException(e));
            }

            return (new VoteApiDto(deletedVote), null);
        } 

        public async Task<Exception> DoVoteAsync(long id, int answer)
        {
            var vote = await voteRepository.GetAsync(id);
            if (vote == null)
            {
                return new KeyNotFoundException($"Голосования с Id = {id} не существует.");
            }

            if (answer <= 0 || answer > vote.Answers.Count())
            {
                return new ArgumentOutOfRangeException($"Введите число в диапазоне от 1 до {vote.Answers.Count()}");
            }

            voteRepository.AcceptAnswer(vote, answer - 1);

            try
            {
                await voteRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return new SaveChangesException(e);
            }
            return null;
        }
    }
}
