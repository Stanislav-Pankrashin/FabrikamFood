﻿using FabrikamFood.DataModels;
using FabrikamFood.UserData;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabrikamFood.Views {
    public partial class PreviousOrder : ContentPage {
        List<string> splitItems;
        public PreviousOrder() {
            InitializeComponent();
            populateCart();

        }

        public void populateCart() {
            string userItems = CrossSettings.Current.GetValueOrDefault("userItems", "");
            splitItems = new List<string>(userItems.Split(','));
            List<Tuple<string, string>> convertedItems = new List<Tuple<string, string>>();
            for (int i = 0; i < splitItems.Count; i++) {
                convertedItems.Add(new Tuple<string, string>(splitItems[i], ""));
            }

            //Dictionary<string, Tuple<double, double>> items = Cart.CartInstance.items;
            CartList.ItemsSource = convertedItems;

        }
        public async void addToOrder(object sender, EventArgs e) {
            await DisplayAlert("Note", "This opereration may take a few moments. this page will close automatically when completed", "ok");
            UploadingIndicator.IsRunning = true;
            List<Menu> menu = await AzureManager.AzureManagerInstance.getSpecific("");

            for (int i = 0; i < splitItems.Count; i++) {
                for (int j = 0; j < menu.Count; j++) {
                    if (menu[j].DishName == splitItems[i]) {
                        Cart.CartInstance.addItem(menu[j].DishName, menu[j].DishPrice);
                        j = 1000000;
                    }

                }
            }
            UploadingIndicator.IsRunning = false;
            await Navigation.PopModalAsync();


        }


        public async void BackClicked(object sender, EventArgs e) {
            await Navigation.PopModalAsync();

        }

        public async void howToClicked(object sender, EventArgs e) {
            await DisplayAlert("How To", "Simply press the button at the bottom, And your previous order will be added to your current cart", "Ok");

        }

        public async void removeItem(object sender, EventArgs e) {
            await DisplayAlert("yay!", "It worked!", "Ok");
        }
    }
}
