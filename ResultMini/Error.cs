namespace ResultMini;

/// <summary>
/// Represents an error with a code and a detailed message.
/// </summary>
public struct Error
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    public int Code { get; }

    /// <summary>
    /// Gets the detailed error message.
    /// </summary>
    public string Detail { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="detail">The detailed error message.</param>
    public Error(int code, string detail)
    {
        Code = code;
        Detail = detail;
    }
}


