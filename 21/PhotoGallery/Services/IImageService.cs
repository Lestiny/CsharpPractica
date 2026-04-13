using PhotoGallery.Models;

namespace PhotoGallery.Services;

public interface IImageService
{
    IReadOnlyList<Photo> GetAll();
    Photo? GetById(int id);
    Photo Add(ImageViewModel input);
    bool Delete(int id);
}

