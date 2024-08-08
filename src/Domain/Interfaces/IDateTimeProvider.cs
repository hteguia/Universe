namespace Domain.Interfaces;

public interface IDateTimeProvider
{
    public DateTime UtcNow();
}