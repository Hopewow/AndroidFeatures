using AndroidFeatures.Models;
using System.Net.Http.Json;
using System.Net;

namespace AndroidFeatures;

public partial class CameraPage : ContentPage
{
	public CameraPage()
	{
		InitializeComponent();
	}

    public async void TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                ApiModel apiModel = new();
                var Client = apiModel.getClient();

                ImageUploadM image = new() {
                    image = File.ReadAllBytes(photo.FullPath)
                };

                HttpResponseMessage response = await Client.PostAsJsonAsync("ImageUpload", image);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ImageUploadM rImage = await response.Content.ReadFromJsonAsync<ImageUploadM>();

                    Picture.Source = ImageSource.FromStream(() =>
                    {
                        return new MemoryStream(rImage.image);
                    });
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Data error please try again", "Ok");
                }                
            }
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        TakePhoto();
    }
}