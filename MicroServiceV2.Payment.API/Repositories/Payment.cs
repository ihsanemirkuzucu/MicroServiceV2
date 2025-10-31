namespace MicroServiceV2.Payment.API.Repositories
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Amount { get; set; }
        public string? Error { get; set; }
        public PaymentStatus Status { get; set; }

        // Constructor to initialize a new payment
        public Payment(Guid userId, string orderCode, decimal amount)
        {
            Create(userId, orderCode, amount);
        }

        // Method to initialize a new payment
        public void Create(Guid userId, string orderCode, decimal amount)
        {
            Id = NewId.NextSequentialGuid();
            UserId = userId;
            OrderCode = orderCode;
            CreatedDate = DateTime.UtcNow;
            Amount = amount;
            Status = PaymentStatus.Pending;
        }

        // Method to update the payment status
        public void SetStatus(PaymentStatus status)
        {
            Status = status;
        }
    }
}
