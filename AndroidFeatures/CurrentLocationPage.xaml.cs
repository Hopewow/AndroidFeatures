using AndroidFeatures.ViewModel;

namespace AndroidFeatures;

public partial class CurrentLocationPage : ContentPage
{
	public CurrentLocationPage(GeoLocationModel vm)
	{
        BindingContext = vm;
		InitializeComponent();
	}
}