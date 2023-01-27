namespace Eventive.Models;

/// <summary>
/// An interface that should be implemented or have an inherited implementation by all events.
/// </summary>
public interface IEvent
{
    //
}

/// <summary>
/// The class that all should custom events inherit from.
/// </summary>
public abstract class Event :
    IEvent
{
    //
}