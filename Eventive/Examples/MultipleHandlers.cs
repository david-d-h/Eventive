using Eventive.Models;

// ReSharper disable once CheckNamespace
namespace Eventive.Examples.MultipleHandlers;

internal class SomeEvent : Event
{
    //
}

internal class AnotherEvent : Event
{
    //
}

internal class SomeListener : Listener,
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

internal class EventService : EventServiceProvider
{
    private protected override Dictionary<Type, Type[]> Listen { get; } = new()
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

public class MultipleHandlers
{
    public static void Main()
    {
        var eventService = new EventService();
        eventService.InvokeEvent(new SomeEvent());
        eventService.InvokeEvent(new AnotherEvent());
    }
}