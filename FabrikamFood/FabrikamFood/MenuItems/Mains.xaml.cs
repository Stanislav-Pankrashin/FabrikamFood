using FabrikamFood.DataModels;
using FabrikamFood.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabrikamFood.MenuItems {
    public partial class Mains : ContentPage {
        List<Menu> menuItems;

        //Initialize component then get entree items
        public Mains() {
            InitializeComponent();
            this.getMainItems();
        }
        //Populate the view with the Mains
        public async void getMainItems() {
            UploadingIndicator.IsRunning = true;
            menuItems = await AzureManager.AzureManagerInstance.getSpecific("Main");

            MainList.ItemsSource = menuItems;
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
                }

            } catch {
                await DisplayAlert("Oh no!", "item not found", "ok");
            }




        }
    }
}
