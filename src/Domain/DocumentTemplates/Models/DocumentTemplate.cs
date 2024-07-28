using Domain.Base;

namespace Domain.DocumentTemplates.Models;

public class DocumentTemplate : BaseEntity
{
    public string Name { get; internal set; }
    public string Path { get; internal set; }

    public DocumentTemplate()
    {
        
    }
    
    public DocumentTemplate(string name, string path) : this()
    {
        this.Update(name, path);
    }

    public void Update(string name, string path)
    {
        this.Name = name;
        this.Path = path;
    }
}