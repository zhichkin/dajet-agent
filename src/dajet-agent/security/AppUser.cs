namespace DaJet.Agent.Service.Security
{
    public sealed class AppUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Pswd { get; set; } = string.Empty;
    }
}