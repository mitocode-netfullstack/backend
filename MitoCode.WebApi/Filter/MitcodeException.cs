using System;

namespace MitoCode.WebApi.Filter
{
    public class MitcodeException : Exception
    {
        public MitcodeException(string message)
            : base(message)
        {
        }
    }
}