﻿namespace MicroServiceV2.WEB.Pages.Order.Dto;

public record PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvc, decimal Amount);