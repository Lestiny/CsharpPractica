using PhotoGallery.Models;

namespace PhotoGallery.Data;

public interface IPhotoStore
{
    IReadOnlyList<Photo> GetAll();
    Photo? GetById(int id);
    Photo Add(string title, string url);
    bool Delete(int id);
}

