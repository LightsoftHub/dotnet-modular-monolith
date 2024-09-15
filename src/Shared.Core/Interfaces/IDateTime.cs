namespace ModularMonolith.Core.Interfaces;

public interface IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
