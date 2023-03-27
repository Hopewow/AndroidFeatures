using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidFeatures.ViewModel
{
    public partial class Navigation : ObservableObject
    {
        [RelayCommand]
        public async Task RedirectScanner()
        {
            await Shell.Current.GoToAsync($"{nameof(ScannerPage)}");
        }

        [RelayCommand]
        public async Task RedirectCamera()
        {
            await Shell.Current.GoToAsync($"{nameof(CameraPage)}");
        }

        [RelayCommand]
        public async Task RedirectCurrentLocation()
        {
            await Shell.Current.GoToAsync($"{nameof(CurrentLocationPage)}");
        }

        [RelayCommand]
        void TriggerShake()
        {
            int secondsToVibrate = Random.Shared.Next(1, 7);
            TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

            Vibration.Default.Vibrate(vibrationLength);
        }
    }
}
