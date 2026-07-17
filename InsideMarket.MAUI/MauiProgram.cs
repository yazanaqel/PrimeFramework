using InsideMarket.MAUI.Auth;
using InsideMarket.MAUI.Business.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace InsideMarket.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.Services.AddMudServices();

        builder.Services.AddScoped<ITokenStore,PreferencesTokenStore>();
        builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthStateProvider>();
        builder.Services.AddScoped<IAuthService,AuthService>();

        builder.Services.AddScoped<IStoreService,StoreService>();
        builder.Services.AddScoped<ICategoryService,CategoryService>();
        builder.Services.AddScoped<IBasketService,BasketService>();
        builder.Services.AddScoped<IProductService,ProductService>();

        builder.Services.AddAuthorizationCore();

        builder.Services.AddScoped<TokenMessageHandler>();

        builder.Services.AddHttpClient("Write",client =>
        {
            client.BaseAddress = new Uri("https://localhost:7104/");
        })
        .AddHttpMessageHandler<TokenMessageHandler>();

        builder.Services.AddHttpClient("Read",client =>
        {
            client.BaseAddress = new Uri("https://localhost:7125/");
        })
.AddHttpMessageHandler<TokenMessageHandler>();

        // Default HttpClient (optional but safe)
        builder.Services.AddHttpClient();



        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf","OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
