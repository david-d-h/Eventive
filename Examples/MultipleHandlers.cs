using Eventive;
using Eventive.Models;

namespace Examples;

public class MultipleHandlers
{
    protected class SomeEvent : Event
    {
        //
    }
    
    protected class AnotherEvent : Event
    {
        //
    }
    
    protected class SomeListener : Listener,
        IHandles<SomeEvent>,
        IHandles<AnotherEvent>
    {
        public void Handle(SomeEvent @event)
        {
            Console.WriteLine("Handling SomeEvent");
        }

        public void Handle(AnotherEvent @event)
        {
            Console.WriteLine("Handling AnotherEvent");
        }
    }
    
    protected class EventService : EventServiceProvider
    {
        protected override Dictionary<Type, Type[]> Listen { get; } = new()
        {
            [Event<SomeEvent>()] = new[]
            {
                Listener<SomeListener>()
            },
        
            [Event<AnotherEvent>()] = new[]
            {
                Listener<SomeListener>()
            }
        };
    }
    
    public static void Main()
    {
        var eventService = new EventService();
        eventService.InvokeEvent(new SomeEvent());
        eventService.InvokeEvent(new AnotherEvent());
    }
}