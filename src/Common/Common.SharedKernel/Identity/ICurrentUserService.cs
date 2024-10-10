namespace Common.SharedKernel.Identity;

public interface ICurrentUserService
{
    public string? UserId { get; }
}
