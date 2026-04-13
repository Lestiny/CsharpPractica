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

    public IReadOnlyList<Photo> GetAll()
    {
        return _store.GetAll();
    }

    public Photo? GetById(int id)
    {
        return _store.GetById(id);
    }

    public Photo Add(ImageViewModel input)
    {
        return _store.Add(input.Title, input.Url, input.Description);
    }

    public bool Delete(int id)
    {
        return _store.Delete(id);
    }
}

