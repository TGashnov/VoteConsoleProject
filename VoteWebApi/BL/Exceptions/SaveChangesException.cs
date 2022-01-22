using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoteWebApi.BL.Exceptions
{
    public class SaveChangesException: Exception
    {
        public SaveChangesException() : base("Произошла ошибка при сохранении данных") { }

        public SaveChangesException(Exception innerException) :
            base("Произошла ошибка при сохранении данных", innerException) { }
    }
}
