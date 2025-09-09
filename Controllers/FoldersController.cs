[ApiController]
[Route("api/[controller]")]
public class FoldersController : ControllerBase
{
private readonly MemoriesDbContext _db;
public FoldersController(MemoriesDbContext db) { _db = db; }


[HttpPost]
public async Task<IActionResult> Create([FromBody] MemoryFolder folder)
{
_db.MemoryFolders.Add(folder);
await _db.SaveChangesAsync();
return CreatedAtAction(nameof(Get), new { id = folder.Id }, folder);
}


[HttpGet]
public async Task<IActionResult> List() => Ok(await _db.MemoryFolders.Include(f => f.Images).ToListAsync());


[HttpGet("{id}")]
public async Task<IActionResult> Get(int id) => Ok(await _db.MemoryFolders.Include(f => f.Images).FirstOrDefaultAsync(f => f.Id == id));


[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
var folder = await _db.MemoryFolders.Include(f => f.Images).FirstOrDefaultAsync(f => f.Id == id);
if(folder == null) return NotFound();
// optionally delete images from blob storage here
_db.MemoryFolders.Remove(folder);
await _db.SaveChangesAsync();
return NoContent();
}
}