using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoteConsoleProject.Validation;
using VoteConsoleProject.UserInterface;
using VoteModel;
using VoteConsoleProject.Files.Dto;

namespace VoteProject.Managers
{
    class VoteController
    {
        private List<Vote> Votes { get; } = new List<Vote>();

        public Menu Menu { get; }

        public Menu SortMenu { get; }
        public Menu SortByMenu { get; }
        public Menu SearchMenu { get; }
        public Menu ChangeMenu { get; }

        public SelectFromList<Vote> SelectVote { get; }
        public Vote SelectedVote => SelectVote.SelectedNode;

        public SelectFromList<Vote> SelectFoundVote { get; set; }
        public Vote FoundedVote => SelectFoundVote.SelectedNode;

        bool isOrderByDateDesc = false;
        bool isOrderByRating = false;

        public List<Vote> OrderedVotes
        {
            get
            {
                var votes = Votes;
                if (isOrderByRating)
                {
                    votes.OrderByDescending(vote => vote.VoteRating());
                }
                if (isOrderByDateDesc)
                {
                    votes.OrderByDescending(vote => vote.Published);
                }
                else
                {
                    votes.OrderBy(vote => vote.Published);
                }
                return votes.ToList();
            }
        }

        public Table<Vote> table = new Table<Vote>(new[]
        {
            new TableColumn<Vote>("Вопрос", 100, vote => vote.Question.Text),
            new TableColumn<Vote>("Рейтинг", 10, vote => vote.VoteRating().ToString()),
            new TableColumn<Vote>("Статус", 10, vote => VoteStatusRus.Names[vote.Status])
        });

        public VoteController(List<Vote> votes)
        {
            Votes.AddRange(votes);
            SelectVote = new SelectFromList<Vote>(() => OrderedVotes);
            Menu = new Menu(new List<MenuItem>(SelectVote.Menu.Items)
            {
                new MenuAction(ConsoleKey.F1, "Создать голосование", CreateVote),
                new MenuAction(ConsoleKey.F2, "Редактировать голосование", () => ChangeSelectedVote(SelectedVote)),
                new MenuAction(ConsoleKey.F3, "Удалить голосование", () => DeleteVote(SelectedVote)),
                new MenuAction(ConsoleKey.F4, "Закрыть голосование",() => Close(SelectedVote)),
                new MenuAction(ConsoleKey.F5, "Голосовать", () => DoVote(SelectedVote)),
                new MenuAction(ConsoleKey.F6, "Сортировка", ChooseSort),
                new MenuAction(ConsoleKey.F7, "Поиск", Search),
                new MenuAction(ConsoleKey.F8, "Опубликовать выбранное голосование",() => PostVote(SelectedVote)),
                new MenuAction(ConsoleKey.F9, "Сохранить в файл", SaveToFile),
                new MenuAction(ConsoleKey.F10, "Загрузить из файла", LoadFromFile),
                new MenuAction(ConsoleKey.Enter, "Просмотреть голосование", () => PrintVote(SelectedVote)),
                new MenuClose(ConsoleKey.Tab, "Вернуться на главную страницу")
            });
            SortMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "Сортировать по рейтингу", () => isOrderByRating = true),
                new MenuAction(ConsoleKey.D2, "Сортировать по дате публикации", SortBy)
            });
            SortByMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "Сортировать по дате публикации по возрастанию", () => isOrderByDateDesc = false),
                new MenuAction(ConsoleKey.D2, "Сортировать по дате публикации по убыванию", () => isOrderByDateDesc = true)
            });
            SearchMenu = new Menu(new List<MenuItem>()
            {
                new MenuAction(ConsoleKey.D1, "Поиск по вопросу", SearchByQuestion),
                new MenuAction(ConsoleKey.D2, "Поиск по ответам", SearchByAnswers),
                new MenuAction(ConsoleKey.D3, "Поиск по тэгам", SearchByTags),
            });
            ChangeMenu = new Menu(new List<MenuItem>(SelectVote.Menu.Items) {
                new MenuAction(ConsoleKey.D1, "Изменить вопрос", () => ChangeQuestion(SelectedVote)),
                new MenuAction(ConsoleKey.D2, "Изменить примечание", () => ChangeNote(SelectedVote)),
                new MenuAction(ConsoleKey.D3, "Изменить ответы", () => ChangeAnswers(SelectedVote)),
                new MenuAction(ConsoleKey.D4, "Изменить тэги", () => ChangeTags(SelectedVote)),
                new MenuClose(ConsoleKey.Tab, "Вернуться к голосованиям"),
            });
        }

        public void MainPage()
        {
            while (true)
            {
                Console.Clear();
                Menu.Print();
                table.Print(OrderedVotes, SelectedVote);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Tab) break;
                Menu.Action(key);
            }
        }

        void CreateVote()
        {
            Console.Clear();
            Vote vote = new Vote(
                InputControl.FillInQuestion(),
                InputControl.FillInAnswers(),
                InputControl.FillInTags(),
                InputControl.FillInNote()
                );
            Votes.Add(vote);
        }

        void ChangeVote()
        {
            Console.Clear();
            List<Vote> list = Votes.Where(vote => vote.Status == VoteStatus.Preparation).ToList();
            if (list.Count == 0)
            {
                Console.WriteLine("Пока нет доступных для редактирования голосований. Нажмите любую клавишу, чтобы продолжить.");
                Console.ReadKey();
                return;
            }
            while (true)
            {
                //SelectFromList<Vote> sfl = new SelectFromList<Vote>(() => list);
                //Vote vote = sfl.SelectedNode;
                SelectFoundVote = new SelectFromList<Vote>(() => list);
                ChangeMenu.Print();
                table.Print(list, FoundedVote);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Tab) break;
                ChangeMenu.Action(key);
                Console.Clear();
            }
        }

        void ChangeSelectedVote(Vote vote)
        {
            if (vote.Status != VoteStatus.Preparation) ChangeVote();
            else
            {
                Console.Clear();
                ChangeMenu.Print();
                Console.WriteLine(vote);
                ChangeMenu.Action(Console.ReadKey().Key);
            }
        }

        void ChangeQuestion(Vote vote)
        {
            Console.Clear();
            Console.WriteLine("Старый вопрос:");
            Console.WriteLine(vote.Question.Text);
            vote.Question.Text = InputControl.FillInQuestion();
        }

        void ChangeNote(Vote vote)
        {
            Console.Clear();
            Console.WriteLine("Старое примечание:");
            Console.WriteLine(vote.Question.Note);
            vote.Question.Note = InputControl.ChangeNote();
        }

        void ChangeAnswers(Vote vote)
        {
            Console.Clear();
            Console.WriteLine("Старые ответы:");
            foreach (var answer in vote.Answers) Console.WriteLine(answer);
            vote.ChangeAnswers(InputControl.FillInAnswers());
        }

        void ChangeTags(Vote vote)
        {
            Console.Clear();
            Console.WriteLine("Старые тэги:");
            foreach (var tag in vote.Tags) Console.WriteLine(tag);
            vote.ChangeTags(InputControl.FillInTags());
        }

        void ChooseSort()
        {
            Console.Clear();
            SortMenu.Print();
            table.Print(OrderedVotes, SelectedVote);
            SortMenu.Action(Console.ReadKey().Key);
        }

        void SortBy()
        {
            Console.Clear();
            SortByMenu.Print();
            SortByMenu.Action(Console.ReadKey().Key);
        }

        void Search()
        {
            Console.Clear();
            SearchMenu.Print();
            SearchMenu.Action(Console.ReadKey().Key);
        }

        void SearchByQuestion()
        {
            Console.Clear();
            VoteFinder.SearchByQuestion();
            PrintFoundVotes();
        }

        void SearchByAnswers()
        {
            Console.Clear();
            VoteFinder.SearchByAnswers();
            PrintFoundVotes();
        }

        void SearchByTags()
        {
            Console.Clear();
            VoteFinder.SearchByTags();
            PrintFoundVotes();
        }

        void PrintFoundVotes()
        {
            List<Vote> foundVotes = (List<Vote>)VoteFinder.Search(Votes);
            Console.Clear();
            NotFound(foundVotes);
            while (true)
            {
                //SelectFromList<Vote> sfl = new SelectFromList<Vote>(() => foundVotes);
                //Vote vote => sfl.SelectedNode;
                SelectFoundVote = new SelectFromList<Vote>(() => foundVotes);
                Menu.Print();
                table.Print(foundVotes, FoundedVote);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Tab) break;
                Menu.Action(key);
                Console.Clear();
            }
        }

        void PostVote(Vote vote)
        {
            Console.Clear();
            if (vote.Status == VoteStatus.Preparation)
            {
                Console.WriteLine("Вы уверены, что хотите опубликовать это голосование? Отменить это действие будет невозможно.");
                if (FileValidator.ReadYesNo())
                {
                    vote.FinishPreparation();
                }
            }
            else
            {
                Console.WriteLine("Невозможно опубликовать это голосование, так как оно не находится в статусе подготовки. Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
            }
        }
        void Close(Vote vote)
        {
            Console.Clear();
            if (vote.Status == VoteStatus.Published)
            {
                Console.WriteLine("Вы уверены, что хотите закрыть это голосование? Отменить это действие будет невозможно.");
                if (FileValidator.ReadYesNo())
                {
                    vote.Close();
                }                
            }
            else
            {
                Console.WriteLine("Невозможно закрыть это голосование, так как оно не находится в статусе опубликованно. Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
            }
        }
        void DeleteVote(Vote vote)
        {
            Console.Clear();
            if (vote.Status != VoteStatus.Published)
            {
                Console.WriteLine("Вы уверены, что хотите удалить это голосование? Отменить это действие будет невозможно.");
                if (FileValidator.ReadYesNo())
                {
                    Votes.Remove(vote);
                    SelectVote.SelectedNodeIndex--;
                }                
            }
            else
            {
                Console.WriteLine("Невозможно удалить это голосование, так как оно опубликованно. Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();

            }
        }

        public void DoVote(Vote vote)
        {
            if (vote.Status != VoteStatus.Published)
            {
                Console.Clear();
                Console.WriteLine("Невозвожно проголосовать в выбранном опросе, так как оно находится не в статусе опубликовано.\n" +
                    "Нажмите любую клавишу, чтобы продолжить.");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Console.WriteLine(vote);
            Console.WriteLine();
            Console.Write("Выберите только один вариант ответа: ");
            int count = vote.Answers.Count;
            int response;
            while (!int.TryParse(Console.ReadLine(), out response) || response <= 0 || response > count)
            {
                Console.WriteLine("Пожалуйста, введите число в диапазоне только от 1 до {0}", count);
            }
            vote.AcceptAnswer(response - 1);
        }

        void PrintVote(Vote vote)
        {
            Console.Clear();
            Console.WriteLine(vote);
            Console.ReadKey();
        }

        void SaveToFile()
        {
            try
            {
                SelectFile.SaveToFile(
                    Votes.Select(vote => VoteFileDto.Map(vote)),
                    "Голосования"
                    );
            }
            finally
            {
                Console.WriteLine("Файл успешно сохранен. Нажмите любую клавишу, чтобы продолжить.");
                Console.ReadKey();
            }
        }

        void LoadFromFile()
        {
            try
            {
                var loadedData = SelectFile.LoadFromFile<VoteFileDto>("Голосования");
                if (loadedData != null)
                {
                    LoadVotes(loadedData.Select(vote => VoteFileDto.Map(vote)));
                }
                Console.WriteLine("Файл успешно загружен!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка! Файл содержит некорректные данные: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Чтобы продолжить нажмите любую клавишу.");
                Console.ReadKey();
            }
        }

        public void LoadVotes(IEnumerable<Vote> votes)
        {
            Votes.Clear();
            Votes.AddRange(votes);
        }

        public IEnumerable<Vote> ReccomendedVotes()
        {
            List<Vote> recommendedVotes = Votes.Where(vote => vote.Status == VoteStatus.Published).OrderByDescending(vote => vote.VoteRating()).ToList();
            recommendedVotes.RemoveRange(3, recommendedVotes.Count - 3);
            return recommendedVotes;
        }

        void NotFound(List<Vote> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("По вашему запросу ничего не найдено :(\nНажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
                return;
            }
        }
    }
}