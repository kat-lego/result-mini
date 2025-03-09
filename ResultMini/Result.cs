using ResultMini.Exceptions;

namespace ResultMini;

/// <summary>
/// Represents a result that can either contain a value of type <typeparamref name="T"/> 
/// or a collection of errors.
/// </summary>
/// <typeparam name="T">The type of the result value.</typeparam>
public struct Result<T>
{
    private readonly T? _data;
    private readonly IEnumerable<Error> _errors = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with a value.
    /// </summary>
    /// <param name="data">The value of the result.</param>
    private Result(T data)
    {
        _data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with a collection of errors.
    /// </summary>
    /// <param name="errors">The collection of errors.</param>
    private Result(IEnumerable<Error> errors)
    {
        if (!errors.Any())
            throw new ResultEmptyErrorsInitiationException();

        _errors = errors;
        _data = default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with a single error.
    /// </summary>
    /// <param name="error">The error.</param>
    private Result(Error error)
    {
        _errors = [error];
        _data = default;
    }

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="data">The value to convert.</param>
    public static implicit operator Result<T>(T data)
    {
        return new Result<T>(data);
    }

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }

    /// <summary>
    /// Implicitly converts an array of <see cref="Error"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="errors">The array of errors to convert.</param>
    public static implicit operator Result<T>(Error[] errors)
    {
        return new Result<T>(errors);
    }

    /// <summary>
    /// Deconstructs the <see cref="Result{T}"/> into its value and errors.
    /// </summary>
    /// <param name="res">The result to deconstruct.</param>
    /// <returns>A tuple containing the value and the errors.</returns>
    public static implicit operator (T?, IEnumerable<Error>)(Result<T> res)
    {
        return (res._data, res._errors);
    }

    public void Deconstruct(out T? data, out IEnumerable<Error> errors)
    {
        data = _data;
        errors = _errors;
    }

}
