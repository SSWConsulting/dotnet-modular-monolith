using Common.SharedKernel.Domain.Entities;
using FluentAssertions;
using Modules.Catalog.Categories.Domain;
using Modules.Catalog.Products;

namespace Modules.Catalog.Tests.Products;

public class ProductTests
{
    [Fact]
    public void Create_ShouldThrowException_WhenNameIsNull()
    {
        // Act
        var act = () => Product.Create(null!, "ValidSku");

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldThrowException_WhenNameIsEmpty()
    {
        // Act
        var act = () => Product.Create(string.Empty, "ValidSku");

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldThrowException_WhenNameIsWhiteSpace()
    {
        // Act
        var act = () => Product.Create(" ", "ValidSku");

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldThrowException_WhenSkuIsNull()
    {
        // Act
        var act = () => Product.Create("ValidName", null!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldThrowException_WhenSkuIsEmpty()
    {
        // Act
        var act = () => Product.Create("ValidName", string.Empty);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldThrowException_WhenSkuIsWhiteSpace()
    {
        // Act
        var act = () => Product.Create("ValidName", " ");

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldReturnProduct_WhenNameAndSkuAreValid()
    {
        // Act
        var product = Product.Create("ValidName", "ValidSku");

        // Assert
        product.Name.Should().Be("ValidName");
        product.Sku.Should().Be("ValidSku");
        product.Id.Should().NotBeNull();
    }

    [Fact]
    public void UpdatePrice_ShouldThrowException_WhenPriceIsNegative()
    {
        // Arrange
        var product = Product.Create("ValidName", "ValidSku");

        // Act
        Action act = () => product.UpdatePrice(Money.Create(-1));

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void UpdatePrice_ShouldThrowException_WhenPriceIsZero()
    {
        // Arrange
        var product = Product.Create("ValidName", "ValidSku");

        // Act
        var act = () => product.UpdatePrice(Money.Create(0));

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void UpdatePrice_ShouldUpdatePrice_WhenPriceIsPositive()
    {
        // Arrange
        var product = Product.Create("ValidName", "ValidSku");
        var newPrice = Money.Create(100);

        // Act
        product.UpdatePrice(newPrice);

        // Assert
        product.Price.Should().Be(newPrice);
    }

    [Fact]
    public void AddCategory_ShouldAddCategory_WhenCategoryIsNotDuplicate()
    {
        // Arrage
        var product = Product.Create("ValidName", "ValidSku");
        var category = Category.Create("Category1");

        // Act
        product.AddCategory(category);

        // Assert
        product.Categories.Should().Contain(category);
    }

    [Fact]
    public void AddCategory_ShouldNotAddCategory_WhenCategoryIsDuplicate()
    {
        // Arrange
        var product = Product.Create("ValidName", "ValidSku");
        var category = Category.Create("Category1");

        // Act
        product.AddCategory(category);
        product.AddCategory(category);

        // Assert
        product.Categories.Count.Should().Be(1);
    }

    [Fact]
    public void RemoveCategory_ShouldRemoveCategory_WhenCategoryExists()
    {
        // Arrange
        var product = Product.Create("ValidName", "ValidSku");
        var category = Category.Create("Category1");

        // Act
        product.AddCategory(category);
        product.RemoveCategory(category);

        // Assert
        product.Categories.Should().NotContain(category);
        product.Categories.Count.Should().Be(0);
    }

    [Fact]
    public void RemoveCategory_ShouldNotThrowException_WhenCategoryDoesNotExist()
    {
        // Arrange
        var product = Product.Create("ValidName", "ValidSku");
        var category = Category.Create("Category1");

        // Act
        Action act = () => product.RemoveCategory(category);

        // Assert
        act.Should().NotThrow();
    }
}
