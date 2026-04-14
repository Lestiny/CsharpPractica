namespace PhotoGallery.Models;

public class GalleryIndexViewModel
{
    public IReadOnlyList<ImageItem> Photos { get; init; } = Array.Empty<ImageItem>();
    public AddPhotoInput Input { get; init; } = new();
}

