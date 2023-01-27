namespace Eventive.Models;

/// <summary>
/// An interface that should be implemented by any <see cref="Listener"/> to indicate what events they handle.
/// </summary>
/// <typeparam name="T">The type of the Event this handler should cover</typeparam>
public interface IHandles<in T>
    where T : Event, IEvent
{
    /// <summary>
    /// This is the method that gets called by the Handle method on the <see cref="IListener"/> interface to handle an event.
    /// </summary>
    /// <param name="event">The event instance which will be passed to the listener</param>
    public void Handle(T @event);
}

/// <summary>
/// This interface should be either implemented or have an inherited implementation by all listeners.
/// </summary>
public interface IListener
{
    /// <summary>
    /// This is the method that gets called by the <see cref="EventServiceProvider"/> to resolve which Handle method should be called on the current <see cref="Listener"/>,
    /// it is necessary because a listener can handle multiple events by implementing the <see cref="IHandles{T}"/> interface.
    /// </summary>
    /// <param name="event">The event instance that will be passed to the resolved handler</param>
    /// <typeparam name="T">The type of the event that should be handled by the listener</typeparam>
    public void Handle<T>(T @event)
        where T : Event, IEvent
    {
        (this as IHandles<T>)?.Handle(@event);
    }
}

/// <summary>
/// The class that all custom listeners should inherit from.
/// </summary>
public class Listener :
    IListener
{
    //
}