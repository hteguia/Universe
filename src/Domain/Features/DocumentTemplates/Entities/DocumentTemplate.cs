using System.Text.RegularExpressions;
using Domain.Base;

namespace Domain.Features.DocumentTemplates.Entities;

public class DocumentTemplate : BaseEntity
{
    public string Name { get; internal set; }
    public string Path { get; internal set; }

    public DocumentTemplate()
    {

    }

    public DocumentTemplate(long id, string name, string path) : this()
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0");
        }

        Id = id;
        Update(name, path);
    }

    public DocumentTemplate(string name, string path) : this()
    {
        Update(name, path);
    }

    private void Update(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name must not be null or empty");
        }

        if (!Regex.IsMatch(name, @"\.pdf$|\.docx$", RegexOptions.IgnoreCase))
        {
            throw new ArgumentException("Name must be a valid file name");
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path must not be null or empty");
        }

        Name = name;
        Path = path;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name must not be null or empty");
        }

        Name = name;
    }

    public void UpdatePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path must not be null or empty");
        }

        Path = path;
    }
}