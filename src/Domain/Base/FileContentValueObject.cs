namespace Domain.Base;

public class FileContentValueObject : IEquatable<FileContentValueObject>
{
    public string Name { get; }
    public byte[] Content { get; }

    public FileContentValueObject(string name, byte[] content)
    {
        Name = name;
        Content = content;
    }

    public bool Equals(FileContentValueObject? other)
    {
        if (other == null) return false;
        
        return Name == other.Name && Content.SequenceEqual(other.Content);
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((FileContentValueObject) obj);
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Content);
    }
    
    public static bool operator ==(FileContentValueObject left, FileContentValueObject right)
    {
        return Equals(left, right);
    }
    
    public static bool operator !=(FileContentValueObject left, FileContentValueObject right)
    {
        return !Equals(left, right);
    }
    
    public override string ToString()
    {
        return $"{Name} - {Content.Length} bytes";
    }
}