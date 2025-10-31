namespace MicroServiceV2.Basket.API.Dtos;

public record BasketDto
{

    public List<BasketItemDto> Items { get; set; } = new();
    public decimal? DiscountRate { get; set; }
    public string? Coupon { get; set; }

    public decimal TotalPrice => Items.Sum(item => item.Price);



    [JsonIgnore] public bool IsApplyDiscount => !string.IsNullOrEmpty(Coupon) && DiscountRate is > 0;

    public decimal? TotalPriceWithAppliedDiscount => !IsApplyDiscount
        ? null
        : Items.Sum(x => x.PriceByApplyDiscountRate);

    public BasketDto(List<BasketItemDto> items)
    {
        Items = items;
    }

    public BasketDto()
    {
    }

}

