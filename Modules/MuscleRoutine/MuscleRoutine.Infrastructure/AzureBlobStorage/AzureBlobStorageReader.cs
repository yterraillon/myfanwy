using System.Text.Json;
using Application.ObjectStorage;
using Azure.Storage.Blobs;
using Infrastructure.JsonSerialization;
using MuscleRoutine.Application.Models;

namespace MuscleRoutine.Infrastructure.AzureBlobStorage;

public class AzureBlobStorageReader: IObjectStorageReader<List<Exercice>>
{
    private readonly BlobContainerClient _blobContainerClient;
    private const string BlobContainerId = "muscle-routine-8ad57b3a-fa71-47ab-ae76-58abc8c8797a";

    public AzureBlobStorageReader(Settings settings)
    {
        var connectionString = settings.MuscleRoutineStorageAccountConnectionString;
        var blobServiceClient = new BlobServiceClient(connectionString);
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(BlobContainerId);
    }

    public async Task<List<Exercice>> GetObjectContentAsync(string objectName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(objectName);
        using var memoryStream = new MemoryStream();
        await blobClient.DownloadToAsync(memoryStream);
        memoryStream.Position = 0; // Reset the stream position to the beginning
        
        var content = await JsonSerializer.DeserializeAsync<List<Exercice>>(memoryStream, JsonSerializerOptionsFactory.CreateCamelCaseOptions());
        return content ?? [];
    }
}