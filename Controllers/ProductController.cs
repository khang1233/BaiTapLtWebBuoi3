using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TranMinhKhang_Buoi3.Models;
using TranMinhKhang_Buoi3.Repositories;

namespace TranMinhKhang_Buoi3.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // 1. Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        // 2. Hiển thị form thêm sản phẩm mới
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // 3. Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile? imageUrl)
        {
            ModelState.Remove("ImageUrl");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    product.ImageUrl = await SaveImage(imageUrl);
                }
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        // Viết thêm hàm SaveImage (tham khảo bài 02)
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }

        // 4. Hiển thị thông tin chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // 5. Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // 6. Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product, IFormFile? imageUrl)
        {
            ModelState.Remove("ImageUrl");
            ModelState.Remove("Category");

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                if (imageUrl == null)
                {
                    product.ImageUrl = existingProduct.ImageUrl;
                }
                else
                {
                    if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                    {
                        var oldPath = Path.Combine("wwwroot", existingProduct.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    product.ImageUrl = await SaveImage(imageUrl);
                }

                await _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // 7. Hiển thị trang xác nhận xoá sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // 8. Xử lý xoá sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var path = Path.Combine("wwwroot", product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                await _productRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
