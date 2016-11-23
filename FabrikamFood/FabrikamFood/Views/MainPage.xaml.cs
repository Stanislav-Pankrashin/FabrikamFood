using FabrikamFood.UserData;
using FabrikamFood.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FabrikamFood {
    public partial class MainPage : ContentPage {
        bool authenticated = false;

        protected override async void OnAppearing() {
            base.OnAppearing();

            if (authenticated == true) {
                // Hide the Sign-in button.
                //this.loginButton.IsVisible = false;
            }
        }

        async void loginButton_Clicked(object sender, EventArgs e) {
            if (App.Authenticator != null) {
                authenticated = await App.Authenticator.Authenticate();
            }
            if (authenticated == true) {
                ((Button)sender).Text = "logged in as " + Cart.userToken;
            }
        }

        public MainPage() {
            InitializeComponent();
        }
        public async void showAlert(object sender, EventArgs e) {
            await DisplayAlert("Yay!", "Button Press Registered", "ok");
        }
        public async void showMenu(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new MenuPage());
        }
        public async void showCart(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new CartPage());
        }
        public async void showMap(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new MapsPage());
        }
        public async void showPhoto(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new photoPage());
        }

    }
}
