using Xunit;

namespace SlidingWindow.Tests;

public class SlidingWindowTests
{
    [Fact]
    public void MaxSumSubarray_ValidInput_ReturnsCorrectSum()
    {
        // Arrange
        int[] numbers = { 1, 4, 2, 10, 2, 3, 1, 0, 20 };
        int k = 4;

        // Act
        int result = SlidingWindowAlgorithm.MaxSumSubarray(numbers, k);

        // Assert
        Assert.Equal(25, result); // 2 + 3 + 0 + 20 = 25
    }

    [Fact]
    public void MaxSumSubarray_InvalidInput_ThrowsArgumentException()
    {
        // Arrange
        int[] numbers = { 1, 2, 3 };
        int k = 4;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            SlidingWindowAlgorithm.MaxSumSubarray(numbers, k));
    }

    [Fact]
    public void LongestUniqueSubstring_ValidInput_ReturnsCorrectLength()
    {
        // Arrange
        string text = "abcabcbb";

        // Act
        int result = SlidingWindowAlgorithm.LongestUniqueSubstring(text);

        // Assert
        Assert.Equal(3, result); // "abc" 是最長的不重複字元子字串
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void LongestUniqueSubstring_EmptyOrNullInput_ReturnsZero(string input)
    {
        // Act
        int result = SlidingWindowAlgorithm.LongestUniqueSubstring(input);

        // Assert
        Assert.Equal(0, result);
    }
}
