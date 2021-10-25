using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VoteModel;

namespace VoteProject.Managers
{
    class VoteFinder
    {
        List<string> tags = new List<string>();

        public void AddTag(string tag)
        {
            tags.Add(tag);
        }
        public int TagsCount => tags.Count;
        public void RemoveEmptiness() => tags.Remove("");

        public List<Vote> SearchByTags(List<Vote> votes)
        {
            List<Vote> foundVotes = new List<Vote>();
            for (int i = 0; i < votes.Count; i++)
            {
                foreach (string tag in tags)
                {
                    if (votes[i].TagsContain(new Tag(tag)))
                    {
                        foundVotes.Add(votes[i]);
                        break;
                    }
                }
            }
            return foundVotes;
        }

        public void ClearTags() => tags.Clear();

        public void PrintVote(List<Vote> votes, int index)
        {
            Console.WriteLine(votes[index]);
        }
    }
}