using Google.Cloud.Firestore;

namespace SkylandersCsvLoader.Models;

[FirestoreData]
public class CategoryDocument
{
    [FirestoreProperty]
    public int id { get; set; }

    [FirestoreProperty]
    public required string name { get; set; }
}
