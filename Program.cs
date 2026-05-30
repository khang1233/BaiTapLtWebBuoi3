using Microsoft.EntityFrameworkCore;
using TranMinhKhang_Buoi3.Models;
using TranMinhKhang_Buoi3.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Tự động Seed dữ liệu nếu DB trống
SeedDatabase(app);

app.Run();

// Hàm Seed dữ liệu 10 danh mục và 20 sản phẩm chất lượng cao
void SeedDatabase(WebApplication webApp)
{
    using (var scope = webApp.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Đảm bảo database đã được tạo và apply migrations
        context.Database.Migrate();

        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Điện thoại thông minh" },
                new Category { Name = "Laptop & Máy tính" },
                new Category { Name = "Phụ kiện công nghệ" },
                new Category { Name = "Thiết bị âm thanh" },
                new Category { Name = "Đồng hồ thông minh" },
                new Category { Name = "Thiết bị gia dụng" },
                new Category { Name = "Giày thời trang" },
                new Category { Name = "Balo & Túi xách" },
                new Category { Name = "Dụng cụ thể thao" },
                new Category { Name = "Sách & Quà tặng" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var products = new List<Product>
            {
                // 1. Điện thoại thông minh
                new Product 
                { 
                    Name = "iPhone 15 Pro Max 256GB", 
                    Price = 29990000, 
                    Description = "Siêu phẩm cao cấp của Apple với khung Titanium siêu bền, chip A17 Pro tối tân và hệ thống camera zoom quang học 5x đẳng cấp.", 
                    ImageUrl = "https://images.unsplash.com/photo-1695048133142-1a20484d2569?w=500", 
                    CategoryId = categories[0].Id 
                },
                new Product 
                { 
                    Name = "Samsung Galaxy S24 Ultra AI", 
                    Price = 26990000, 
                    Description = "Đỉnh cao công nghệ AI di động của Samsung, camera 200MP siêu nét, hỗ trợ bút S-Pen thông minh.", 
                    ImageUrl = "https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?w=500", 
                    CategoryId = categories[0].Id 
                },

                // 2. Laptop & Máy tính
                new Product 
                { 
                    Name = "MacBook Pro M3 Max 14\"", 
                    Price = 59990000, 
                    Description = "Sức mạnh tối thượng cho lập trình viên và nhà thiết kế đồ họa chuyên nghiệp, màn hình Liquid Retina XDR sắc nét.", 
                    ImageUrl = "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=500", 
                    CategoryId = categories[1].Id 
                },
                new Product 
                { 
                    Name = "Dell XPS 13 Plus 9320", 
                    Price = 38500000, 
                    Description = "Thiết kế tương lai siêu mỏng nhẹ, bàn phím tràn viền cảm ứng lực vô cùng đẳng cấp và sang trọng.", 
                    ImageUrl = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=500", 
                    CategoryId = categories[1].Id 
                },

                // 3. Phụ kiện công nghệ
                new Product 
                { 
                    Name = "Bàn phím cơ Keychron Q1 Pro", 
                    Price = 3850000, 
                    Description = "Bàn phím cơ Custom không dây cao cấp, vỏ nhôm nguyên khối đầm tay, hỗ trợ hot-swap switch mượt mà.", 
                    ImageUrl = "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=500", 
                    CategoryId = categories[2].Id 
                },
                new Product 
                { 
                    Name = "Chuột Logitech MX Master 3S", 
                    Price = 2450000, 
                    Description = "Chuột công thái học tốt nhất thế giới dành cho lập trình viên và nhà thiết kế, cuộn MagSpeed siêu nhanh.", 
                    ImageUrl = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=500", 
                    CategoryId = categories[2].Id 
                },

                // 4. Thiết bị âm thanh
                new Product 
                { 
                    Name = "Tai nghe Sony WH-1000XM5", 
                    Price = 6850000, 
                    Description = "Tai nghe chụp tai chống ồn đỉnh cao thế giới, thời lượng pin 30 giờ, âm thanh độ phân giải cao Hi-Res Audio.", 
                    ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500", 
                    CategoryId = categories[3].Id 
                },
                new Product 
                { 
                    Name = "Loa Bluetooth Marshall Acton III", 
                    Price = 5490000, 
                    Description = "Phong cách cổ điển đặc trưng Marshall, âm thanh cơ học chi tiết ấm áp, trang trí phòng cực sang.", 
                    ImageUrl = "https://images.unsplash.com/photo-1545454675-3531b543be5d?w=500", 
                    CategoryId = categories[3].Id 
                },

                // 5. Đồng hồ thông minh
                new Product 
                { 
                    Name = "Apple Watch Ultra 2 Titanium", 
                    Price = 21490000, 
                    Description = "Đồng hồ thể thao chuyên nghiệp với vỏ Titanium siêu bền bỉ, GPS tần số kép độ chính xác cực cao.", 
                    ImageUrl = "https://images.unsplash.com/photo-1508685096489-7aacd43bd3b1?w=500", 
                    CategoryId = categories[4].Id 
                },
                new Product 
                { 
                    Name = "Garmin Fenix 7 Pro Solar", 
                    Price = 18990000, 
                    Description = "Đồng hồ thám hiểm đỉnh cao tích hợp sạc năng lượng mặt trời, bản đồ chi tiết trực quan chuyên phượt dã ngoại.", 
                    ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500", 
                    CategoryId = categories[4].Id 
                },

                // 6. Thiết bị gia dụng
                new Product 
                { 
                    Name = "Nồi chiên không dầu Philips HD9270", 
                    Price = 2850000, 
                    Description = "Công nghệ Rapid Air độc quyền giảm 90% lượng mỡ thừa có hại, dung tích XXL cho cả gia đình.", 
                    ImageUrl = "https://images.unsplash.com/photo-1621972750749-0fbb1abb7736?w=500", 
                    CategoryId = categories[5].Id 
                },
                new Product 
                { 
                    Name = "Robot hút bụi Ecovacs Deebot T20", 
                    Price = 13490000, 
                    Description = "Robot hút bụi lau nhà thông minh vượt trội, tự động giặt giẻ lau bằng nước nóng 55 độ C tiện lợi vô cùng.", 
                    ImageUrl = "https://images.unsplash.com/photo-1518310383802-640c2de311b2?w=500", 
                    CategoryId = categories[5].Id 
                },

                // 7. Giày thời trang
                new Product 
                { 
                    Name = "Giày Nike Air Force 1 '07 White", 
                    Price = 2850000, 
                    Description = "Đôi giày huyền thoại mang tính biểu tượng thời trang đường phố toàn cầu, dễ phối đồ cá tính.", 
                    ImageUrl = "https://images.unsplash.com/photo-1549298916-b41d501d3772?w=500", 
                    CategoryId = categories[6].Id 
                },
                new Product 
                { 
                    Name = "Giày Adidas Ultraboost Light", 
                    Price = 3890000, 
                    Description = "Giày chạy bộ êm ái nhất lịch sử Adidas, đệm hạt Boost siêu nhẹ phản hồi năng lượng tối đa.", 
                    ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=500", 
                    CategoryId = categories[6].Id 
                },

                // 8. Balo & Túi xách
                new Product 
                { 
                    Name = "Balo Peak Design Everyday 20L", 
                    Price = 6490000, 
                    Description = "Balo máy ảnh và công nghệ đỉnh cao với thiết kế tháo mở đa hướng, chất liệu chống nước tuyệt hảo.", 
                    ImageUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=500", 
                    CategoryId = categories[7].Id 
                },
                new Product 
                { 
                    Name = "Túi đeo chéo Herschel Heritage", 
                    Price = 1250000, 
                    Description = "Phong cách trẻ trung cổ điển năng động, chất liệu vải dệt siêu bền bỉ kháng nước nhẹ.", 
                    ImageUrl = "https://images.unsplash.com/photo-1622560480605-d83c853bc5c3?w=500", 
                    CategoryId = categories[7].Id 
                },

                // 9. Dụng cụ thể thao
                new Product 
                { 
                    Name = "Thảm tập Yoga Adidas Premium 8mm", 
                    Price = 850000, 
                    Description = "Độ bám dính cao chống trơn trượt tuyệt đối, đệm êm giảm chấn bảo vệ an toàn khớp xương tập luyện.", 
                    ImageUrl = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=500", 
                    CategoryId = categories[8].Id 
                },
                new Product 
                { 
                    Name = "Vợt cầu lông Yonex Astrox 100ZZ", 
                    Price = 4150000, 
                    Description = "Vợt cầu lông thiên công mạnh mẽ hàng đầu thế giới được các tuyển thủ chuyên nghiệp tin chọn.", 
                    ImageUrl = "https://images.unsplash.com/photo-1617083266333-5a534ad41f4f?w=500", 
                    CategoryId = categories[8].Id 
                },

                // 10. Sách & Quà tặng
                new Product 
                { 
                    Name = "Sách Đắc Nhân Tâm (Bản Cao Cấp)", 
                    Price = 150000, 
                    Description = "Cuốn sách nghệ thuật thu phục lòng người và dẫn lối thành công bán chạy nhất mọi thời đại.", 
                    ImageUrl = "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=500", 
                    CategoryId = categories[9].Id 
                },
                new Product 
                { 
                    Name = "Bút ký cao cấp Parker IM Black GT", 
                    Price = 950000, 
                    Description = "Thân hợp kim sơn mài bóng loáng sang trọng, ngòi mạ vàng, món quà tặng doanh nhân vô cùng ý nghĩa.", 
                    ImageUrl = "https://images.unsplash.com/photo-1583485088034-697b5bc54ccd?w=500", 
                    CategoryId = categories[9].Id 
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
