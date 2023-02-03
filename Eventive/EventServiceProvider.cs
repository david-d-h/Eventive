using Eventive.Exceptions;
using Eventive.Exceptions.EventMapping;
using Eventive.Models;

namespace Eventive;

public abstract class EventServiceProvider
{
	/// <summary>
    /// The externally defined dictionary that holds the Event -> Listener mapping.
    /// </summary>
    protected abstract Dictionary<Type, Type[]> Listen { get; }

	/// <summary>
	/// Checks whether the Event -> Listener mapping that was the defined doesn't contain invalid types, else throws.
	/// </summary>
	/// <exception cref="InvalidEventMappingTypesException">This exception gets thrown if the checks have failed.</exception>
	protected EventServiceProvider()
	{
		if (!CheckDefinedMapping())
			throw new InvalidEventMappingTypesException();
	}

	/// <summary>
    /// A helper method that resolves an event into it's underlying type and returns it.
    /// </summary>
    /// <typeparam name="T">The type of the event that should be registered</typeparam>
    /// <returns>A type that can be used for registering an event</returns>
    protected static Type Event<T>()
        where T : Event
    {
        return typeof(T);
    }

    /// <summary>
    /// A helper method that resolves a listener into it's underlying type and returns it.
    /// </summary>
    /// <typeparam name="T">The type of the listener that should be registered</typeparam>
    /// <returns>A type that can be used for registering a listener</returns>
    protected static Type Listener<T>()
        where T : Listener, new()
    {
        return typeof(T);
    }

    /// <summary>
    /// The method that should be called to invoke an event, this in turn calls the appropriate method for handling a specific event on an <see cref="IListener"/>.
    /// </summary>
    /// <param name="event">The event instance that will be passed to the registered listeners</param>
    /// <typeparam name="T">The type of the event that will be handled by the registered listeners.</typeparam>
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

    /// <summary>
    /// This function takes the defined Event -> Listener mapping and checks whether all defined Events and Listeners inherit from their corresponding types.
    /// </summary>
    /// <returns>Value based off whether all checks were successful.</returns>
    private bool CheckDefinedMapping()
    {
	    foreach (var binding in Listen)
	    {
		    if (!binding.Key.IsSubclassOf(typeof(Event)))
			    return false;

		    if (binding.Value.Any(listenerT => !listenerT.IsSubclassOf(typeof(Listener))))
			    return false;
	    }

	    return true;
    }
}