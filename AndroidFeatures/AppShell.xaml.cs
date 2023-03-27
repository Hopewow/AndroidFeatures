namespace AndroidFeatures;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ScannerPage), typeof(ScannerPage));
        Routing.RegisterRoute(nameof(CurrentLocationPage), typeof(CurrentLocationPage));
        Routing.RegisterRoute(nameof(CameraPage), typeof(CameraPage));
    }
}
