namespace PhotoGallery.Models;

public class Photo
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime DateUploaded { get; set; }
}

