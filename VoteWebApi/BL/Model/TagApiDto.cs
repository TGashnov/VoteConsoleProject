using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteDbContext.Model.DTO;

namespace VoteWebApi.BL.Model
{
    public class TagApiDto
    {
        public long TagId { get; set; }
        public string Text { get; set; }

        public TagApiDto() { }

        public TagApiDto(TagDbDTO tag)
        {
            TagId = tag.TagId;
            Text = tag.Text;
        }

        public TagDbDTO Create()
        {
            return new TagDbDTO()
            {
                TagId = TagId,
                Text = Text
            };
        }

        public void Update(TagDbDTO tag)
        {
            tag.Text = Text;
        }
    }

    public class ViewTag
    {
        public string Text { get; set; }

        public ViewTag(TagApiDto tag)
        {
            Text = tag.Text;
        }
    }
}
