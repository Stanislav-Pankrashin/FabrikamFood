using FabrikamFood.UserData;
using FabrikamFood.Views;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FabrikamFood {
    public partial class MainPage : ContentPage {
        bool authenticated = false;

        /*async void loginButton_Clicked(object sender, EventArgs e) {
            if (App.Authenticator != null) {
                authenticated = await App.Authenticator.Authenticate();
            }
            if (authenticated == true) {
                ((Button)sender).Text = "logged in as " + Cart.userToken;
            }
        }*/

        public MainPage() {
            InitializeComponent();
            string name = CrossSettings.Current.GetValueOrDefault("userName", "");
            if(!name.Equals("")) {
                Banner.Text = "Welcome Back To Fabrikam Foods " + name + ".";
            }
            //cartButton.Text = string.Format("Cart ({0})", 0);
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
        public async void showPast(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new PreviousOrder());
        }
        public async void showAbout(object sender, EventArgs e) {
            await DisplayAlert("About", "This app was created for the purposes of assessment for MSA. This is not intended for commercial use.", "Ok");
        }
        public async void showHowTo(object sender, EventArgs e) {
            bool clear = await DisplayAlert("How To", "Simply add items from our menu. And check them out at the cart! It's that easy. We'll even save your information for next time to make things easier", "Clear User Data", "Ok");
            if(clear) {
                CrossSettings.Current.Remove("userName");
                CrossSettings.Current.Remove("userAddress");
                CrossSettings.Current.Remove("userNumber");
                CrossSettings.Current.Remove("userItems");
                await DisplayAlert("Cleared", "User Data Cleared", "Ok");

            }
        }

    }
}
