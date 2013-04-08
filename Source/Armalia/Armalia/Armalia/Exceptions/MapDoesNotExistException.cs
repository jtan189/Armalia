using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Armalia.Exceptions
{
    class MapDoesNotExistException : Exception
    {
        public MapDoesNotExistException(string msg)
            : base(msg)
        {
        }

        public MapDoesNotExistException()
            : base("There exist no map with that name")
        {
        }

    }
}
