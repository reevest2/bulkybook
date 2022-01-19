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

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Cover> objCoverList = _unitOfWork.Cover.GetAll();
        return View(objCoverList);
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
    public IActionResult Upsert(Cover obj)
    {
        //if (obj.Name == obj.Name.ToString())
        //{
        //    ModelState.AddModelError("Name", "The display order cannot match the name.");
        //}

        if (ModelState.IsValid)
        {
            _unitOfWork.Cover.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Edited Successfully";
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
    public IActionResult DeletePost(int id)
    {
        var obj = _unitOfWork.Cover.GetFirstorDefault(u => u.Id == id);
        //if (obj.Name == obj.Name.ToString())
        //{
        //    ModelState.AddModelError("Name", "The display order cannot match the name.");
        //}

        if (ModelState.IsValid)
        {
            _unitOfWork.Cover.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
}


