using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class ExceptionObjectNotFound : Exception
    {
        public ExceptionObjectNotFound()
        { Console.WriteLine("object not found");  }
    }
    public class ExceptionNoMoreSpace : Exception
    {
        public ExceptionNoMoreSpace()
        {
            Console.WriteLine("There is no more space");
        }
    }
    public class ExceptionObjectAlreadyExist : Exception 
    {
        public ExceptionObjectAlreadyExist()
        {
            Console.WriteLine("Object already exists");
        }
    }
    public class ExceptionFailedToRead : Exception
    {
        public ExceptionFailedToRead()
        {
            Console.WriteLine("Faild to read properties");
        }
    }

}
