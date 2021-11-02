using System;
using System.Collections.Generic;
using System.Text;

namespace VoteConsoleProject.UserInterface
{
    class SelectFromList<T> where T : class
    {
        Func<List<T>> getNodes;

        List<T> Nodes => getNodes();

        int selectedNodeIndex = 0;
        public int SelectedNodeIndex
        {
            get => selectedNodeIndex;
            set
            {
                if (value < 0)
                {
                    selectedNodeIndex = 0;
                }
                else if (value >= Nodes.Count)
                {
                    selectedNodeIndex = Nodes.Count - 1;
                }
                else
                {
                    selectedNodeIndex = value;
                }
            }
        }
        public T SelectedNode
        {
            get => Nodes.Count > 0
            ? Nodes[SelectedNodeIndex]
            : null;
            set => SelectedNodeIndex = Nodes.IndexOf(value);
        }
        public Menu Menu { get; }

        public SelectFromList(Func<List<T>> getNodes)
        {
            Menu = new Menu(new MenuItem[] {
                new MenuAction(ConsoleKey.UpArrow, "Вверх", () => SelectedNodeIndex--, true),
                new MenuAction(ConsoleKey.DownArrow, "Вниз", () => SelectedNodeIndex++, true),
            });
            this.getNodes = getNodes;
        }
    }
}
