using PhotoGallery.Data;
using PhotoGallery.Models;

namespace PhotoGallery.Services;

public sealed class ImageService : IImageService
{
    private readonly IPhotoStore _store;

    public ImageService(IPhotoStore store)
    {
        _store = store;
    }

    public IReadOnlyList<ImageItem> GetAll()
    {
        return _store.GetAll();
    }

    public ImageItem? GetById(int id)
    {
        return _store.GetById(id);
    }

    public ImageItem Add(ImageViewModel input)
    {
        return _store.Add(input.Title, input.Url, input.Description);
    }

    public bool UpdateTitle(int id, string title)
    {
        return _store.UpdateTitle(id, title);
    }

    public bool Delete(int id)
    {
        return _store.Delete(id);
    }
}

