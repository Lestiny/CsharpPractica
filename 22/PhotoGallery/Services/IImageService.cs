using PhotoGallery.Models;

namespace PhotoGallery.Services;

public interface IImageService
{
    IReadOnlyList<ImageItem> GetAll();
    ImageItem? GetById(int id);
    ImageItem Add(ImageViewModel input);
    bool UpdateTitle(int id, string title);
    bool Delete(int id);
}

