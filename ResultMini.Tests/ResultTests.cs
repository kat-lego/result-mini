using ResultMini.Exceptions;

namespace ResultMini.Tests;

public class ResultTests
{

    [Fact]
    public void ImplicitConversion_FromError_ShouldCreateResultWithSingleError()
    {
        // Arrange
        var expectedError = new Error(1, "Test Error");

        // Act
        Result<int> result = expectedError;

        // Assert
        var (data, errors) = result;

        Assert.Equal(data, default);
        Assert.Equal(errors.Count(), 1);
        Assert.Equivalent(errors.FirstOrDefault(), expectedError);
    }

    [Fact]
    public void ImplicitConversion_FromErrorArray_ShouldCreateResultWithErrors()
    {
        // Arrange
        var expectedErrors = new[]
        {
            new Error(1, "Error 1"),
            new Error(2, "Error 2")
        };

        // Act
        Result<int> result = expectedErrors;

        // Assert
        var (data, errors) = result;

        Assert.Equal(data, default);
        Assert.Equal(errors.Count(), 2);
        Assert.Equivalent(errors, expectedErrors);
    }

    [Fact]
    public void Deconstruction_ShouldReturnDataAndErrors()
    {
        // Arrange
        var expectedData = 100;
        Result<int> result = expectedData;

        // Act
        var (data, errors) = result;

        // Assert
        Assert.Equal(expectedData, data);
        Assert.Empty(errors);
    }
}
