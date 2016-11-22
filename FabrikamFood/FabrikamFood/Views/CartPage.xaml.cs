using Acr.UserDialogs;
using FabrikamFood.DataModels;
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
            Dictionary<string, Tuple<double, double>> items = Cart.CartInstance.items;
            CartList.ItemsSource = items;

        }

        public void clearCart(object sender, EventArgs e) {
            Cart.CartInstance.clearCart();
            CartList.ItemsSource = Cart.CartInstance.items;

        }

        public async void checkOut(object sender, EventArgs e) {
            List<string> items;
            string itemString;
            Dictionary<string, Tuple<double, double>> dict = Cart.CartInstance.items;

            string name = userName.Text;
            string address = userAddress.Text;
            string number = userNumber.Text;

            if (name == null) {
                await DisplayAlert("Please enter Information", "Please enter your name", "ok");
                return;
            }
            if (address == null) {
                await DisplayAlert("Please enter Information", "Please enter your address", "ok");
                return;
            }
            try {
                int testNumber = int.Parse(number);

            } catch {
                await DisplayAlert("Please enter Information", "Please enter a valid number", "ok");
                return;
            }


            try {
                items = new List<string>(dict.Keys);
                //Check to see if any items have multiples, if there are, add copies of that to the list
                int count = items.Count;
                for(int i = 0; i < count; i++) {
                    //If more than one exists
                    if(dict[items[i]].Item2 > 1) {
                        //add that many
                        int numToAdd = (int)dict[items[i]].Item2 - 1;
                        for(int j = numToAdd; j > 0; j--) {
                            items.Add(items[i]);
                        }
                    }

                }
                items.Sort();

                //delimit all of the elements with a ',' so it can later be seperated
                count = items.Count;
                for(int i = 0; i < count - 1; i++) {
                    items[i] += ",";

                }

                itemString = string.Concat(items);
                //Generate an orders object
                Orders order = new Orders();

                order.Name = name;
                order.Address = address;
                order.Items = itemString;
                order.PhoneNumber = number;
                //then upload it to the table
                await AzureManager.AzureManagerInstance.UpdateOrders(order);
                await DisplayAlert("Yay!", "Order sent, you will be contacted when order is ready", "ok");
                Cart.CartInstance.clearCart();
                CartList.ItemsSource = Cart.CartInstance.items;



            } catch {
                await DisplayAlert("Error", "Something went wrong :(", "ok");
            }


        }
    }
}
