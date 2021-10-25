using System;
using System.Collections.Generic;
using System.Text;
using VoteProject.Managers;
using VoteModel;

namespace VoteConsoleProject
{
    class VoteConsole
    {
        public VoteConsole()
        {
            Manager();
        }

        private void Manager()
        {
            Console.WriteLine("Приветствую! Чтобы вы хотели сделать?");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Нажмите F1, чтобы создать голосование");
                Console.WriteLine("Нажмите F2, чтобы участвовать в голосовании");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1: CreateVote(); break;
                    case ConsoleKey.F2: DoVote(); break;
                    default: Console.WriteLine("Команда не распознана!"); break;
                }
                Console.WriteLine();
                Console.WriteLine("Если вы закончили, то нажмите Escape, иначе - любую другую клавишу");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private void CreateVote()
        {
            votes.Add(VoteController.FillInVote());
        }

        private void DoVote()
        {
            if (votes.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Пока нет ни одного активного голосования");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Нажмите F1, чтобы вывести все голосования");
                Console.WriteLine("Нажмите F2, чтобы найти голосование по тэгу");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1: PrintAllVotes(); break;
                    case ConsoleKey.F2: SearchController.ChooseTags(votes); break;
                    default: Console.WriteLine("Команда не распознана!"); break;
                }
            }
        }

        private void PrintAllVotes()
        {
            Console.WriteLine();
            for (int i = 0; i < votes.Count; i++)
            {
                Console.Write("{0}. ", i + 1);
                Console.WriteLine(votes[i]);
                Console.WriteLine();
            }
            SearchController.SelectVote(votes, votes.Count);
        }

        List<Vote> votes = new List<Vote>();
    }
}