using AndroidFeatures.Models;
using Microsoft.Maui.Devices.Sensors;
using System.Net.Http.Json;
using System.Net;
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

    public bool LockScan { get; set; } = false;

    private async Task PostQRCode(string e)
    {
        try
        {
            ApiModel apiModel = new();
            var Client = apiModel.getClient();

            QRCodeM code = new()
            {
                code = e
            };

            HttpResponseMessage response = await Client.PostAsJsonAsync("QRCode", code);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                QRCodeM rCode = await response.Content.ReadFromJsonAsync<QRCodeM>();

                Dispatcher.Dispatch(() => {
                    QRCodeLabel.Text = rCode.code;
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Data error please try again", "Ok");
            }
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "We encountered a serverside error please try again later", "OK");
        }
    }

    private async void BarcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {    
        if (!LockScan)
        {
            if (!e.Results[0].Value.StartsWith("https://"))
            {
                await PostQRCode(e.Results[0].Value);
            }
            else
            {
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
                catch (Exception)
                {
                    await Shell.Current.DisplayAlert("Error", "We encountered and error while trying to open the link please try again", "OK");
                }
            }

            Dispatcher.Dispatch(() => {
                LockScan = true;
                UnlockBtn.IsVisible = true;
            });
        }
    }

    private void UnlockBtn_Clicked(object sender, EventArgs e)
    {
        Dispatcher.Dispatch(() => {
            LockScan = false;
            UnlockBtn.IsVisible = false;
        });
    }
}
