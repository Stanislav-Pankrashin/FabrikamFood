using FabrikamFood.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FabrikamFood {
    public partial class MainPage : ContentPage {
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
    }
}
