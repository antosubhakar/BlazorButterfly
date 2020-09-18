using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.View
{
    public class ViewComponentBase<T> : ComponentBase, IView where T : class, IViewModel
    {
        [Inject]
        IJSRuntime Runtime { get; set; }
        private static IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public T ViewModel { get; set;}
        
        protected override void OnInitialized()
        {
            JSRuntime = Runtime;
            Debug.WriteLine("ViewComponentBase.OnInit");
            if (ViewModel != null)
            {
                Debug.WriteLine($"Initializing {ViewModel.GetType().Name}");
                ViewModel.Initialize(SetBusy);
                Debug.WriteLine($"Initialization complete for {ViewModel.GetType().Name}");
            }
            else
                Debug.WriteLine("OnInit, no ViewModel");
        }

        public  async Task SetBusy(bool busy)
        {
            await WaitManager.SetBusy(busy, JSRuntime);
        }
        protected async override Task OnInitializedAsync()
        {
            JSRuntime = Runtime;

            Debug.WriteLine("ViewComponentBase.OnInitAsync");
            if (ViewModel != null)
            {
                Debug.WriteLine($"Initializing {ViewModel.GetType().Name} async");
                await ViewModel.InitializeAsync(SetBusy);
                Debug.WriteLine($"Async initialization complete for {ViewModel.GetType().Name}");
            }
            else
                Debug.WriteLine("OnInitAsync, no ViewModel");
        }
    }
}
