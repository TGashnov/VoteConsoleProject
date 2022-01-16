using System;
using System.Collections.Generic;
using System.Text;
using VoteDbContext.Model.DTO;
using VoteModel;

namespace VoteConsoleProject.DB.Mapping
{
    static class TagMapper
    {
        public static Tag Map(TagDbDTO tag)
        {
            if (tag == null)
                return null;

            return new Tag(
                tag.TagId,
                tag.Text);
        }

        public static TagDbDTO Map(Tag tag)
        {
            if (tag == null)
                return null;

            return new TagDbDTO()
            {
                TagId = tag.TagId,
                Text = tag.TagText
            };
        }
    }
}
