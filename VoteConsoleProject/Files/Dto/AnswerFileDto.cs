using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using VoteModel;

namespace VoteConsoleProject.Files.Dto
{
    [XmlType(TypeName = "Answer")]
    public class AnswerFileDto
    {
        public string Answer { get; set; }

        public int NumberOfVoters { get; set; }

        public static AnswerFileDto Map(Answer answer)
        {
            return new AnswerFileDto()
            {
                Answer = answer.AnswerText,
                NumberOfVoters = answer.NumberOfVoters,
            };
        }

        public static Answer Map(AnswerFileDto answer)
        {
            return new Answer(answer.Answer, answer.NumberOfVoters);            
        }
    }
}
