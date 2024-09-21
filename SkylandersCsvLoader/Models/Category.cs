namespace SkylandersCsvLoader.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public override bool Equals(object? obj)
    {
        return this.Name == ((Category)obj!).Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
