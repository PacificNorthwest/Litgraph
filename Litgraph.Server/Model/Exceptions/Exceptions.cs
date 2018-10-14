using System;

namespace Litgraph.Server.Model.Exceptions
{
    public class AuthorizationExceptions : Exception 
    { 
        public AuthorizationExceptions() : base("Failed to authorize user") { }
    }

    public class SignUpException : Exception 
    {
        public SignUpException(string reason) : base(reason) { }
    }

}