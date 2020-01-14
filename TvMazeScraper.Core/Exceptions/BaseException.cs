using System;
using System.Net;

namespace TvMazeScraper.Core.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public BaseException()
            : this(HttpStatusCode.InternalServerError, null)
        {
        }
        public BaseException(HttpStatusCode statusCode)
            : this(statusCode, null)
        {
        }
        public BaseException(string message)
            : this(HttpStatusCode.InternalServerError, message)
        {
        }
        public BaseException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}