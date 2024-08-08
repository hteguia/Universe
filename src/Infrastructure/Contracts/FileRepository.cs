using System.IO.Abstractions;
 
using Domain.Contracts;

namespace Infrastructure.Contracts;

public class FileRepository : IFileRepository
{
    public readonly IFileSystem _fileSystem;
    private const string _folder = @"c:\demo";
    
    public FileRepository(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }
    public string SaveFile(string name, byte[] content)
    {
        if (!_fileSystem.Directory.Exists(_folder))
        {
            _fileSystem.Directory.CreateDirectory(_folder);
        }

        string fileFolder = Path.Combine(_folder, $"{name}");
        var fileStream = _fileSystem.File.Create(fileFolder);
        fileStream.Write(content, 0, content.Length);

        return fileFolder;
    }

    public string SaveFile(string folder, string name, byte[] content)
    {
        if (!_fileSystem.Directory.Exists(folder))
        {
            _fileSystem.Directory.CreateDirectory(folder);
        }

        string fileFolder = Path.Combine(folder, $"{name}");
        var fileStream = _fileSystem.File.Create(fileFolder);
        fileStream.Write(content, 0, content.Length);

        return fileFolder;
    }
}