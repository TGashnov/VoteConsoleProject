using System;
using System.Collections.Generic;
using System.Text;
using VoteModel;

namespace VoteProject.Managers
{
    static class AnswerAccepter
    {
        public static void AcceptAnswer(List<Vote> list, int index)
        {
            Console.WriteLine();
            Console.Write("Выберите только один вариант ответа: ");
            int count = list[index].Answers.Count;
            int response;
            while (!int.TryParse(Console.ReadLine(), out response) || response <= 0 || response > count)
            {
                Console.WriteLine("Пожалуйста, введите число в диапазоне только от 1 до {0}", count);
            }
            list[index].AcceptAnswer(response - 1);
        }
    }
}