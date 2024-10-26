using Contracts.Common.Events;

namespace Ordering.Domain.OrderAggregate.Events;

public class OrderDeletedEvent(long id) : BaseEvent
{
    public long Id { get; set; } = id;
}
