namespace MicroServiceV2.Order.Application.Dtos;

public record PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvc, decimal Amount);