namespace MicroServiceV2.Basket.API.Features.Baskets
{
    public class BasketMapping:Profile
    {
        public BasketMapping()
        {

            CreateMap<BasketDto, Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}
