using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Diagnostics;
using System.Linq;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.View
{
    public class DynamicComponentCollection : ComponentBase
    {
        private bool _isInitialized = false;

        [Inject]
        private IViewModelRepository ViewModelRepository { get; set; }

        [Inject]
        [Parameter]
        public IViewTypeSelector ViewModelTypeSelector { get; set; }

        [Parameter]
        public IViewModelCollectionManager ViewModelCollectionManager { get; set; }

        private void ViewModelManager_ModelUpdated(object sender, EventArgs e)
        {
            var newModel = ViewModelCollectionManager.Current;
            Debug.WriteLine($"DynamicComponentCollection.ViewModelManager_ModelUpdated");

            if (_isInitialized)
            {
                StateHasChanged();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            foreach(var viewModel in ViewModelCollectionManager.Current)
            {
                var viewType = ViewModelTypeSelector.SelectViewType(viewModel.GetType().Name); // ViewModelRepository.FindViewType(viewModel.GetType().Name);

                builder.OpenComponent(seq, viewType);
                builder.AddAttribute(++seq, "ViewModel", viewModel);
                builder.CloseComponent();

                Debug.WriteLine($"Created component with viewModel '{viewModel.GetType().Name}' and view '{viewType.Name}'");

                seq++;
            }

            base.BuildRenderTree(builder);
        }

        protected override void OnInitialized()
        {
            if (ViewModelCollectionManager == null || ViewModelCollectionManager.Current == null)
            {
                ViewModelCollectionManager = new ViewModelCollectionManager(Enumerable.Empty<IViewModel>());
            }

            _isInitialized = true;
        }
    }
}
