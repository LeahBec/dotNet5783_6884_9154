using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class ExceptionObjectNotFound : Exception
    {
        public override string Message =>"object not found";
    }
    
    public class ExceptionNoMoreSpace : Exception
    {
        public override string Message => "there is no more space";
    }
    public class ExceptionObjectAlreadyExist : Exception 
    {
        public override string Message => "Object already exists";
        
    }
    public class ExceptionFailedToRead : Exception
    {
        public override string Message => "Faild to read properties";

    } public class ExceptionNoMatchingItems : Exception
    {
        public override string Message => "no matching items";

    }

}
