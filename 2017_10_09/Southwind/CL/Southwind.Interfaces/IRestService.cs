using System.Threading.Tasks;

namespace Southwind.Contracts.Interfaces
{
    public interface IRestService
    {
        Task<T> GetDataAsync<T>(string url);

        Task PostDataAsync<T>(T value, string url);
    }
}
