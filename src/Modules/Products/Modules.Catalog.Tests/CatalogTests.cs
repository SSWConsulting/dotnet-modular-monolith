using FluentAssertions;
using Modules.Catalog.Categories.Domain;

namespace Modules.Catalog.Tests;

public class CatalogTests
{
    [Fact]
    public void Create_ShouldThrowException_WhenNameIsNull()
    {
        // Arrange
        Action act = () => Category.Create(null!);

        // Act & Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldThrowException_WhenNameIsEmpty()
    {
        // Arrange
        Action act = () => Category.Create(string.Empty);

        // Act & Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldThrowException_WhenNameIsWhiteSpace()
    {
        // Arrange
        Action act = () => Category.Create(" ");

        // Act & Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldReturnCategory_WhenNameIsValid()
    {
        // Arrange
        var name = "ValidName";

        // Act
        var category = Category.Create(name);

        // Assert
        category.Name.Should().Be(name);
        category.Id.Should().NotBeNull();
    }
}
