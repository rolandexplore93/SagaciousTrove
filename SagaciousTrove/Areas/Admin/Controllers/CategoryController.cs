using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace SagaciousTrove.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //var objCategorylist = _db.Categories.ToList();
            IEnumerable<Category> objCategorylist = _unitOfWork.Category.GetAll();
            return View(objCategorylist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var category = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                NotFound();
            }

            return View(categoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            var existingCategory = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == obj.Id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = obj.Name;
            existingCategory.DisplayOrder = obj.DisplayOrder;
            existingCategory.ModifiedDateTime = DateTime.Now;
            _unitOfWork.Category.Update(existingCategory);
            _unitOfWork.Save();
            TempData["Success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var category = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                NotFound();
            }

            return View(categoryFromDbFirst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            //var obj = _db.Categories.Find(id);
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return BadRequest();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}


