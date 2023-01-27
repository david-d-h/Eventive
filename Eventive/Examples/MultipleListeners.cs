using Eventive.Models;

// ReSharper disable once CheckNameSpace
namespace Eventive.Examples.MultipleListeners;

internal class SomeEvent : Event
{
    //
}

internal class SomeListener : Listener,
    IHandles<SomeEvent>
{
    public void Handle(SomeEvent @event)
    {
        Console.WriteLine("SomeListener");
    }
}

internal class AnotherListener : Listener,
    IHandles<SomeEvent>
{
    public void Handle(SomeEvent @event)
    {
        Console.WriteLine("AnotherListener");
    }
}

internal class EventService : EventServiceProvider
{
    // To register multiple listeners for one event,
    // just add multiple listeners to the array at the index of Event<SomeEvent>(). 
    private protected override Dictionary<Type, Type[]> Listen { get; } = new()
    {
        [Event<SomeEvent>()] = new []
        {
            Listener<SomeListener>(),
            Listener<AnotherListener>()
        }
    };
} 

public class MultipleListeners
{
    public static void Main()
    {
        var eventService = new EventService();
        eventService.InvokeEvent(new SomeEvent());
    }
}