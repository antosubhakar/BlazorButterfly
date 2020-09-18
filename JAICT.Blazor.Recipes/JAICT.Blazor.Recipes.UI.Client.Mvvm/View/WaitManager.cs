using Microsoft.JSInterop;
using System.Threading.Tasks;
using Microsoft.JSInterop.WebAssembly;
namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.View
{
    public static class WaitManager
    {
        public static async Task SetBusy(bool busy, IJSRuntime jsRuntime)
        {
            if (busy)
                await StartWait(jsRuntime);
            else
                await EndWait(jsRuntime);
        }
        public static async Task StartWait(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("startwait"); // "Added as JSRuntime.Current Depreciated"
        }

        public static async Task EndWait(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<bool>("endwait");
        }
    }
}
