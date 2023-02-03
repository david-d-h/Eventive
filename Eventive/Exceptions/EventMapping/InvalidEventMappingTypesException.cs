namespace Eventive.Exceptions.EventMapping;

public class InvalidEventMappingTypesException : Exception
{
	public InvalidEventMappingTypesException()
	{
		Console.Error.WriteLine("There's invalid types in the Event mapping.");
	}

	public InvalidEventMappingTypesException(string message)
		: base(message)
	{
		//
	}

	public InvalidEventMappingTypesException(string message, Exception inner)
		: base(message, inner)
	{
		//
	}
}