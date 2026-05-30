using System.ComponentModel.DataAnnotations;

namespace TranMinhKhang_Buoi3.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
        public string Name { get; set; }

        [Range(1000, 10000000000, ErrorMessage = "Giá sản phẩm phải nằm trong khoảng từ 1.000đ đến 10.000.000.000đ")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public List<ProductImage>? Images { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
