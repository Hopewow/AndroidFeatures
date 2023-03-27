using AndroidFeatures.ViewModel;

namespace AndroidFeatures;

public partial class MainPage : ContentPage
{

	public MainPage(Navigation vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}

