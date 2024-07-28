namespace Domain.Contrats;

public interface IFileRepository
{
    public string SaveFile(string name, byte[] content);
}