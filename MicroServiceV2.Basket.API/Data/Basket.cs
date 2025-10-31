namespace MicroServiceV2.Basket.API.Data
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        public decimal? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        [JsonIgnore] public bool IsApplyDiscount => !string.IsNullOrEmpty(Coupon) && DiscountRate is > 0;

        public Basket()
        {
        }

        public Basket(Guid userId, List<BasketItem> items)
        {
            UserId = userId;
            Items = items;
        }

        [JsonIgnore] public decimal TotalPrice => Items.Sum(item => item.Price);

        [JsonIgnore]
        public decimal? TotalPriceWithAppliedDiscount => !IsApplyDiscount
            ? null
            : Items.Sum(x => x.PriceByApplyDiscountRate);

        public void ApplyNewDiscount(string coupon, decimal discountRate)
        {
            Coupon = coupon;
            DiscountRate = discountRate;


            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - discountRate);
            }
        }

        public void ApplyAvailableDiscount()
        {
            if (!IsApplyDiscount)
                return;

            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate!);
            }
        }

        public void RemoveDiscount()
        {
            Coupon = null;
            DiscountRate = null;
            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = null;
            }
        }

    }
}
