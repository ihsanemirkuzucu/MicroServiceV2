namespace MicroServiceV2.WEB.Options;

public class MicroServiceOption
{
    public required MicroServiceOptionItem Catalog { get; set; }
    public required MicroServiceOptionItem File { get; set; }
    public required MicroServiceOptionItem Basket { get; set; }
    public required MicroServiceOptionItem Discount { get; set; }
    public required MicroServiceOptionItem Order { get; set; }
}

public class MicroServiceOptionItem
{
    public required string BaseAddress { get; set; }
}