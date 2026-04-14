using Microsoft.EntityFrameworkCore;
using PhotoGallery.Models;

namespace PhotoGallery.Data;

public sealed class GalleryDbContext : DbContext
{
    public GalleryDbContext(DbContextOptions<GalleryDbContext> options)
        : base(options)
    {
    }

    public DbSet<ImageItem> ImageItems => Set<ImageItem>();
}
