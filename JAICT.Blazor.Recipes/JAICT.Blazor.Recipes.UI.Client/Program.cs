using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using JAICT.Blazor.Recipes.Application;
using JAICT.Blazor.Recipes.UI.Client.Mvvm;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.View;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
//using JAICT.Blazor.Recipes.UI.Client.Views;

namespace JAICT.Blazor.Recipes.UI.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<Main>("app");
            builder.Services.AddSingleton<ISessionStateManager, SessionStateManager>();
            builder.Services.AddSingleton<ICommandFactory, CommandFactory>();
            builder.Services.AddSingleton<IViewModelRepository, ViewModelRepository>();
            builder.Services.AddSingleton<IViewRepository, ViewRepository>();
            builder.Services.AddSingleton<IViewTypeSelector, ViewTypeSelector>();

            builder.Services.AddTransient<IStateStorage, CookieStorage>();

            // Force viewmodel to load assembly to search
            var vmAssembly = "JAICT.Blazor.Recipes.UI.Client.ViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            foreach (var viewModelType in ViewModelRepository.GetViewModelTypes(new[] { vmAssembly }))
            {
                builder.Services.AddTransient(viewModelType);
            }
            await builder.Build().RunAsync();
        }
        /*
        public static WebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            WebAssemblyHostBuilder.CreateDefault()
                
            .UseStartup<Startup>();*/
    }
}
