namespace ResultMini.Tests;

public class ResultTests
{

    [Fact]
    public void ImplicitConversion_FromError_ShouldCreateResultWithSingleError()
    {
        // Arrange
        var expected = new Error(1, "Test Error");

        // Act
        Result<int> result = expected;

        // Assert
        var (data, errors) = result;

        Assert.Equal(default, data);
        Assert.Single(errors);
        Assert.Equivalent(expected, errors.FirstOrDefault());
    }

    [Fact]
    public void ImplicitConversion_FromErrorArray_ShouldCreateResultWithErrors()
    {
        // Arrange
        var expected = new[]
        {
            new Error(1, "Error 1"),
            new Error(2, "Error 2")
        };

        // Act
        Result<int> result = expected;

        // Assert
        var (data, errors) = result;

        Assert.Equal(default, data);
        Assert.Equal(2, errors.Count());
        Assert.Equivalent(expected, errors);
    }

    [Fact]
    public void Deconstruction_ShouldReturnDataAndErrors()
    {
        // Arrange
        var expected = 100;
        Result<int> result = expected;

        // Act
        var (data, errors) = result;

        // Assert
        Assert.Equal(expected, data);
        Assert.Empty(errors);
    }
}
