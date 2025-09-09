public class MemoryFolder
{
public int Id { get; set; }
public string Name { get; set; }
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
public List<MemoryImage> Images { get; set; } = new();
}