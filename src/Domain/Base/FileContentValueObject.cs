namespace Domain.Base;

public sealed class FileContentValueObject(string name, byte[] content) : IEquatable<FileContentValueObject>
{
    public string Name { get; } = name;
    public byte[] Content { get; } = content;

    public bool Equals(FileContentValueObject other)
    {
        if (other == null) return false;
        
        return Name == other.Name && Content.SequenceEqual(other.Content);
    }
    
    public override bool Equals(object obj)
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