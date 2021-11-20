using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VoteModel;
using VoteConsoleProject.Validation;

namespace VoteProject.Managers
{
    class VoteFinder
    {
        static List<string> tags = new List<string>();
        static string question = "";
        static List<string> answers = new List<string>();

        public static void SearchByQuestion()
        {
            question = InputControl.FindQuestion();
        }

        public static void SearchByAnswers()
        {
            answers = InputControl.SearchByAnswers();
        }

        public static void SearchByTags()
        {
            tags = InputControl.SearchByTags();
        }

        public static IEnumerable<Vote> Search(IEnumerable<Vote> list)
        {
            List<Vote> votes = (List<Vote>)list;
            List<Vote> foundVotes = new List<Vote>();
            if (answers.Count != 0 || tags.Count != 0)
            {
                for (int i = 0; i < votes.Count; i++)
                {
                    foreach (string answer in answers)
                    {
                        if (votes[i].Answers.Contains(new Answer(answer)))
                        {
                            foundVotes.Add(votes[i]);
                            break;
                        }
                    }
                    foreach (string tag in tags)
                    {
                        if (votes[i].Tags.Contains(new Tag(tag)))
                        {
                            foundVotes.Add(votes[i]);
                            break;
                        }
                    }
                }
            }
            else if (question != "")
            {
                foundVotes = votes.Where(vote => vote.Question.Text.Contains(question)).ToList();
            }
            question = "";
            answers.Clear();
            tags.Clear();
            return foundVotes;
        }

    }
}