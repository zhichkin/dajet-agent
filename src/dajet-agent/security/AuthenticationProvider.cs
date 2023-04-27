using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaJet.Agent.Service.Security
{
    public sealed class AuthenticationProvider
    {
        private readonly List<AppUser> _users = new List<AppUser>()
        {
            new AppUser { Id = 1, Username = "admin", Password = "admin" }
        };

        public async Task<AppUser> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null)
            {
                return null;
            }

            return new AppUser() { Id = user.Id, Username = user.Username, Password = string.Empty };
        }
    }
}