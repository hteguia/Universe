using Domain.Base;

namespace Domain.Features.DocumentTypes.Repositories;

public class DocumentType : BaseEntity
{
    public string Name { get; internal set; }
    public string Description { get; internal set; }

    public DocumentType()
    {
        
    }
    
    public DocumentType(long id, string name, string description) : this()
    {
        if(id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0");
        }
        
        Id = id;
        Update(name, description);
    }
    
    public DocumentType(string name, string description) : this()
    {
        Update(name, description);
    }

    private void Update(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name must not be null or empty");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description must not be null or empty");
        }
        
        Name = name;
        Description = description;
    }
    
    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description must not be null or empty");
        }
        
        Description = description;
    }
    
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name must not be null or empty");
        }
        
        Name = name;
    }
}