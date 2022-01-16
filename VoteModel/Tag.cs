using System;
using System.Collections.Generic;
using System.Text;

namespace VoteModel
{
    public class Tag
    {
        public long TagId { get; set; }
        public string TagText { get; set; }

        public Tag(string tag)
        {
            TagText = tag;
        }

        public Tag(long id, string text)
        {
            TagId = id;
            TagText = text;
        }

        public override string ToString()
        {
            return TagText;
        }

        public override bool Equals(object obj)
        {
            return ToString().ToLower() == obj.ToString().ToLower();
        }
    }
}