using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabrikamFood.UserData {


    class Cart {
        private static Cart userCart;
        public static bool loggedIn { get; set; }
        //public List<Tuple<string, double>> items { get; private set; }
        public Dictionary<string, Tuple<double, double>> items { get; private set; }
        public double totalPrice { get; private set; }
        public string userName;
        public string address;


        private Cart() {
            //this.items = new List<Tuple<string, double>>();
            //Dictionary is in the format "itemName": (price, quantity)
            this.items = new Dictionary<string, Tuple<double, double>>();

        }


        public static Cart CartInstance {
            get {
                if (userCart == null) {
                    userCart = new Cart();
                }
                return userCart;
            }

        }

        public void addItem(string item, double price) {
            if (items.ContainsKey(item)) {
                Tuple<double, double> tuple = items[item];
                items[item] = new Tuple<double, double>(tuple.Item1 * 2, tuple.Item2 + 1);
                totalPrice += price;

            } else {
                items.Add(item, new Tuple<double, double>(price, 1));
                totalPrice += price;
            }

        }

        /* public void deleteItem(string item) {
             try {
                 int counter = 0;
                 while (true) {
                     if (items[0].Item1 == item) {
                         items.RemoveAt(counter);

                     }
                     counter++;
                 }

             } catch { }
         }*/

        public void clearCart() {
            this.items = new Dictionary<string, Tuple<double, double>>();

        }



    }
}
