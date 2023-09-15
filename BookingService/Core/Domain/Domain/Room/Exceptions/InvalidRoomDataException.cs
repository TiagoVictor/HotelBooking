using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Room.Exceptions
{
    public class InvalidRoomDataException : Exception
    {
        public override string Message => "Missing required information passed";
    }
}
