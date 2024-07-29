namespace Domain.Contracts;

public interface IFileRepository
{
    public string SaveFile(string name, byte[] content);
    
    public string SaveFile(string folder, string name, byte[] content);
}