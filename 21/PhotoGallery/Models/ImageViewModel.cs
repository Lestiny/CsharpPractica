using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Models;

public class ImageViewModel
{
    [Required(ErrorMessage = "Введите название.")]
    [StringLength(80, ErrorMessage = "Слишком длинное название (макс. 80).")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Введите URL изображения.")]
    [Url(ErrorMessage = "Введите корректный URL (например, https://...).")]
    [StringLength(2048, ErrorMessage = "Слишком длинный URL.")]
    public string Url { get; set; } = "";

    [StringLength(500, ErrorMessage = "Слишком длинное описание (макс. 500).")]
    public string Description { get; set; } = "";
}

