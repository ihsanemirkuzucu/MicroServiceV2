namespace MicroServiceV2.Order.Domain.Entities
{
    //Anemic Model => Rich Domain Model
    public class OrderItem:BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // Constructor to initialize an OrderItem with a product ID, name, and unit price.
        public void SetItem(Guid productId, string productName, decimal unitPrice)
        {
            if(string.IsNullOrEmpty(productName))
            {
                throw new ArgumentException("Product name cannot be empty");
            }
            if(unitPrice <= 0)
            {
                throw new ArgumentException("Unit price must be greater than zero");
            }

            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
        }

        // This method updates the unit price of the order item.
        public void UpdatePrice(decimal newPrice)
        {
            if(newPrice <= 0)
            {
                throw new ArgumentException("New price must be greater than zero");
            }
            UnitPrice = newPrice;
        }

        // This method applies a discount to the unit price of the order item.
        public void ApplyDiscount(decimal discountPercentage)
        {
            if(discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentException("Discount percentage must be between 0 and 100");
            }
            UnitPrice -= (UnitPrice * (decimal)discountPercentage / 100);
        }

        // This method checks if the current order item is the same as another order item based on the product ID.
        public bool IsSameItem(OrderItem orderItem)
        {
            return ProductId == orderItem.ProductId;
        }

    }
}
