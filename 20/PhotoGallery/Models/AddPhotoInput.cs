using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Models;

public class AddPhotoInput
{
    [Required(ErrorMessage = "Введите заголовок.")]
    [StringLength(80, ErrorMessage = "Слишком длинный заголовок (макс. 80).")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Введите URL изображения.")]
    [Url(ErrorMessage = "Введите корректный URL (например, https://...).")]
    [StringLength(2048, ErrorMessage = "Слишком длинный URL.")]
    public string Url { get; set; } = "";
}

