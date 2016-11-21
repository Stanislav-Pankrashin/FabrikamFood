using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabrikamFood.UserData {
    class Cart {
        private static Cart userCart;
        public List<Tuple<string, double>> items { get; private set; }
        public double totalPrice { get; private set; }


        private Cart() {
            this.items = new List<Tuple<string, double>>();

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
            items.Add(new Tuple<string, double>(item, price));
            totalPrice += price;

        }

        public void deleteItem(string item) {
            try {
                int counter = 0;
                while (true) {
                    if (items[0].Item1 == item) {
                        items.RemoveAt(counter);

                    }
                    counter++;
                }

            } catch { }
        }

        public void clearCart() {
            this.items = new List<Tuple<string, double>>();

        }



    }
}
