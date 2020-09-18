using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace JAICT.Blazor.Recipes.Application
{
    public interface IStateStorage
    {
        Task Store<T>(T state, string key, IJSRuntime jsRuntime) where T : class;

        Task<T> Get<T>(string key, IJSRuntime jsRuntime) where T : class;
    }
}
