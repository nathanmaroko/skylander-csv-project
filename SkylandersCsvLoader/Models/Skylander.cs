namespace SkylandersCsvLoader.Models;

public class Skylander
{
    public int Id { get; set; }

    public int ElementId { get; set; }
    public string? Element { get; set; }

    public int GameId { get; set; }

    public int CategoryId { get; set; }
    public string? Category { get;set; }

    public string Name { get; set; }

    public string? Gender { get; set; }

    public string? ImageLink { get; set; }

    public string? Quote { get; set; }

    public string? SoundByteLink { get; set; }

    public string? FigureLink { get; set; }

    public Skylander()
    {

    }

    public Skylander(int id, string element, int gameId, string category, string name, string gender, string imageLink, string quote, string soundByteLink, string figureLink)
    {
        Id = id;
        Element = element;
        GameId = gameId;
        Category = category;
        Name = name;
        Gender = gender;
        ImageLink = imageLink;
        Quote = quote;
        SoundByteLink = soundByteLink;
        FigureLink = figureLink;
    }

    public override bool Equals(object? obj)
    {
        return this.Name == ((Skylander)obj!).Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
