using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandlers
{
    public class SomerenException : Exception
    {
        public SomerenException(string exMessage) : base(exMessage) { }
        public SomerenException(string exMessage, Exception baseException) : base(exMessage, baseException) { }
    }
}
