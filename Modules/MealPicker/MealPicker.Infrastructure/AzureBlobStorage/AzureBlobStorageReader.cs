using System.Text.Json;
using Application.ObjectStorage;
using Azure.Storage.Blobs;
using Infrastructure.JsonSerialization;
using MealPicker.Application.Models;

namespace MealPicker.Infrastructure.AzureBlobStorage;

public class AzureBlobStorageReader: IObjectStorageReader<List<Meal>>
{
    private readonly BlobContainerClient _blobContainerClient;
    private const string BlobContainerId = "meal-picker-f9d7992b-dd9a-4927-b431-d07434c78061";

    public AzureBlobStorageReader(Settings settings)
    {
        var connectionString = settings.MealPickerStorageAccountConnectionString;
        var blobServiceClient = new BlobServiceClient(connectionString);
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(BlobContainerId);
    }
    
    public async Task<List<Meal>> GetObjectContentAsync(string objectName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(objectName);
        using var memoryStream = new MemoryStream();
        await blobClient.DownloadToAsync(memoryStream);
        memoryStream.Position = 0; // Reset the stream position to the beginning
        
        var content = await JsonSerializer.DeserializeAsync<List<Meal>>(memoryStream, JsonSerializerOptionsFactory.CreateCamelCaseOptions());
        return content ?? [];
    }
}