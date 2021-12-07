using System;
using System.Globalization;

namespace WA.Pizza.Infrastructure.ErrorHandling
{
    public class InvalidException: Exception
    {
        public InvalidException() : base(){ }
        
        public InvalidException(string message) : base(message){ }
        
        public InvalidException(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args)){ }
    }
}
