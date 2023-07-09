using Authentication.Domain.Common;

namespace Authentication.Domain.Entities
{
    public class Client:EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
