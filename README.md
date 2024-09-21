# Skylanders Firebase Console Application

## Overview

This console application connects to Google Firebase and manages Skylander-related data by reading from a series of CSV files. It retrieves existing Skylanders, Elements, Categories, and Games from Firebase, parses new data from CSV files, and uploads any new Skylanders, Elements, and Categories that do not already exist in the Firebase Firestore database.

## Features

-   **Firebase Integration**: Connects to Firebase Firestore to retrieve and store data on Skylanders, Elements, Categories, and Games.
-   **CSV Data Import**: Reads multiple CSV files containing Skylander data and processes them into Skylander, Category, and Element objects.
-   **Duplicate Handling**: Compares existing Firestore data with CSV data to prevent duplicates.
-   **Automatic Data Upload**: Pushes newly created Skylander, Category, and Element objects to Firebase.
-   **Relationship Mapping**: Automatically maps the Skylander object to its corresponding Category and Element by looking up existing entries in Firestore.

## Prerequisites

-   Google Firebase Admin SDK (Firestore)
-   CSV files with Skylander data (in the `csv_files` folder)
-   A valid Firebase service account key file (`skylanders-collection-app-firebase-adminsdk-chcjw-0c7c2f0336.json`)

## Setup

### 1. Firebase Configuration

1.  Create a Firebase project and enable Firestore.
2.  Download the Firebase Admin SDK private key (JSON).
3.  Place the service account key file (`skylanders-collection-app-firebase-adminsdk-chcjw-0c7c2f0336.json`) in the project root directory.

### 2. CSV File Format

The CSV files should follow this format:

`Category,Name,Image,Have,Need,Duplicates,Element,Vehicle Type,Link,Image Link`

Each CSV file represents Skylanders from a specific game and should be named `skylanders0.csv`, `skylanders1.csv`, etc.

## Running the Application

### 1. Prepare the CSV Files

Ensure that the CSV files are placed in the `csv_files` directory inside the project root. The application expects the files to be named `skylanders0.csv`, `skylanders1.csv`, and so on.

### 2. Run the Application

Execute the following command in the terminal to run the application:

`dotnet run` 

### 3. What Happens During Execution:

-   **Firebase Initialization**: The application sets up the Firebase connection using the service account key.
-   **Data Retrieval**: The application retrieves existing Skylanders, Categories, Elements, and Games from Firestore.
-   **CSV Parsing**: The application reads and processes each CSV file to extract Skylander data.
-   **Element and Category Creation**: It creates new Elements and Categories if they do not already exist.
-   **Skylander Creation**: The application maps Skylanders to their corresponding Category and Element and uploads them to Firestore.
-   **Completion**: A summary is displayed, indicating how many Skylanders, Categories, and Elements were created.

## Example Output

`Connected to Firebase successfully.`
`Retrieving existing Skylanders, Categories, and Elements...`
`Reading CSV files...`
`Created Category: Magic`
`Created Category: Tech`
`Created Element: Fire`
`Created Element: Gold`
`Created Skylander: Spyro`
`Created Skylander: TriggerHappy`
`Elements Created: 2`
`Categories Created: 2`
`Skylanders Created: 2` 

## Code Structure

-   **`Program.cs`**: Main entry point that handles Firebase connection, CSV parsing, object creation, and Firestore document uploads.
-   **`FirestoreService.cs`**: A service for interacting with Firebase Firestore, including retrieving and adding Skylanders, Categories, and Elements.
-   **`Skylander.cs`, `Category.cs`, `Element.cs`**: Models representing Skylanders, Categories, and Elements in the project context.
-   **CSV Parsing Logic**: The application uses `TextFieldParser` to process CSV files and extract data row by row.

## Error Handling

-   **Missing Files**: If any CSV file is missing or in an incorrect format, the application will throw an error.
-   **Firestore Errors**: If there is an issue connecting to Firebase or adding data, the application will log the error and terminate.

## Future Improvements

-   Add logging for better error tracking and debugging.
-   Improve error handling and validation of CSV data.
-   Add unit tests for key functionalities like CSV parsing and Firestore integration.
-   Provide support for different file formats (e.g., JSON or XML).
