namespace MicroServiceV2.Shared.Services
{
    public interface IIdentityService
    {
        public Guid UserId { get; }
        public string Username { get; }
        public List<string> Roles { get; }
    }
}
