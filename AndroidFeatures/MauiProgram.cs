using AndroidFeatures.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace AndroidFeatures;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseBarcodeReader();

		#if DEBUG
			builder.Logging.AddDebug();
		#endif

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<Navigation>();
        builder.Services.AddSingleton<CurrentLocationPage>();
        builder.Services.AddSingleton<ScannerPage>();
		builder.Services.AddSingleton<GeoLocationModel>();
		builder.Services.AddSingleton<CameraPage>();

		return builder.Build();
	}
}
