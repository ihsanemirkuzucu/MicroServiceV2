namespace MicroServiceV2.WEB.Options
{
    public class IdentityOption
    {
        public required string Address { get; set; }
        public required string BaseAddress { get; set; }
        public IdentityOptionsItem Admin { get; set; } = null!;
        public IdentityOptionsItem Web { get; set; } = null!;
    }

    public class IdentityOptionsItem
    {

        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}
