namespace MicroServiceV2.Bus.Events;

public record OrderCreatedEvent(Guid OrderId, Guid UserId);
