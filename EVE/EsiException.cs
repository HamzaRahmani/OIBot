using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EveESI
{
    public class EsiException : Exception
    {
        public EsiException(HttpStatusCode code, string reason)
        {
            
        }
    }
}
