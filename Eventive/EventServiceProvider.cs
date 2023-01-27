using Eventive.Models;

namespace Eventive;

public abstract class EventServiceProvider
{
    /// <summary>
    /// The externally defined dictionary that holds the Event -> Listener mappings.
    /// </summary>
    private protected abstract Dictionary<Type, Type[]> Listen { get; }

    protected static Type Event<T>()
        where T : Event
    {
        return typeof(T);
    }

    protected static Type Listener<T>()
        where T : Listener, new()
    {
        return typeof(T);
    }

    public void InvokeEvent<T>(T @event)
        where T : Event, IEvent
    {
        var subscribers = Listen[typeof(T)];

        foreach (var t in subscribers)
        {
            if (t.GetInterface(nameof(IListener)) == null) continue;
            if (Activator.CreateInstance(t) is IListener listener)
            {
                listener.Handle(@event);
            }
        }
    }
}