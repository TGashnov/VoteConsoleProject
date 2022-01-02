using System;
using System.Collections.Generic;
using System.Text;

namespace VoteModel
{
    public class Answer
    {
        public string AnswerText { get; set; }
        public int NumberOfVoters { get; set; } = 0;

        public Answer(string answer)
        {
            AnswerText = answer;
        }

        public Answer(string answer, int number)
        {
            AnswerText = answer;
            NumberOfVoters = number;
        }

        public override string ToString()
        {
            return AnswerText;
        }

        public override bool Equals(object obj)
        {
            return ToString().ToLower() == obj.ToString().ToLower();
        }
    }
}