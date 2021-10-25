using System;
using System.Collections.Generic;
using System.Text;
using VoteModel;

namespace VoteProject.Managers
{
    class VoteController
    {
        static VoteManager voteManager = new VoteManager();

        public static Vote FillInVote()
        {
            Vote vote = new Vote();
            FillInQuestion(vote);
            FillInAnswers(vote);
            FillInTags(vote);
            return vote;
        }

        private static void FillInQuestion(Vote vote)
        {
            Console.WriteLine();
            Console.WriteLine("Введите вопрос для голосования");
            string question = Console.ReadLine().ToLower();
            if (question == "")
            {
                Console.WriteLine("Вопрос не должен быть пустым. Попробуйте еще раз");
                FillInQuestion(vote);
            }
            else
            {
                vote.Question.Text = voteManager.CapitalizedWord(question);
            }
        }

        private static void FillInAnswers(Vote vote)
        {
            Console.WriteLine();
            Console.Write("Сколько вариантов ответа вы хотите?    ");
            uint n;
            while (!uint.TryParse(Console.ReadLine(), out n) || n <= 1)
            {
                Console.WriteLine("Должно быть как минимум 2 варианта ответов");
            }
            Console.WriteLine();
            Console.WriteLine("Введите ответы для голосования в формате");
            Console.WriteLine("1.Первый ответ\n2.Второй ответ\n3.Третий ответ\nи т.д.");
            for (int i = 0; i < n; i++)
            {
                string answer = Console.ReadLine();
                if (voteManager.AnswersContainInput(vote, answer))
                {
                    Console.WriteLine("Такой вариант ответа уже имеется");
                    i--;
                    continue;
                }
                vote.Answers.Add(new Answer(voteManager.CapitalizedWord(answer)));
            }
        }

        private static void FillInTags(Vote vote)
        {
            Console.WriteLine();
            Console.Write("Сколько тэгов вы хотите добавить?    ");
            uint n;
            while (!uint.TryParse(Console.ReadLine(), out n) || n == 0)
            {
                Console.WriteLine("Должен быть как минимум 1 тэг");
            }
            Console.WriteLine();
            Console.WriteLine("Введите тэги, чтобы затем по ним найти интересующее вас голосование. \n");
            string tag;
            for (int i = 0; i < n; i++)
            {
                tag = Console.ReadLine().ToLower();
                if (voteManager.TagsContainInput(vote, tag))
                {
                    Console.WriteLine("Такой тэг уже имеется");
                    i--;
                    continue;
                }
                if (tag == "")
                {
                    Console.WriteLine("Тэг не может быть пустым!");
                    i--;
                    continue;
                }
                vote.Tags.Add(new Tag(tag));
            }
        }
    }
}