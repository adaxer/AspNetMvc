using Southwind.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
