using FabrikamFood.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabrikamFood.Views {
    public partial class CartPage : ContentPage {
        public CartPage() {
            InitializeComponent();
            this.populateCart();
        }

        public void populateCart() {
            List<Tuple<string, double>> items = Cart.CartInstance.items;
            CartList.ItemsSource = items;

        }

        public void clearCart(object sender, EventArgs e) {
            Cart.CartInstance.clearCart();
            CartList.ItemsSource = Cart.CartInstance.items;

        }

        public void checkOut(object sender, EventArgs e) {
            //Some serverside stuff goes here

        }
    }
}
