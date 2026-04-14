using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var appDataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(appDataDir);
var dbPath = Path.Combine(appDataDir, "gallery.db");

builder.Services.AddDbContext<GalleryDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<IPhotoStore, EfPhotoStore>();
builder.Services.AddScoped<PhotoGallery.Services.IImageService, PhotoGallery.Services.ImageService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GalleryDbContext>();
    db.Database.EnsureCreated();
    EnsureDescriptionColumn(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

static void EnsureDescriptionColumn(GalleryDbContext db)
{
    using var connection = db.Database.GetDbConnection();
    if (connection.State != System.Data.ConnectionState.Open)
    {
        connection.Open();
    }

    using var checkCommand = connection.CreateCommand();
    checkCommand.CommandText = "PRAGMA table_info('ImageItems');";

    var hasDescription = false;
    using (var reader = checkCommand.ExecuteReader())
    {
        while (reader.Read())
        {
            if (string.Equals(reader["name"]?.ToString(), "Description", StringComparison.OrdinalIgnoreCase))
            {
                hasDescription = true;
                break;
            }
        }
    }

    if (hasDescription)
    {
        return;
    }

    db.Database.ExecuteSqlRaw("ALTER TABLE ImageItems ADD COLUMN Description TEXT NOT NULL DEFAULT '';");
}
