using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Connection;
using VoteDbContext.Model;
using Microsoft.EntityFrameworkCore;
using VoteDbContext.Model.DTO;
using System.Linq;

namespace VoteDbContext
{
    class Program
    {
        static readonly string connectionString = new ConnectionStringManager().ConnectionString;

        static void Main(string[] args)
        {
            AddMockData();
            PrintVote();
        }

        static VoteContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VoteContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return new VoteContext(options);
        }

        static void AddMockData()
        {
            using (var context = CreateContext())
            {
                context.Add(AddMockVote());
                context.SaveChanges();
            }
        }

        static void PrintVote()
        {
            using(var context = CreateContext())
            {
                var votes = context.Votes
                    .Select(v => new
                    {
                        v.Question,
                        v.Answers,
                        v.Tags,
                        v.Created,
                        v.VoteStatus.Name
                    })
                    .ToArray();
                foreach(var vote in votes)
                {
                    Console.WriteLine("Вопрос: " + vote.Question);
                    Console.WriteLine("Дата создания:" + vote.Created);
                    Console.WriteLine("Статус:" + vote.Name);
                    Console.WriteLine("Ответы:");
                    int i = 0;
                    foreach(var ans in vote.Answers)
                    {
                        i++;
                        Console.WriteLine(i + "." + ans.Text + "\t" + ans.NumberOfVoters);
                    }
                    Console.WriteLine("Тэги:");
                    Console.WriteLine(string.Format(", ", vote.Tags));
                    Console.WriteLine(new string('-', Console.WindowWidth));
                }
            }
        }

        static VoteDbDTO AddMockVote()
        {
            return new VoteDbDTO()
            {
                Question = "Последовательность чисел 13; 21; 29; 37; ... - это:",
                Answers = AddMockAnswers(),
                Tags = AddMockTag(),
                VoteStatus = AddMockStatus(),
                NumberOfVoters = 6,
                Created = DateTime.Now
            };
        }

        static List<AnswerDbDTO> AddMockAnswers()
        {
            return new List<AnswerDbDTO>()
            {
                new AnswerDbDTO()
                {
                    Text = "Ряд Тейлора", NumberOfVoters = 3
                },
                new AnswerDbDTO()
                {
                    Text = "Арифметическая прогрессия", NumberOfVoters = 2
                },
                new AnswerDbDTO()
                {
                    Text = "Ряд Фурье", NumberOfVoters = 1
                },
                new AnswerDbDTO()
                {
                    Text = "Геометрическая прогрессия"
                }
            };
        }

        static List<TagDbDTO> AddMockTag()
        {
            return new List<TagDbDTO>()
            {
                new TagDbDTO()
                {
                    Text = "Математика"
                },
                new TagDbDTO()
                {
                    Text = "Х"
                }
            };
        }

        static VoteStatusDbDTO AddMockStatus()
        {
            return new VoteStatusDbDTO
            {
                Name = "Редактируемый"
            };
        }
    }
}
