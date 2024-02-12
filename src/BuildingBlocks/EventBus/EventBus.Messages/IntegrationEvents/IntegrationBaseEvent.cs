namespace EventBus.Messages.IntegrationEvents
{
    public record IntegrationBaseEvent() : IIntegrationEvent
    {
        public DateTime CreationDate { get; } = DateTime.UtcNow;
        public Guid Id { get; set; }
    }
}