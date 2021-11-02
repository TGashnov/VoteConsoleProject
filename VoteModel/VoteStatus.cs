using System;
using System.Collections.Generic;
using System.Text;

namespace VoteModel
{
    public enum VoteStatus
    {
        Preparation,
        Published,
        Closed
    }

    public static class VoteStatusRus
    {
        public static readonly Dictionary<VoteStatus, string> Names =
            new Dictionary<VoteStatus, string>()
            {
                { VoteStatus.Preparation, "Подготовка" },
                { VoteStatus.Published, "Опубликован" },
                { VoteStatus.Closed, "Закрыт" },
            };
    }
}