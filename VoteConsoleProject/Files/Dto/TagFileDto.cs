using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using VoteModel;

namespace VoteConsoleProject.Files.Dto
{
    [XmlType(TypeName = "Tag")]
    public class TagFileDto
    {
        public string Tag { get; set; }

        public static TagFileDto Map(Tag tag)
        {
            return new TagFileDto()
            {
                Tag = tag.TagText,
            };
        }

        public static Tag Map(TagFileDto tag)
        {
            return new Tag(tag.Tag);
        }
    }
}
