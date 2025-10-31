namespace MicroServiceV2.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid UserId => Guid.Parse("269c0df1-e669-4cd1-92db-bc57137f52e2");
        public string Username => "Ahmet Yılmaz";
        public List<string> Roles => [];
    }
}
