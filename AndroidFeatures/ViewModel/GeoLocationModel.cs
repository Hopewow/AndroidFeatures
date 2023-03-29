using AndroidFeatures.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndroidFeatures.ViewModel
{
    public partial class GeoLocationModel : ObservableObject
    {
        [ObservableProperty]
        public string lat;

        [ObservableProperty]
        public string lon;

        [ObservableProperty]
        public string altitude;

        [ObservableProperty]
        public string accuracy;

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        [RelayCommand]
        public async Task GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    Lat = $"Latitude: {location.Latitude}";
                    Lon = $"Longitude: {location.Longitude}";
                    Altitude = $"Altitude: {location.Altitude}";
                    Accuracy = $"Accuracy: {location.Accuracy}";

                    ApiModel apiModel = new();
                    var Client = apiModel.getClient();

                    GeoLocationM geoLocation = new()
                    {
                        latitude = location.Latitude,
                        longitude = location.Latitude,
                        altitude = location.Altitude,
                        accuracy = location.Accuracy
                    };

                    HttpResponseMessage response = await Client.PostAsJsonAsync("GeoLocation", geoLocation);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        await Shell.Current.DisplayAlert("Error", "Data error please try again", "Ok");
                    }
                }
            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"{ex}", "Ok");
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
    }
}
