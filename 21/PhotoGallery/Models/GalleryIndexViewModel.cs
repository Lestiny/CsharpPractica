namespace PhotoGallery.Models;

public class GalleryIndexViewModel
{
    public IReadOnlyList<Photo> Photos { get; init; } = Array.Empty<Photo>();
    public AddPhotoInput Input { get; init; } = new();
}

