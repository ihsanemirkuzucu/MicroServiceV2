namespace MicroServiceV2.Discount.API.Features.Discounts
{
    public class Discount: BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
