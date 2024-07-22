﻿using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;

namespace SagaciousTrove.CoverTypeController
{
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

        //public IActionResult Index()
        //{
        //    IEnumerable<CoverType> objCoverTypelist = _unitOfWork.CoverType.GetAll();
        //    return View(objCoverTypelist);
        //}

        public IActionResult Index()
        {
            IEnumerable<Product> objProductTypelist = _unitOfWork.Product.GetAll();
            return View(objProductTypelist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                        u => new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.Id.ToString()
                        }
                ),

                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                )
            };

            if (id == null || id == 0)
            {
                // Create new product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            else
            {
                // Update existing product
            }

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images/products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"/images/products/" + fileName + extension;
                }
                //return View(obj);
            }

            _unitOfWork.Product.Add(obj.Product);
            _unitOfWork.Save();
            TempData["Success"] = "Product created successfully";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (CoverTypeFromDbFirst == null)
            {
                NotFound();
            }

            return View(CoverTypeFromDbFirst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return BadRequest();
            }

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}


