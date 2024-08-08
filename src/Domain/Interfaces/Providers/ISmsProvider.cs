namespace Domain.Interfaces.Providers;

public interface ISmsProvider
{
    Task Send(string phoneNumber, string message);
}