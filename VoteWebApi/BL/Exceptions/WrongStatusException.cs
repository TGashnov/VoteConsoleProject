using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoteWebApi.BL.Exceptions
{
    public class PubliсationException: Exception
    {
        public PubliсationException() : 
            base("Невозможно опубликовать данное голосование, так как оно уже находится в статусе опубликовано")
        { }
    }

    public class ClosingException: Exception
    {
        public ClosingException() :
            base("Невозможно закрыть данное голосование, так как оно уже находится в статусе закрыто")
        { }
    }

    public class UpdateException: Exception
    {
        public UpdateException() :
            base("Голосование можно изменить только, если оно находится в статусе подготовка")
        { }
    }

    public class DeleteException: Exception
    {
        public DeleteException() :
            base("Голосование можно удалить только, если оно находится в статусе подготовка")
        { }
    }
}
