using Southwind.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.Contracts.Interfaces
{
    public interface IShopService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        //IEnumerable<Category> GetCategories();

        Task SaveCategoryAsync(Category cat);
        //void SaveCategory(Category cat);

        Task DeleteCategoryAsync(Category cat);
    }
}
