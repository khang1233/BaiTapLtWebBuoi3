using System.Collections.Generic;
using System.Threading.Tasks;
using TranMinhKhang_Buoi3.Models;

namespace TranMinhKhang_Buoi3.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
