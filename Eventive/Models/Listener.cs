namespace Eventive.Models;

internal interface IHandles<in T>
    where T : Event, IEvent
{
    public void Handle(T @event);
}

public interface IListener
{
    public void Handle<T>(T @event)
        where T : Event, IEvent
    {
        (this as IHandles<T>)?.Handle(@event);
    }
}

public class Listener :
    IListener
{
    //
}