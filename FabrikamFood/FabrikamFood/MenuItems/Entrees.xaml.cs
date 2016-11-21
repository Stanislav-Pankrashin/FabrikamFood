using FabrikamFood.DataModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabrikamFood.MenuItems {
    public partial class Entrees : ContentPage {
        //Initialize component then get entree items
        public Entrees() {
            InitializeComponent();
            this.getEntreeItems();
        }
        //Populate the view with the entrees
        public async void getEntreeItems() {
            UploadingIndicator.IsRunning = true;
            List<Menu> menuItems = await AzureManager.AzureManagerInstance.getEntree();

            //EntreeList.ItemsSource = menuItems;
            //UploadingIndicator.IsRunning = false;
            try {
                int counter = 0;
                while (true) {
                    var item = menuItems[counter];
                    Button btn = new Button();
                    btn.Text = item.DishName + item.DishPrice;


                }
                   

            }catch {


            }

        }
    }
}


