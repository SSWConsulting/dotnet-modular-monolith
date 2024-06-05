using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.SharedKernel;

public interface IModule
{
    void AddServices(IServiceCollection services, IConfiguration configuration);
}
