namespace ResultMini.Exceptions;

public class ResultEmptyErrorsInitiationException : Exception
{
    internal ResultEmptyErrorsInitiationException() : base("Cannot initialize result object with an empty array or null object") { }
}

