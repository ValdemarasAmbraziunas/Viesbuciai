using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPPro.Exceptions
{
    public class ITPProException : Exception
    {
        public ITPProException(string message) : base(message) { }
    }
}