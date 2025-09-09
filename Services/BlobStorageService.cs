using Azure.Storage.Blobs;


public class BlobStorageService : IStorageService
{
private readonly BlobContainerClient _container;
public BlobStorageService(string connectionString, string containerName)
{
var client = new BlobServiceClient(connectionString);
_container = client.GetBlobContainerClient(containerName);
_container.CreateIfNotExists();
}


public async Task<string> UploadFileAsync(Stream stream, string fileName, string contentType)
{
var blobClient = _container.GetBlobClient(fileName);
await blobClient.UploadAsync(stream, overwrite: true);
await blobClient.SetHttpHeadersAsync(new Azure.Storage.Blobs.Models.BlobHttpHeaders { ContentType = contentType });
return blobClient.Uri.ToString();
}


public async Task DeleteFileAsync(string blobUrl)
{
var blobName = new Uri(blobUrl).Segments.Last();
var blobClient = _container.GetBlobClient(blobName);
await blobClient.DeleteIfExistsAsync();
}
}