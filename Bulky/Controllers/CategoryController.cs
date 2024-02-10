using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            //custom validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "Display Order should not same as name");
            }

            if (category.Name is not null && category.Name.ToLower().Contains("test"))
            {
                ModelState.AddModelError("", "Category shouldn't have test content in the name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created Successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "Display Order should not same as name");
            }

            if (category.Name is not null && category.Name.ToLower().Contains("test"))
            {
                ModelState.AddModelError("", "Category shouldn't have test content in the name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category updated Successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted Successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
