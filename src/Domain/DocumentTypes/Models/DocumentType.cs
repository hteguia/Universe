using Domain.Base;

namespace Domain.DocumentTypes.Models;

public class DocumentType : BaseEntity
{
    public string Name { get; internal set; }
    public string Description { get; internal set; }

    public DocumentType()
    {
        
    }
    
    public DocumentType(string name, string description) : this()
    {
        this.Update(name, description);
    }

    public void Update(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }
}