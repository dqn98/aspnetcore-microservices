using Contracts.Domains;

namespace Contracts.Common.Events
{
    public class EventEntity<T> : EntityBase<T>, IEventEntity<T>
    {
        private readonly List<BaseEvent> _domainEvents = new List<BaseEvent>();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }

        public IReadOnlyCollection<BaseEvent> DomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
    }
}