using FabrikamFood.MenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using FabrikamFood.UserData;
using FabrikamFood.Views;

namespace FabrikamFood {
    public partial class MenuPage : ContentPage {
        public MenuPage() {
            InitializeComponent();
            cartButton.Text = string.Format("Cart ({0})", Cart.numberItems);
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
        public async void CartClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new CartPage());
        }



    }
}
