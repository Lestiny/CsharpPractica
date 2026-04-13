using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Models;
using PhotoGallery.Services;

namespace PhotoGallery.Controllers;

public class ImageController : Controller
{
    private readonly IImageService _images;

    public ImageController(IImageService images)
    {
        _images = images;
    }

    public IActionResult Index()
    {
        ViewBag.Images = _images.GetAll();
        return View(new ImageViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(ImageViewModel input)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Images = _images.GetAll();
            return View("Index", input);
        }

        _images.Add(input);
        TempData["Notice"] = "Изображение добавлено";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("/Image/Show/{id:int}")]
    public IActionResult Show(int id)
    {
        var img = _images.GetById(id);
        if (img is null)
        {
            return NotFound();
        }

        return View(img);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _images.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}

