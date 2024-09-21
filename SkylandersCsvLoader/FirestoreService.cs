using Google.Cloud.Firestore;
using SkylandersCsvLoader.Models;
using System.Xml.Linq;

namespace SkylandersCsvLoader;

/**
 * This service is used to communicate with firestore. It's setup using dependency injection
 * These methods are called to interact with the firestore api
 */
public class FirestoreService
{
    private readonly FirestoreDb _firestoreDb;

    public FirestoreService(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }

    /** SKYLANDERS METHODS **/
    public async Task<List<Skylander>> GetAllSkylanders()
    {
        var collection = _firestoreDb.Collection("skylanders");
        var snapshot = await collection.GetSnapshotAsync();

        var skylanderDocuments = snapshot.Documents.Select(s => s.ConvertTo<SkylanderDocument>()).ToList();
        return skylanderDocuments.Select(ConvertDocumentToModel).ToList();
    }

    public async Task AddSkylanderAsync(Skylander skylander)
    {
        var collection = _firestoreDb.Collection("skylanders");
        var skylanderDocument = ConvertModelToDocument(skylander);
        await collection.AddAsync(skylanderDocument);
    }

    private static Skylander ConvertDocumentToModel(SkylanderDocument skylander)
    {
        return new Skylander
        {
            Id = skylander.id,
            ElementId = skylander.elementId,
            GameId = skylander.gameId,
            CategoryId = skylander.categoryId,
            Name = skylander.name,
            Gender = skylander.gender,
            ImageLink = skylander.imageLink,
            Quote = skylander.quote,
            SoundByteLink = skylander.soundByteLink,
            FigureLink = skylander.figureLink
        };
    }
    private static SkylanderDocument ConvertModelToDocument(Skylander skylander)
    {
        return new SkylanderDocument
        {
            id = skylander.Id,
            elementId = skylander.ElementId,
            gameId = skylander.GameId,
            categoryId = skylander.CategoryId,
            name = skylander.Name,
            gender = skylander.Gender,
            imageLink = skylander.ImageLink,
            quote = skylander.Quote,
            soundByteLink = skylander.SoundByteLink,
            figureLink = skylander.FigureLink
        };
    }

    /** ELEMENT METHODS **/
    public async Task AddElementAsync(Element element)
    {
        var collection = _firestoreDb.Collection("elements");
        var elementDocument = ConvertModelToDocument(element);
        await collection.AddAsync(elementDocument);
    }

    public async Task<List<Element>> GetAllElements()
    {
        var collection = _firestoreDb.Collection("elements");
        var snapshot = await collection.GetSnapshotAsync();

        var elementDocuments = snapshot.Documents.Select(s => s.ConvertTo<ElementDocument>()).ToList();
        return elementDocuments.Select(ConvertDocumentToModel).ToList();
    }

    private static Element ConvertDocumentToModel(ElementDocument element)
    {
        return new Element
        {
            Id = element.id,
            Name = element.name,
            ImageLink = element.imageLink
        };
    }

    private static ElementDocument ConvertModelToDocument(Element element)
    {
        return new ElementDocument
        {
            id = element.Id,
            name = element.Name,
            imageLink = element.ImageLink
        };
    }

    /** CATEGORY METHODS **/
    public async Task AddCategoryAsync(Category category)
    {
        var collection = _firestoreDb.Collection("categories");
        var categoryDocument = ConvertModelToDocument(category);
        await collection.AddAsync(categoryDocument);
    }

    public async Task<List<Category>> GetAllCategories()
    {
        var collection = _firestoreDb.Collection("categories");
        var snapshot = await collection.GetSnapshotAsync();

        var categoryDocuments = snapshot.Documents.Select(s => s.ConvertTo<CategoryDocument>()).ToList();
        return categoryDocuments.Select(ConvertDocumentToModel).ToList();
    }

    private static Category ConvertDocumentToModel(CategoryDocument category)
    {
        return new Category
        {
            Id = category.id,
            Name = category.name
        };
    }

    private static CategoryDocument ConvertModelToDocument(Category category)
    {
        return new CategoryDocument
        {
            id = category.Id,
            name = category.Name
        };
    }

    /** GAME METHODS **/
    public async Task AddGameAsync(Game game)
    {
        var collection = _firestoreDb.Collection("games");
        var gameDocument = ConvertModelToDocument(game);
        await collection.AddAsync(gameDocument);
    }

    public async Task<List<Game>> GetAllGames()
    {
        var collection = _firestoreDb.Collection("games");
        var snapshot = await collection.GetSnapshotAsync();

        var categoryDocuments = snapshot.Documents.Select(s => s.ConvertTo<GameDocument>()).ToList();
        return categoryDocuments.Select(ConvertDocumentToModel).ToList();
    }

    private static Game ConvertDocumentToModel(GameDocument game)
    {
        return new Game
        {
            Id = game.id,
            Name = game.name,
            ReleaseDate = game.releaseDate
        };
    }

    private static GameDocument ConvertModelToDocument(Game game)
    {
        return new GameDocument
        {
            id = game.Id,
            name = game.Name,
            releaseDate = game.ReleaseDate
        };
    }
}
