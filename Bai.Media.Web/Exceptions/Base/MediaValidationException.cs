using System;

namespace Bai.Media.Web.Exceptions.Base
{
    public class MediaValidationException : Exception
    {
        public MediaValidationException()
        {
        }

        public MediaValidationException(string message) : base(message)
        {
        }

        public MediaValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
