public interface IStorageService
{
Task<string> UploadFileAsync(Stream stream, string fileName, string contentType);
Task DeleteFileAsync(string blobUrl);
}