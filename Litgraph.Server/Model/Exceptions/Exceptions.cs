using System;

namespace Litgraph.Server.Model.Exceptions
{
    public class ServerException : Exception 
    {
        public ServerException(string message) : base(message) { }
    }

    public class AuthorizationExceptions : ServerException 
    { 
        public AuthorizationExceptions() : base("Failed to authorize user") { }
    }

    public class SignUpException : ServerException 
    {
        public SignUpException(string reason) : base(reason) { }
    }

}