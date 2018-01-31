using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.Interfaces
{
    public interface IRestService
    {
        Task<T> GetDataAsync<T>(string url);

        Task PostDataAsync<T>(T value, string url);
    }
}
