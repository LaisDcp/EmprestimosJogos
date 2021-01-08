using System;
using System.Net;

namespace EmprestimosJogos.Domain.Core.Attributes
{
    public class HttpStatusCodeAttribute : Attribute
    {
        public HttpStatusCode HttpStatusCode { get; }

        public HttpStatusCodeAttribute(int statusCode)
        {
            HttpStatusCode = (HttpStatusCode)statusCode;
        }
    }
}
