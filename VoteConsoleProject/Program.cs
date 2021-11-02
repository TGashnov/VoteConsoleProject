using System;
using System.Collections.Generic;
using VoteConsoleProject.Mocks;
using VoteConsoleProject.UserInterface;
using VoteModel;
using VoteProject.Managers;

namespace VoteConsoleProject
{
    class Program
    {
        static VoteController voteController;
        static SelectFromList<Vote> SelectVote { get; } = new SelectFromList<Vote>(() => (List<Vote>)voteController.ReccomendedVotes());
        static Vote SelectedVote => SelectVote.SelectedNode;

        static Program()
        {
            voteController = new VoteController(MocksFabric.MockVotes);
        }

        static void Main(string[] args)
        {
            MainMenuInput();
        }

        static readonly Menu mainMenu = new Menu(new List<MenuItem>(SelectVote.Menu.Items) {
            new MenuAction(ConsoleKey.F1, "Проголосовать в выбранном голосовании",
                () => DoVote(SelectedVote)),
            new MenuAction(ConsoleKey.Tab, "Перейти к голосованиям",
                () => MenuInput()),
            new MenuClose(ConsoleKey.Escape, "Выход"),
        });

        static void MainMenuInput()
        {
            while (true)
            {
                Console.Clear();
                mainMenu.Print();
                voteController.table.Print(voteController.ReccomendedVotes(), SelectedVote);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape) return;
                mainMenu.Action(key);
            }
        }

        private static void MenuInput()
        {
            voteController.MainPage();
        }

        private static void DoVote(Vote vote)
        {
            voteController.DoVote(vote);
        }
    }
}
