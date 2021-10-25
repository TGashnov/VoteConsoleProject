using System;
using System.Collections.Generic;
using System.Text;
using VoteModel;

namespace VoteProject.Managers
{
    static class SearchController
    {
        static VoteFinder voteFinder = new VoteFinder();
        public static void ChooseTags(List<Vote> list)
        {
            Console.WriteLine();
            Console.WriteLine("Введите тэги, чтобы найти интересующее вас голосование(как минимум 1). \nЧтобы закончить нажмите Enter без ввода тэга");
            string tag;
            do
            {
                tag = Console.ReadLine().ToLower();
                if (voteFinder.TagsCount == 0 && tag == "")
                {
                    Console.WriteLine("Должен быть введен хотя бы 1 тэг");
                    tag = " ";
                    continue;
                }
                voteFinder.AddTag(tag);
            } while (tag != "");
            voteFinder.RemoveEmptiness();
            PrintFoundVote(list);
        }

        public static void PrintFoundVote(List<Vote> list)
        {
            int n = 0;
            List<Vote> foundVotes = voteFinder.SearchByTags(list);
            foreach (Vote vote in foundVotes)
            {
                n++;
                Console.WriteLine(n + ". " + vote);
            }
            voteFinder.ClearTags();
            SelectVote(foundVotes, n);
        }

        public static void SelectVote(List<Vote> list, int number)
        {
            if (number >= 1)
            {
                if (number > 1)
                {
                    Console.WriteLine();
                    Console.Write("Выберите в каком именно из найденных голосований вы хотите поучаствовать: ");
                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice) || choice > number || choice <= 0)
                    {
                        Console.WriteLine("Пожалуйста, введите число в диапазоне только от 1 до {0}", number);
                    }
                    number = choice;
                    Console.WriteLine();
                    voteFinder.PrintVote(list, number - 1);
                }
                AnswerAccepter.AcceptAnswer(list, number - 1);
            }
            else if (number == 0)
            {
                Console.WriteLine("К сожалению, по введенным тэгам ничего не найдено. Хотите попробовать сначала?");
                Console.WriteLine("Нажмите Enter, чтобы попробовать сначала или любую другую клавишу, чтобы закончить");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    ChooseTags(list);
                }
                else
                {
                    return;
                }
            }
        }
    }
}