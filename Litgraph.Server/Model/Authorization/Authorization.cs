namespace Litgraph.Server.Model.Authorization
{
    public class SignInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SignUpRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}