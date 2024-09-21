using Google.Cloud.Firestore;

namespace SkylandersCsvLoader.Models;

[FirestoreData]
public class SkylanderDocument
{
    [FirestoreProperty]
    public int id { get; set; }

    [FirestoreProperty]
    public int elementId { get; set; }

    [FirestoreProperty]
    public int gameId { get; set; }

    [FirestoreProperty]
    public int categoryId { get; set; }

    [FirestoreProperty]
    public required string name { get; set; }

    [FirestoreProperty]
    public string? gender { get; set; }

    [FirestoreProperty]
    public string? imageLink { get; set; }

    [FirestoreProperty]
    public string? quote { get; set; }

    [FirestoreProperty]
    public string? soundByteLink { get; set; }

    [FirestoreProperty]
    public string? figureLink { get; set; }
}