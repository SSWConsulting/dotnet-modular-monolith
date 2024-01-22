using Modules.Warehouse.Application.Common.Interfaces;
using Modules.Warehouse.Domain.Categories;

namespace Modules.Warehouse.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly IWarehouseDbContext _dbContext;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(IWarehouseDbContext dbContext, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name, _categoryRepository);

        _dbContext.Categories.Add(category);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
