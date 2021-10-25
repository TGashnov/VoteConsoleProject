using System;
using System.Collections.Generic;
using System.Text;
using VoteModel;

namespace VoteProject.Managers
{
    class VoteManager
    {
        public bool AnswersContainInput(Vote vote, string answer) => vote.AnswersContain(new Answer(CapitalizedWord(answer)));

        public bool TagsContainInput(Vote vote, string tag) => vote.TagsContain(new Tag(tag));

        public string CapitalizedWord(string str)
        {
            str.ToLower();
            string l = str.Substring(0, 1).ToUpper();
            return l + str.Substring(1);
        }
    }
}