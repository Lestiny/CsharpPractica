using Microsoft.EntityFrameworkCore;
using PhotoGallery.Models;

namespace PhotoGallery.Data;

public sealed class EfPhotoStore : IPhotoStore
{
    private readonly GalleryDbContext _db;

    public EfPhotoStore(GalleryDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<ImageItem> GetAll()
    {
        return _db.ImageItems
            .OrderByDescending(x => x.UploadedAt)
            .ToList();
    }

    public ImageItem? GetById(int id)
    {
        return _db.ImageItems.FirstOrDefault(x => x.Id == id);
    }

    public ImageItem Add(string title, string path, string description)
    {
        var item = new ImageItem
        {
            Title = title.Trim(),
            Path = path,
            Description = description.Trim(),
            UploadedAt = DateTime.UtcNow
        };

        _db.ImageItems.Add(item);
        _db.SaveChanges();
        return item;
    }

    public bool UpdateTitle(int id, string title)
    {
        var item = _db.ImageItems.FirstOrDefault(x => x.Id == id);
        if (item is null)
        {
            return false;
        }

        item.Title = title.Trim();
        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var item = _db.ImageItems.FirstOrDefault(x => x.Id == id);
        if (item is null)
        {
            return false;
        }

        _db.ImageItems.Remove(item);
        _db.SaveChanges();
        return true;
    }
}
