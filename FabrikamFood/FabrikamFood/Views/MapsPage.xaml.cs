using Android.Locations;
using FabrikamFood.DataModels;
using FabrikamFood.UserData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FabrikamFood.Views {
    public partial class MapsPage : ContentPage {
        public static LocationManager locMgr;
        string provider;
        string lat;
        string longitude;
        double distance;
        string kmDistance;
        string timeTaken;
        double time;

        public MapsPage() {
            InitializeComponent();
            cartButton.Text = string.Format("Cart ({0})", Cart.numberItems);


            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-36.850213, 174.763808), Distance.FromKilometers(1)));
            var position = new Position(-36.850213, 174.763808); // Latitude, Longitude
            var pin = new Pin {
                Type = PinType.Place,
                Position = position,
                Label = "Fabrikam Food",
                Address = "22 Elliot St"
            };
            MyMap.Pins.Add(pin);

            this.getLocation();
            if (lat != null) {
                var userPosition = new Position(double.Parse(lat), double.Parse(longitude)); // Latitude, Longitude
                var userLoc = new Pin {
                    Type = PinType.Place,
                    Position = userPosition,
                    Label = "Your location",
                    Address = ""
                };
                MyMap.Pins.Add(userLoc);
                this.googleRequest();

                //Distance equation , Distance = sqrt((X2 - X1) ^ 2 + (y2 - y1)^2)
                distance = Math.Sqrt((-36.850213 - double.Parse(lat) * (-36.850213 - double.Parse(lat)) + (174.763808 - double.Parse(lat) * (174.763808 - double.Parse(longitude)))));

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-36.850213, 174.763808), Distance.FromKilometers(distance + 3)));
            }
        }

        private async void getLocation() {
            try {
                provider = LocationManager.GpsProvider;
                Location location = locMgr.GetLastKnownLocation(provider);

                lat = location.Latitude.ToString();
                longitude = location.Longitude.ToString();


            } catch {
                await DisplayAlert("Oops", "It looks like you don't have gps enabled", "ok");
                outputField.Text = string.Format("No Gps Available :(");
            }
        }

        private async void googleRequest() {
            GoogleResponse.RootObject rootObject;
            HttpClient client = new HttpClient();

            string x = await client.GetStringAsync(new Uri("https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=-36.850213,174.763808&destinations="+this.lat + "," + this.longitude));
            rootObject = JsonConvert.DeserializeObject<GoogleResponse.RootObject>(x);
            string thekmDistance = rootObject.rows[0].elements[0].distance.text;
            string thetimeTaken = rootObject.rows[0].elements[0].duration.text;
            outputField.Text = string.Format("We estimate the distance to reach us as: {0}, with a time of: {1} by car. ", thekmDistance, thetimeTaken);


        }

        public async void BackClicked(object sender, EventArgs e) {
            await Navigation.PopModalAsync();

        }
        public async void CartClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new CartPage());
        }
        public async void whereToFind(object sender, EventArgs e) {
            await DisplayAlert("Where We are", "You can find us at 22 Elliot St, Auckland. We are open 9-5 every day. Please call 1234567 for reservations", "Ok");
        }
    }
}
