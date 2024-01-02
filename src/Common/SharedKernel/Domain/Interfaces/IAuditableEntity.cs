namespace SharedKernel.Domain.Interfaces;

public interface IAuditableEntity
{
    void Created(DateTimeOffset dateTime, string? user);

    void Updated(DateTimeOffset dateTime, string? user);
}