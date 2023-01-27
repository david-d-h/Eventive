using Eventive;
using Eventive.Models;

namespace Examples;

public class MultipleListeners
{
    protected class SomeEvent : Event
    {
        //
    }

    protected class SomeListener : Listener,
        IHandles<SomeEvent>
    {
        public void Handle(SomeEvent @event)
        {
            Console.WriteLine("SomeListener");
        }
    }

    protected class AnotherListener : Listener,
        IHandles<SomeEvent>
    {
        public void Handle(SomeEvent @event)
        {
            Console.WriteLine("AnotherListener");
        }
    }

    protected class EventService : EventServiceProvider
    {
        // To register multiple listeners for one event,
        // just add multiple listeners to the array at the index of Event<SomeEvent>(). 
        protected override Dictionary<Type, Type[]> Listen { get; } = new()
        {
            [Event<SomeEvent>()] = new []
            {
                Listener<SomeListener>(),
                Listener<AnotherListener>()
            }
        };
    }
    
    public static void Main()
    {
        var eventService = new EventService();
        eventService.InvokeEvent(new SomeEvent());
    }
}