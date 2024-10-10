namespace Common.SharedKernel.Identity;

// This should be implemented based on your configured identity provider
public class CurrentUserService : ICurrentUserService
{
    public string? UserId => "Admin";
}
