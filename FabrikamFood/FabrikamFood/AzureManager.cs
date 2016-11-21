using FabrikamFood.DataModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabrikamFood {
    class AzureManager {
        public static AzureManager manager;
        private MobileServiceClient client;
        private IMobileServiceTable<Menu> menuTable;
        //Get a service client and the table
        private AzureManager() {
            this.client = new MobileServiceClient("http://fabrikamfoods.azurewebsites.net");
            this.menuTable = this.client.GetTable<Menu>();
        }
        //If there is no isntance, create one and return it
        public static AzureManager AzureManagerInstance {
            get {
                if (manager == null) {
                    manager = new AzureManager();
                }
                return manager;
            }
        }
        //Remove all of the non type items from the menu, and return the smaller menu
        public async Task<List<Menu>> getSpecific(string type) {
            List<Menu> menuItems = await this.menuTable.ToListAsync();

            try {
                int counter = 0;
                while (true) {
                    if (menuItems[counter].DishType != type) {
                        menuItems.RemoveAt(counter);
                    } else {
                        counter++;
                    }
                }

            } catch { }

            return menuItems;
        }
        
    }
}
