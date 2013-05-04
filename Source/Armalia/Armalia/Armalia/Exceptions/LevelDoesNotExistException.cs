using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Armalia.Exceptions
{
    class LevelDoesNotExistException : Exception
    {
        public LevelDoesNotExistException(string message)
            : base(message)
        {
        }

        public LevelDoesNotExistException()
            : base("No level with that name exists.")
        {
        }

    }
}
