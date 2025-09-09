var builder = WebApplication.CreateBuilder(args);


// EF Core
builder.Services.AddDbContext<MemoriesDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Storage service - using connection string from config
var blobConn = builder.Configuration["Blob:ConnectionString"];
var container = builder.Configuration["Blob:Container"] ?? "images";
builder.Services.AddSingleton<IStorageService>(_ => new BlobStorageService(blobConn, container));


builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();