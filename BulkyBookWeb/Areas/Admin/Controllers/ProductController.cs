using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {
        ProductViewModel productViewModel = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverList = _unitOfWork.Cover.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };
        if (id == null || id == 0)
        {
            return View(productViewModel);
        }

        return View(productViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductViewModel obj, IFormFile file)
    {
        //if (obj.Name == obj.Name.ToString())
        //{
        //    ModelState.AddModelError("Name", "The display order cannot match the name.");
        //}

        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    fileStream.CopyTo(fileStream);
                }
                obj.Product.ImageUrl = @"\images\products" + fileName + extension;
            }

            _unitOfWork.Product.Add(obj.Product);
            _unitOfWork.Save();
            TempData["success"] = "Product created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var coverFromDb = _unitOfWork.Cover.GetFirstorDefault(u => u.Id == id);

        if (coverFromDb == null)
        {
            return NotFound();
        }
        return View(coverFromDb);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.Cover.GetFirstorDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.Cover.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _unitOfWork.Product.GetAll();
        return Json(new { data = productList });
    }

    #endregion


}




