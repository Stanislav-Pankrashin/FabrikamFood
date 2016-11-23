using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FabrikamFood.Views {
    public partial class MapsPage : ContentPage {
        public MapsPage() {
            InitializeComponent();
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-36.850213, 174.763808), Distance.FromKilometers(1)));
            var position = new Position(-36.850213, 174.763808); // Latitude, Longitude
            var pin = new Pin {
                Type = PinType.Place,
                Position = position,
                Label = "Fabrikam Food",
                Address = "Elliot St"
            };
            MyMap.Pins.Add(pin);
        }
    }
}
