using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Data.Tables;
using _02_homework;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=storage732t18731;AccountKey=QsgmzByK8trRCoZn4MAZTT8QXuvnfN+FPu+JiyuvtA2unl26mHc2mBgE5kCWuvAa0Rd28OxIdr1w+AStdFdmZA==;EndpointSuffix=core.windows.net"; // Insert your connection string here

        await WorkWithBlobStorage(connectionString);
        await WorkWithQueueStorage(connectionString);
        await WorkWithTableStorage(connectionString);
    }

    static async Task WorkWithBlobStorage(string connectionString)
    {
        Console.WriteLine("=== Blob Storage ===");

        string containerName = "mycontainer";
        string localFilePath = "localfile.txt";
        string downloadFilePath = "downloadedfile.txt";
        string blobName = "file.txt";

        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        
        File.WriteAllText(localFilePath, "Hello Azure Blob!");

        var blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.UploadAsync(localFilePath, overwrite: true);
        Console.WriteLine("File uploaded to Blob Storage.");

        await blobClient.DownloadToAsync(downloadFilePath);
        Console.WriteLine("File downloaded from Blob Storage.");

        await blobClient.DeleteAsync();
        Console.WriteLine("File deleted from Blob Storage.");
    }

    static async Task WorkWithQueueStorage(string connectionString)
    {
        Console.WriteLine("\n=== Queue Storage ===");

        string queueName = "myqueue";

        var queueClient = new QueueClient(connectionString, queueName);
        await queueClient.CreateIfNotExistsAsync();

        await queueClient.SendMessageAsync("Hello from Azure Queue!");
        Console.WriteLine("Message sent to the queue.");

        var receivedMessage = await queueClient.ReceiveMessageAsync();
        if (receivedMessage.Value != null)
        {
            Console.WriteLine($"Received message: {receivedMessage.Value.MessageText}");
            await queueClient.DeleteMessageAsync(receivedMessage.Value.MessageId, receivedMessage.Value.PopReceipt);
            Console.WriteLine("Message deleted from the queue.");
        }
    }

    static async Task WorkWithTableStorage(string connectionString)
    {
        Console.WriteLine("\n=== Table Storage ===");

        string tableName = "Students";
        var tableClient = new TableClient(connectionString, tableName);
        await tableClient.CreateIfNotExistsAsync();
        
        var student = new StudentEntity
        {
            RowKey = Guid.NewGuid().ToString(),
            Name = "Ivan Ivanov",
            Grade = 90
        };

        await tableClient.AddEntityAsync(student);
        Console.WriteLine("Student added to the table.");
        
        await foreach (var s in tableClient.QueryAsync<StudentEntity>())
        {
            Console.WriteLine($"{s.Name} — {s.Grade}");
        }
    }
}
