namespace Domain.Interfaces.Providers;

public interface IFileProvider
{
    public string SaveFile(string name, byte[] content);

    public string SaveFile(string folder, string name, byte[] content);
}