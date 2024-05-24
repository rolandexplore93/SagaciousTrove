using Microsoft.AspNetCore.Mvc;
using SagaciousTrove.Data;
using SagaciousTrove.Models;

namespace SagaciousTrove.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var objCategorylist = _db.Categories.ToList();
            IEnumerable<Category> objCategorylist = _db.Categories;
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

            _db.Categories.Add(obj);
            _db.SaveChanges();
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

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            _db.Categories.Update(obj);
            _db.SaveChanges();
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

            var category = _db.Categories.Find(id);

            if (category == null)
            {
                NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return BadRequest();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }
}


