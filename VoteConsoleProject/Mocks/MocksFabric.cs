using System;
using System.Collections.Generic;
using System.Text;
using VoteModel;

namespace VoteConsoleProject.Mocks
{
    static class MocksFabric
    {
        public static List<Vote> MockVotes => new List<Vote>
        {
            new Vote("Последовательность чисел 13; 21; 29; 37; ... - это:", 
                new List<Answer>() {
                    new Answer("Ряд Тейлора"),
                    new Answer("Арифметическая прогрессия"),
                    new Answer("Ряд Фурье"),
                    new Answer("Геометрическая прогрессия"),
                },
                new List<Tag>()
                {
                    new Tag("Математика"),
                    new Tag("Х")
                },
                VoteStatus.Published),
            new Vote("Кто был первым лауреатом Нобелевской премии по физике?",
                new List<Answer>() {
                    new Answer("Лоренц"),
                    new Answer("Рентген"),
                    new Answer("Рэлей"),
                },
                new List<Tag>()
                {
                    new Tag("Физика"),
                    new Tag("Премия"),
                    new Tag("Х"),
                },
                VoteStatus.Published),
            new Vote("Поездке в какой город был рад Бармалей в сказке Корнея Чуковского?",
                new List<Answer>() {
                    new Answer("В Ленинград"),
                    new Answer("В Париж"),
                    new Answer("В Москву"),
                    new Answer("В Сталинград")
                },
                new List<Tag>()
                {
                    new Tag("Сказка"),
                    new Tag("Город"),
                    new Tag("Литература"),
                },
                VoteStatus.Published,
                "Сейчас такого города уже нет"),
            new Vote("В каком из этих городов состоялись игры Олимпиады-2000?",
                new List<Answer>() {
                    new Answer("Канберра"),
                    new Answer("Сидней"),
                    new Answer("Дарвин")
                },
                new List<Tag>()
                {
                    new Tag("Олимпиада"),
                    new Tag("Город"),
                },
                VoteStatus.Published,
                "Город из правильного ответа находится в Австралии"),
        };

    }
}
