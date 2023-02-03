using Eventive;
using Eventive.Models;

namespace Examples;

public class EventData
{
    // This is how to define an event that holds data.
    protected class DataEvent : Event
    {
        public readonly string Planet;

        public DataEvent(string planet)
        {
            Planet = planet;
        }
    }

    protected class DataListener : Listener,
        IHandles<DataEvent>
    {
        public void Handle(DataEvent @event)
        {
            Console.WriteLine($"Hello from {@event.Planet}!");
        }
    }

    // There is nothing special going on here, it's the exact same as the Basic example.
    protected class EventService : EventServiceProvider
    {
        protected override Dictionary<Type, Type[]> Listen { get; } = new()
        {
            [Event<DataEvent>()] = new []
            {
                Listener<DataListener>()
            }
        };
    }

    public static void Main()
    {
        var eventService = new EventService();
        eventService.InvokeEvent(new DataEvent("Mars"));
    }
}