using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TranMinhKhang_Buoi3.Models;
using TranMinhKhang_Buoi3.Repositories;

namespace TranMinhKhang_Buoi3.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // 1. Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        // 2. Hiển thị form thêm danh mục mới
        public IActionResult Add()
        {
            return View();
        }

        // 3. Xử lý thêm danh mục mới
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            ModelState.Remove("Products");

            if (ModelState.IsValid)
            {
                await _categoryRepository.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // 4. Hiển thị form cập nhật danh mục
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // 5. Xử lý cập nhật danh mục
        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            ModelState.Remove("Products");

            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // 6. Hiển thị trang xác nhận xoá danh mục
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // 7. Xử lý xoá danh mục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
