using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Data;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers;

public class GalleryController : Controller
{
    private readonly IPhotoStore _store;

    public GalleryController(IPhotoStore store)
    {
        _store = store;
    }

    public IActionResult Index()
    {
        return View(new GalleryIndexViewModel
        {
            Photos = _store.GetAll()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(AddPhotoInput input)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", new GalleryIndexViewModel
            {
                Photos = _store.GetAll(),
                Input = input
            });
        }

        _store.Add(input.Title, input.Url);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("/Gallery/Show/{id:int}")]
    public IActionResult Show(int id)
    {
        var photo = _store.GetById(id);

        if (photo is null)
        {
            return NotFound();
        }

        return View(photo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _store.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}

