namespace PhotoGallery.Models;

public class ImageItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Path { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime UploadedAt { get; set; }
}
