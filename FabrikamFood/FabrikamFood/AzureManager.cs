using FabrikamFood.DataModels;
using FabrikamFood.UserData;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabrikamFood {
    public class AzureManager {
        public static AzureManager manager;
        public MobileServiceClient client;
        private IMobileServiceTable<Menu> menuTable;
        private IMobileServiceTable<Orders> ordersTable;
        private IMobileServiceTable<UsersData> usersTable;
        //Get a service client and the table
        public AzureManager() {
            this.client = new MobileServiceClient("http://fabrikamfoodsv2.azurewebsites.net");
            this.menuTable = this.client.GetTable<Menu>();
        }
        //If there is no instance, create one and return it
        public static AzureManager AzureManagerInstance {
            get {
                if (manager == null) {
                    manager = new AzureManager();
                }
                return manager;
            }
        }

        public async Task UpdateOrders(Orders order) {
            if (ordersTable == null) {
                this.ordersTable = this.client.GetTable<Orders>();
            }
            await this.ordersTable.InsertAsync(order);
        }

        public async Task UpdatePersonalInfo(Orders order) {
            if (usersTable == null) {
                this.usersTable = this.client.GetTable<UsersData>();
            }

            try {
                UsersData data = new UsersData();
                List<UsersData> usersItems = await this.usersTable.ToListAsync();
                int len = usersItems.Count;
                for (int i = 0; i < len; i++) {
                    if (usersItems[i].UserId == Cart.userId) {
                        data.id = usersItems[i].id;
                        /*data.createdAt = usersItems[i].c;
                        data.version;
                        data.deleted;*/
                        data.UserName = order.Name;
                        data.UserAddress = order.Address;
                        data.UserNumber = order.PhoneNumber;
                        data.LastOrder = order.Items;
                        data.UserId = Cart.userId;

                        await this.usersTable.UpdateAsync(data);

                    } else {
                        data.UserName = order.Name;
                        data.UserAddress = order.Address;
                        data.UserNumber = order.PhoneNumber;
                        data.LastOrder = order.Items;
                        data.UserId = Cart.userId;

                        await this.usersTable.InsertAsync(data);
                    }

                }


            } catch { }



        }



        //Remove all of the non type items from the menu, and return the smaller menu
        public async Task<List<Menu>> getSpecific(string type) {
            List<Menu> menuItems = await this.menuTable.ToListAsync();

            if (type == "") {
                return menuItems;
            }

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
