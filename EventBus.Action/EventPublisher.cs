using MassTransit;
using System.Threading.Tasks;

public class EventPublisher<TEvent>
{
    private readonly IBus _bus;

    public EventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task Publish(TEvent eventMessage)
    {
        await _bus.Publish(eventMessage);
    }
}