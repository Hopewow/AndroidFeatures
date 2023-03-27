using ZXing;

namespace AndroidFeatures;

public partial class ScannerPage : ContentPage
{
    public ScannerPage()
    {
        InitializeComponent();
        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions()
        {
            AutoRotate = true,
            TryHarder = true,
            Multiple = false
        };
    }

    private async void BarcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        
        if (!e.Results[0].Value.StartsWith("https://"))
        {
            Dispatcher.Dispatch(() => {
                Label newLabel = new();

                newLabel.Parent = Page;
                newLabel.Text = e.Results[0].Value;

                Page.Add(newLabel);
            });
        } else {
            try
            {
                Uri uri = new Uri(e.Results[0].Value);
                BrowserLaunchOptions options = new BrowserLaunchOptions()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                };

                await Browser.Default.OpenAsync(uri, options);
            }
            catch (Exception ex)
            {
                // An unexpected error occurred. No browser may be installed on the device.
            }
        }
    }
}
