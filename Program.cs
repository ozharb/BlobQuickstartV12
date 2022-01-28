using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlobQuickstartV12
{
    class Program
    {
        private static async Task GetBlobPropertiesAsync(BlobClient blob)
{
    try
    {
        // Get the blob properties
        BlobProperties properties = await blob.GetPropertiesAsync();

        // Display some of the blob's property values
        Console.WriteLine($" ContentLanguage: {properties.ContentLanguage}");
        Console.WriteLine($" ContentType: {properties.ContentType}");
        Console.WriteLine($" CreatedOn: {properties.CreatedOn}");
        Console.WriteLine($" LastModified: {properties.LastModified}");
        Console.WriteLine($" LastAccessed: {properties.LastAccessed}");
    }
    catch (Exception e)
    {
    
        Console.WriteLine(e.Message);
        Console.ReadLine();
    }
}
        static async Task Main(string[] args)
        {
            Console.WriteLine("Azure Blob Storage v12 - .NET quickstart sample\n");

// Retrieve the connection string for use with the application. The storage
// connection string is stored in an environment variable on the machine
// running the application called AZURE_STORAGE_CONNECTION_STRING. If the
// environment variable is created after the application is launched in a
// console or with Visual Studio, the shell or application needs to be closed
// and reloaded to take the environment variable into account.
string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
// Create a BlobServiceClient object which will be used to create a container client
BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

//Create a unique name for the container
//string containerName = "quickstartblobs" + Guid.NewGuid().ToString();
string containerName = args[0];
// Create the container and return a container client object
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
Console.WriteLine("Listing blobs...");

// List all blobs in the container
await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
{
   Console.WriteLine(blobItem.Name);
   BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);
   await GetBlobPropertiesAsync(blobClient);
         
}

        }
    }
}