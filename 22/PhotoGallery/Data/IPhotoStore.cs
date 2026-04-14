using PhotoGallery.Models;

namespace PhotoGallery.Data;

public interface IPhotoStore
{
    IReadOnlyList<ImageItem> GetAll();
    ImageItem? GetById(int id);
    ImageItem Add(string title, string path, string description);
    bool UpdateTitle(int id, string title);
    bool Delete(int id);
}

