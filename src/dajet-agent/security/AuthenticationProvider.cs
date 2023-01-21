using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaJet.Agent.Service.Security
{
    public sealed class AuthenticationProvider
    {
        private readonly List<AppUser> _users = new List<AppUser>()
        {
            new AppUser { Id = 1, Name = "admin", Pswd = "admin" }
        };
        private SqliteAppSettingsProvider App { get; set; }
        public AuthenticationProvider(SqliteAppSettingsProvider _app)
        {
            App = _app;
        }
        public AppUser Authenticate(string username, string password)
        {
            AppUser user = new()
            {
                Name = username,
                Pswd = password
            };

            if (!App.TrySelectUser(in user))
            {
                return null;
            }

            user.Pswd = string.Empty;

            return user;
        }
        public async Task<AppUser> AuthenticateAsync(string username, string password)
        {
            AppUser user = Authenticate(username, password);

            return await Task.FromResult(user);
        }
    }
}