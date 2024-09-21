// See https://aka.ms/new-console-template for more information
// https://code-maze.com/dotnet-firebase/
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using SkylandersCsvLoader;
using SkylandersCsvLoader.Models;
using System.Xml.Linq;

var executablePath = AppDomain.CurrentDomain.BaseDirectory;
var serviceAccountKeyPath = Path.Combine(executablePath, "skylanders-collection-app-firebase-adminsdk-chcjw-0c7c2f0336.json");
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountKeyPath);

var services = new ServiceCollection();

// Register the FirestoreService as a singleton
services.AddSingleton(s => new FirestoreService(
    FirestoreDb.Create("skylanders-collection-app")
));

var serviceProvider = services.BuildServiceProvider();

// Resolve the FirestoreService from the service provider
var firestoreService = serviceProvider.GetRequiredService<FirestoreService>();

//Retrieve existing data from firebase (it's assumed that all games already exist on firebase)
List<Skylander> skylanders = new List<Skylander>();
skylanders = await firestoreService.GetAllSkylanders();

List<Element> elements = new List<Element>();
elements = await firestoreService.GetAllElements();

List<Category> categories = new List<Category>();
categories = await firestoreService.GetAllCategories();

List<Game> games = new List<Game>();
games = await firestoreService.GetAllGames();

int ctr = 0; // current row
int currentId = 0;

List<Skylander> csvSkylanders = new List<Skylander>();
List<Element> csvElements = new List<Element>();
List<Category> csvCategory = new List<Category>();

int currentGame = 0;
string csvStringBase = Path.Combine(executablePath, "csv_files/skylanders");

//Iterate through all 6 games
//Expected format of csv files: Category,Name,Image,Have,Need,Duplicates,Element,Vehicle Type,Link,Image Link
for (int i = 0; i < 6; i++)
{
    string csvStringFull = csvStringBase + currentGame + ".csv";

    using (TextFieldParser parser = new TextFieldParser(csvStringFull))
    {
        // Set the delimiter (comma in the case of CSV)
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");

        // Skip the first row if it contains headers (optional)
        parser.ReadLine();

        while (!parser.EndOfData) //Parse through until the end of the file
        {
            if (ctr == 0)
            {
                ctr++;
                parser.ReadFields();
                continue;
            }
            // Read each line and split it into an array of fields
            string[] columns = parser.ReadFields()!;

            //TODO restructure skylander constructor to better match csv column order
            csvSkylanders.Add(new Skylander(currentId, columns[6], currentGame, columns[0], columns[1], "", "", "", "", columns[9]));
            currentId++;
        }
    }

    //Initialize the csv elements - since there are multiples on each skylander we want the distinct ones (no duplicates)
    if(currentGame == 0)
    {
        int elementId = 0;
        var tmp = csvSkylanders.DistinctBy(x => x.Element).Select(x => x.Element);
        foreach(var element in tmp)
        {
            csvElements.Add(new Element { Id = elementId, Name = element, ImageLink = "" });
            elementId++;
        }
        csvElements.RemoveAt(8);
    }

    currentGame++;
}

// Initialize the csv categories - since there are multiples on each skylander we want the distinct ones (no duplicates)
int categoryId = 0;
var tmpCat = csvSkylanders.DistinctBy(x => x.Category).Select(x => x.Category);
foreach(var category in tmpCat)
{
    csvCategory.Add(new Category { Id = categoryId, Name = category! });
    categoryId++;
}

//Get elements, categories, and skylanders that don't exist on firebase
csvElements = csvElements.Except(elements).ToList();
csvCategory = csvCategory.Except(categories).ToList();
csvSkylanders = csvSkylanders.Except(skylanders).ToList();

// CREATE ALL CATEGORIES
foreach(var category in csvCategory)
{
    await firestoreService.AddCategoryAsync(category);
    Console.WriteLine("Created Category: " + category.Name);
}
categories = await firestoreService.GetAllCategories();

//CREATE ALL ELEMENTS
foreach (var element in csvElements)
{
    await firestoreService.AddElementAsync(element);
    Console.WriteLine("Created Element: " + element.Name);
}
elements = await firestoreService.GetAllElements();

//CREATE ALL SKYLANDERS
foreach(var skylander in csvSkylanders)
{
    skylander.CategoryId = categories.Single(x => x.Name == skylander.Category).Id!;
    int elementId = -1;
    var element = elements.SingleOrDefault(x => x.Name == skylander.Element);
    if (element != null)
        elementId = element.Id;
    skylander.ElementId = elementId;
    await firestoreService.AddSkylanderAsync(skylander);
    Console.WriteLine("Created Skylander: " + skylander.Name);
}

//Print statistics to the user
Console.WriteLine("Elements Created: " + csvElements.Count());
Console.WriteLine("Categories Created: " + csvCategory.Count());
Console.WriteLine("Skylanders Created: " + csvSkylanders.Count());