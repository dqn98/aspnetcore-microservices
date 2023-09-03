namespace EventBus.Messages.IntegrationEvents;

public interface IIntegrationEvent
{
    DateTime CreationDate { get; }

    Guid Id { get; set; }
}