using System;
using System.Collections.Generic;
using System.Text;
using VoteModel;

namespace VoteConsoleProject.Validation
{
    public static class InputControl
    {
        public static string FillInQuestion()
        {
            Console.WriteLine("Введите вопрос для голосования");
            string question = Console.ReadLine().ToLower();
            if (question == "")
            {
                Console.WriteLine("Вопрос не должен быть пустым. Попробуйте еще раз");
                FillInQuestion();
            }
            return CapitalizedWord(question);
        }

        public static string FillInNote()
        {
            Console.WriteLine();
            Console.WriteLine("Хотите ли вы к вопросу добавить примечание? Введите его, если хотите или же оставьте строку пустой");
            string note = Console.ReadLine().ToLower();
            if (note != "")
            {
                return CapitalizedWord(note);
            }
            return null;
        }

        public static string ChangeNote()
        {
            Console.WriteLine();
            Console.WriteLine("Введите примечание.");
            string note = Console.ReadLine().ToLower();
            if (note == "")
            {
                Console.WriteLine("Вы ничего не ввели. Попробуйте еще раз.");
                ChangeNote();
            }
            return CapitalizedWord(note);
        }

        public static List<Answer> FillInAnswers()
        {
            Console.WriteLine();
            List<Answer> answers = new List<Answer>();
            Console.Write("Сколько вариантов ответа вы хотите?    ");
            uint n;
            while (!uint.TryParse(Console.ReadLine(), out n) || n <= 1)
            {
                Console.WriteLine("Должно быть как минимум 2 варианта ответов");
                Console.WriteLine();
            }
            Console.WriteLine("Введите ответы для голосования в формате");
            Console.WriteLine("Первый ответ\nВторой ответ\nТретий ответ\nи т.д.");
            for (int i = 0; i < n; i++)
            {
                string answer = Console.ReadLine().ToLower();
                if (answers.Contains(new Answer(answer)))
                {
                    Console.WriteLine("Такой вариант ответа уже имеется");
                    i--;
                    continue;
                }
                if (answer == "")
                {
                    Console.WriteLine("Вариант ответа не может быть пустым");
                    i--;
                    continue;
                }
                answers.Add(new Answer(CapitalizedWord(answer)));
            }
            return answers;
        }

        public static List<Tag> FillInTags()
        {
            Console.WriteLine();
            List<Tag> tags = new List<Tag>();
            Console.Write("Сколько тэгов вы хотите добавить?    ");
            uint n;
            while (!uint.TryParse(Console.ReadLine(), out n) || n == 0)
            {
                Console.WriteLine("Должен быть как минимум 1 тэг");
                Console.WriteLine();
            }
            Console.WriteLine("Введите тэги, чтобы затем по ним найти голосование. \n");
            string tag;
            for (int i = 0; i < n; i++)
            {
                tag = Console.ReadLine().ToLower();
                if (tags.Contains(new Tag(tag)))
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
                tags.Add(new Tag(tag));
            }
            return tags;
        }

        public static List<string> SearchByTags()
        {
            List<string> tags = new List<string>();
            Console.Clear();
            Console.WriteLine("Введите тэги, чтобы найти интересующее вас голосование(как минимум 1). \nЧтобы закончить нажмите Enter без ввода тэга");
            string tag;
            do
            {
                tag = Console.ReadLine().ToLower().Trim();
                if (tags.Count == 0 && tag == "")
                {
                    Console.WriteLine("Должен быть введен хотя бы 1 тэг");
                    tag = " ";
                    continue;
                }
                tags.Add(tag);
            } while (tag != "");
            return tags;
        }

        public static string FindQuestion()
        {
            Console.Clear();
            Console.WriteLine("Введите вопрос или его часть, чтобы найти интересующее вас голосование.");
            string question = Console.ReadLine().ToLower().Trim();
            if (question == "")
            {
                Console.WriteLine("Вы не ввели вопрос!");
                FindQuestion();
            }
            return question;
        }

        public static List<string> SearchByAnswers()
        {
            List<string> answers = new List<string>();
            Console.Clear();
            Console.WriteLine("Введите ответы, чтобы найти интересующее вас голосование(как минимум 1). \nЧтобы закончить нажмите Enter без ввода ответа");
            string answer;
            do
            {
                answer = Console.ReadLine().ToLower().Trim();
                if (answers.Count == 0 && answer == "")
                {
                    Console.WriteLine("Должен быть введен хотя бы 1 ответ");
                    answer = " ";
                    continue;
                }
                answers.Add(answer);
            } while (answer != "");
            return answers;
        }

        private static string CapitalizedWord(string str)
        {
            str.ToLower();
            string l = str.Substring(0, 1).ToUpper();
            return l + str.Substring(1);
        }
    }
}
