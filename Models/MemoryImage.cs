public class MemoryImage
{
public int Id { get; set; }
public string FileName { get; set; }
public string BlobUrl { get; set; } // if using Blob Storage
public string ContentType { get; set; }
public long Size { get; set; }
public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
public int MemoryFolderId { get; set; }
public MemoryFolder MemoryFolder { get; set; }
}