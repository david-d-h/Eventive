using Eventive.Models;

// ReSharper disable once CheckNamespace
namespace Eventive.Examples.WithData;

// This is how to define an event that holds data.
internal class DataEvent : Event
{
    public readonly string Planet;

    public DataEvent(string planet)
    {
        Planet = planet;
    }
}

internal class DataListener : Listener,
    IHandles<DataEvent>
{
    public void Handle(DataEvent @event)
    {
        Console.WriteLine($"Hello from {@event.Planet}!");
    }
}

// There is nothing special going on here, it's the exact same as the Basic example.
internal class EventService : EventServiceProvider
{
    private protected override Dictionary<Type, Type[]> Listen { get; } = new()
    {
        [Event<DataEvent>()] = new []
        {
            Listener<DataListener>()
        }
    };
}

public class EventData
{
    public static void Main()
    {
        var eventService = new EventService();
        eventService.InvokeEvent(new DataEvent("Mars"));
    }
}