﻿using System;

namespace VoteModel
{
    public class Question
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}