using MediatR;

namespace Ordering.Application;

public class DeleteOrderCommand : IRequest
{
    public long Id { get; private set; }
    public DeleteOrderCommand(long id)
    {
        Id = id;
    }
}
