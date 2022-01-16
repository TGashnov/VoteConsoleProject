using System;
using System.Collections.Generic;
using VoteConsoleProject.DB;
using VoteConsoleProject.Mocks;
using VoteConsoleProject.UserInterface;
using VoteModel;
using VoteProject.Managers;

namespace VoteConsoleProject
{
    class Program
    {
        static Program()
        {
            if (useMocks)
            {
                voteController = new VoteController(MocksFabric.MockVotes);
            }
            else
            {
                voteController = new VoteController(DbManager.GetVotes());
            }
        }

        static VoteController voteController;
        static SelectFromList<Vote> SelectVote { get; } = new SelectFromList<Vote>(() => (List<Vote>)voteController.ReccomendedVotes());
        static Vote SelectedVote => SelectVote.SelectedNode;
        const bool useMocks = false;

        static void Main(string[] args)
        {
            MainMenuInput();
            if(!useMocks)
            {
                DbManager.UpdateVotes(voteController.GetVotes());
            }
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
                if (key == ConsoleKey.Escape) break;
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
