using FabrikamFood.DataModels;
using FabrikamFood.UserData;
using FabrikamFood.Views;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabrikamFood.MenuItems {
    public partial class Entrees : ContentPage {
        List<Menu> menuItems;

        //Initialize component then get entree items
        public Entrees() {
            InitializeComponent();
            this.getEntreeItems();
            cartButton.Text = string.Format("Cart({0})", Cart.numberItems);
        }
        //Populate the view with the entrees
        public async void getEntreeItems() {
            UploadingIndicator.IsRunning = true;
            menuItems = await AzureManager.AzureManagerInstance.getSpecific("Entree");

            EntreeList.ItemsSource = menuItems;
            UploadingIndicator.IsRunning = false;

        }

        public async void itemClicked(object sender, EventArgs e) {
            Button btn = (Button)sender;
            string itemName = btn.Text;
            string itemDescription;
            double itemprice;
            bool addItem;
            Menu item;

            try {
                int counter = 0;
                while (true) {
                    if (menuItems[counter].DishName == itemName) {
                        item = menuItems[counter];
                        break;
                    }
                    counter++;

                }

                itemDescription = item.DishDescription;
                itemprice = item.DishPrice;
                addItem = await DisplayAlert(itemName, itemDescription + "\n\n" + "$" + itemprice, "purchase", "no thanks");
                if (addItem) {
                    Cart.CartInstance.addItem(itemName, itemprice);
                    cartButton.Text = string.Format("Cart({0})", Cart.numberItems);
                }
            } catch {
                await DisplayAlert("Oh no!", "item not found", "ok");
            }
            cartButton.Text = string.Format("Cart({0})", Cart.numberItems);


        }
        public async void BackClicked(object sender, EventArgs e) {
            await Navigation.PopModalAsync();

        }
        public async void CartClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new CartPage());
        }

    }
}


