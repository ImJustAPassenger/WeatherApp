
using Microsoft.Maui.Controls.Maps;

using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using WeatherApp.ViewModels;
namespace WeatherApp.Pages
{

    public partial class MapPage : ContentPage
    {
        public MapPage(WeatherInfoViewModel weatherInfoViewModel)
        {
            InitializeComponent();
            _weatherInfoViewModel = weatherInfoViewModel;
        }

        private readonly WeatherInfoViewModel _weatherInfoViewModel;

        protected override async void OnAppearing()
        {
            var geolocationRequest = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(geolocationRequest);
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(20)));
            var pin = new Pin
            {
                Label = "First Location",
                Address = "address",
                Type = PinType.Place,
                Location = location
            };
            pin.MarkerClicked += Pin_MarkerClicked;
            myMap.Pins.Add(pin);
            var startingPoint = new Circle
            {
                Center = location,
                Radius = Distance.FromMeters(30),
                StrokeColor = Colors.White,
                StrokeWidth = 8,
                FillColor = Colors.Green
            };
            myMap.MapElements.Add(startingPoint);

        }




        private void myMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            /* 
                        var distance = e.Location.CalculateDistance(
                           myMap.Ma.First().Location.Latitude,
                           myMap.Pins.First().Location.Longitude,
                           DistanceUnits.Kilometers
                           ); */

            /*             var pin = new Pin
                        {
                            Label = "location clicked",
                            Address = $"{distance} km from point 0",
                            Type = PinType.Place,
                            Location = e.Location
                        };
                        pin.MarkerClicked += Pin_MarkerClicked;
                        myMap.Pins.Add(pin);
             */
            var endingPoint = new Circle
            {
                Center = e.Location,
                Radius = Distance.FromMeters(30),
                StrokeColor = Colors.White,
                StrokeWidth = 8,
                FillColor = Colors.Red
            };
            myMap.MapElements.Add(endingPoint);
            var polyline = new Polyline
            {
                StrokeColor = Colors.Blue,
                StrokeWidth = 5,
                Geopath ={
                    myMap.Pins.First().Location,
                    e.Location
                }
            };
            myMap.MapElements.Add(polyline);
        }

        private async void Pin_MarkerClicked(object? sender, PinClickedEventArgs e)
        {
            var pinInfo = (Pin)sender;
            await DisplayAlert("hi", pinInfo.Address, "ok");
        }

        private async void GetWeather_Clicked(object sender, EventArgs e)
        {
            var geolocationRequest = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(geolocationRequest);
            _weatherInfoViewModel.Latitude = location.Latitude.ToString();
            _weatherInfoViewModel.Longitude = location.Longitude.ToString();
            await _weatherInfoViewModel.FetchFromCoordinateAsync();
        }
    }
}