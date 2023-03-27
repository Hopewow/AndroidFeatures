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
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                Picture.Source = ImageSource.FromFile(localFilePath);
            }
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        TakePhoto();
    }
}