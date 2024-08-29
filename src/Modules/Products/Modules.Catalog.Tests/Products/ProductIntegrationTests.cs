using Ardalis.Specification.EntityFrameworkCore;
using FluentAssertions;
using Modules.Catalog.Categories.Domain;
using Modules.Catalog.Products.Domain;
using Modules.Catalog.Tests.Common;
using System.Net;
using Xunit.Abstractions;

namespace Modules.Catalog.Tests.Products;

public class ProductIntegrationTests(CatalogDatabaseFixture fixture, ITestOutputHelper output)
    : CatalogIntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task AddProductCategory_ValidRequest_ShouldReturnNotContent()
    {
        // Arrange
        var client = GetAnonymousClient();
        var category = Category.Create("category");
        await AddEntityAsync(category);
        var product = Product.Create("product", "12345678");
        await AddEntityAsync(product);

        // Act
        var response = await client.PostAsync($"/api/products/{product.Id.Value}/categories/{category.Id.Value}",null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var updatedProduct = GetQueryable<Product>().WithSpecification(new ProductByIdSpec(new ProductId(product.Id.Value))).FirstOrDefault();
        updatedProduct.Should().NotBeNull();
        updatedProduct!.Categories.Should().HaveCount(1);
        updatedProduct.Categories[0].Should().BeEquivalentTo(category);
    }

    [Fact]
    public async Task RemoveProductCategory_ValidRequest_ShouldReturnNotContent()
    {
        // Arrange
        var client = GetAnonymousClient();
        var category = Category.Create("category");
        await AddEntityAsync(category);
        var product = Product.Create("product", "12345678");
        await AddEntityAsync(product);
        product.AddCategory(category);
        await SaveAsync();

        // Act
        var response = await client.DeleteAsync($"/api/products/{product.Id.Value}/categories/{category.Id.Value}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var updatedProduct = GetQueryable<Product>().WithSpecification(new ProductByIdSpec(new ProductId(product.Id.Value))).FirstOrDefault();
        updatedProduct.Should().NotBeNull();
        updatedProduct!.Categories.Should().HaveCount(0);
    }
}
