[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
private readonly MemoriesDbContext _db;
private readonly IStorageService _storage;


public ImagesController(MemoriesDbContext db, IStorageService storage)
{
_db = db; _storage = storage;
}


[HttpPost("{folderId}")]
public async Task<IActionResult> Upload(int folderId, IFormFile file)
{
var folder = await _db.MemoryFolders.FindAsync(folderId);
if(folder == null) return NotFound("Folder not found");


var uniqueName = $"{Guid.NewGuid()}_{file.FileName}";
using var stream = file.OpenReadStream();
var url = await _storage.UploadFileAsync(stream, uniqueName, file.ContentType);


var img = new MemoryImage { FileName = file.FileName, BlobUrl = url, ContentType = file.ContentType, Size = file.Length, MemoryFolderId = folderId };
_db.MemoryImages.Add(img);
await _db.SaveChangesAsync();


return CreatedAtAction(nameof(Get), new { id = img.Id }, img);
}


[HttpGet("{id}")]
public async Task<IActionResult> Get(int id)
{
var img = await _db.MemoryImages.FindAsync(id);
if(img == null) return NotFound();
return Redirect(img.BlobUrl); // or stream through API
}


[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
var img = await _db.MemoryImages.FindAsync(id);
if(img == null) return NotFound();
await _storage.DeleteFileAsync(img.BlobUrl);
_db.MemoryImages.Remove(img);
await _db.SaveChangesAsync();
return NoContent();
}
}