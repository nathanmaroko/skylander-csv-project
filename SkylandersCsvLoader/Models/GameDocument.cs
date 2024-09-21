using Google.Cloud.Firestore;

namespace SkylandersCsvLoader.Models;

[FirestoreData]
public class GameDocument
{
    [FirestoreProperty]
    public int id { get; set; }

    [FirestoreProperty]
    public required string name { get; set; }

    [FirestoreProperty]
    public DateTimeOffset releaseDate { get; set; }
}
