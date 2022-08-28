using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProductCrud.Data;
using ProductCrud.Models;

namespace ProductCrud.Controllers;

public class MobileController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly ApplicationDbContext _db;

    public MobileController(IWebHostEnvironment environment, ApplicationDbContext db)
    {
        _db = db;
        _environment = environment;
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var obj = _db.Mobiles.Find(id);
        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public IActionResult DeletePost(Mobile mobile)
    {
        if (mobile.MobilePhotoImage != null)
        {
            string imagePath = Path.Combine(_environment.WebRootPath, "images") + "/" + mobile.MobilePhotoImage;
            System.IO.File.Delete(imagePath);
        }

        _db.Mobiles.Remove(mobile);
        _db.SaveChanges();
        return Redirect("/Home");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (ModelState.IsValid)
        {
            var mobile = _db.Mobiles.SingleOrDefault(m => m.Id == id);
            var obj = new MobileViewModel()
            {
                MobileName = mobile.MobileName,
                Price = mobile.Price
            };
            return View(obj);
        }

        return Redirect("/Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(MobileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var mobile = _db.Mobiles.Find(model.Id);
            mobile.MobileName = model.MobileName;
            mobile.Price = model.Price;

            if (model.MobileImage != null)
            {
                mobile.MobilePhotoImage = FileUpload(model);
            }

            _db.Mobiles.Update(mobile);
            _db.SaveChanges();
            return Redirect("/Home");
        }

        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MobileViewModel model)
    {
        if (ModelState.IsValid)
        {
            string? uniqueFileName = FileUpload(model);
            var mobile = new Mobile()
            {
                MobileName = model.MobileName,
                Price = model.Price,
                MobilePhotoImage = uniqueFileName
            };
            _db.Add(mobile);
            _db.SaveChanges();
            return Redirect("/Home");
        }

        return View();
    }

    private string? FileUpload(MobileViewModel model)
    {
        string? uniqueFileName = null;
        if (model.MobileImage != null)
        {
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.MobileImage.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.MobileImage.CopyTo(fileStream);
            }
        }

        return uniqueFileName;
    }
}