using Microsoft.AspNetCore.Mvc;

namespace PhotoGallery.Controllers;

public class ImageController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Gallery");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add()
    {
        return RedirectToAction("Index", "Gallery");
    }

    [HttpGet("/Image/Show/{id:int}")]
    public IActionResult Show(int id)
    {
        return RedirectToAction("Show", "Gallery", new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        return RedirectToAction("Index", "Gallery");
    }
}

