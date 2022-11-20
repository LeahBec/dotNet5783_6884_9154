﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) :
                                base(message)
        {
        }

    }

    public class EntityAlreadyExistException : Exception
    {
        public EntityAlreadyExistException(string message) :
                                        base(message)
        {
        }

    }

    public class NoEntitiesFound : Exception
    {
    
        public NoEntitiesFound(string message) :
                                        base(message)
        {
        }

    }
}


