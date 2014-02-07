using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERClassFactory.Exceptions
{
    internal class ConnStrBuilderException : Exception
    {
        public new string Message;
        internal ConnStrBuilderException()
        {
            this.Message = "Incorrect Number Of Arguments.";
        }
    }
}
