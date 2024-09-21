namespace SkylandersCsvLoader.Models;

public class Game
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset ReleaseDate { get; set; }
}
