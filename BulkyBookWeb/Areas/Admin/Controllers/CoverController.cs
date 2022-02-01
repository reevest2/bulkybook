using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CoverController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Cover> objCoverList = _unitOfWork.Cover.GetAll();
            return View(objCoverList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cover obj)
        {
            //if (obj.Name == obj.Name.ToString())
            //{
            //    ModelState.AddModelError("Name", "The display order cannot match the name.");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Cover.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
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
        public IActionResult Edit(Cover obj)
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


