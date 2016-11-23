using FabrikamFood.MenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabrikamFood {
    public partial class MenuPage : ContentPage {
        public MenuPage() {
            InitializeComponent();
        }

        public async void entreesClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new Entrees());
        }

        public async void mainsClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new Mains());
        }

        public async void vegetarianClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new Vegetarian());
        }
        public async void BackClicked(object sender, EventArgs e) {
            await Navigation.PopModalAsync();

        }

    }
}
