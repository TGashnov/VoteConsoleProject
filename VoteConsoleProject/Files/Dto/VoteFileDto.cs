using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using VoteModel;

namespace VoteConsoleProject.Files.Dto
{
    [XmlType(TypeName = "Vote")]
    public class VoteFileDto
    {
        [XmlAttribute("Question")]
        public string Question { get; set; }
        public string Note { get; set; }

        [XmlAttribute("Created")]
        public DateTime Created { get; set; }

        [XmlAttribute("Published")]
        public DateTime Published { get; set; }
        public VoteStatus Status { get; set; }

        [XmlAttribute("number")]
        public int NumberOfVoters { get; set; }

        public AnswerFileDto[] Answers { get; set; }
        public TagFileDto[] Tags { get; set; }

        public static VoteFileDto Map(Vote vote)
        {
            return new VoteFileDto()
            {
                Question = vote.Question.Text,
                Note = vote.Question.Note,
                Created = vote.Created,
                Published = vote.Published,
                Status = vote.Status,
                NumberOfVoters = vote.NumberOfVotersForVote,
                Answers = vote.Answers.Select(answer => AnswerFileDto.Map(answer)).ToArray(),
                Tags = vote.Tags.Select(tag => TagFileDto.Map(tag)).ToArray()
            };
        }

        public static Vote Map(VoteFileDto vote)
        {
            return new Vote(
                vote.Question,
                vote.Answers.Select(answer => AnswerFileDto.Map(answer)).ToList(),
                vote.Tags.Select(tag => TagFileDto.Map(tag)).ToList(),
                vote.Status,
                vote.Created,
                vote.Published,
                vote.NumberOfVoters,
                vote.Note
                );            
        }

    }
}
