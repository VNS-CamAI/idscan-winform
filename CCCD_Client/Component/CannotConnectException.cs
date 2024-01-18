using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCD_Client.Component
{
    public class CannotConnectException : Exception
    {
        public CannotConnectException(string message) : base(message)
        {
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
