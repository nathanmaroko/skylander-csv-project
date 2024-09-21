namespace SkylandersCsvLoader.Models;

public class Element
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? ImageLink { get; set; }

    public override bool Equals(object? obj)
    {
        return this.Name == ((Element)obj!).Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
