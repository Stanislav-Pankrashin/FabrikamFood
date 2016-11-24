using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace FabrikamFood.UserData {


    public class Cart {
        private static Cart userCart;
        public static bool loggedIn { get; set; }
        //public List<Tuple<string, double>> items { get; private set; }
        public Dictionary<string, Tuple<double, double>> items { get; private set; }
        public static double totalPrice { get; private set; }
        public static int numberItems { get; set; }
        public static MobileServiceUser userObject { get; set; }
        public static string userToken;
        public static string userId;
        public string userName;
        public string address;


        private Cart() {
            //this.items = new List<Tuple<string, double>>();
            //Dictionary is in the format "itemName": (price, quantity)
            this.items = new Dictionary<string, Tuple<double, double>>();

            numberItems = 0;

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
            numberItems++;

        }

        public void clearCart() {
            this.items = new Dictionary<string, Tuple<double, double>>();
            Cart.totalPrice = 0;
            Cart.numberItems = 0;

        }
        /*
        public static async void getUserInfo() {
            userToken = AzureManager.AzureManagerInstance.client.CurrentUser.MobileServiceAuthenticationToken;
            string authToken = "Bearer" + " " + userToken;
            var webRequest = System.Net.WebRequest.Create("https://graph.facebook.com/v2.8/me?fields=id,name");
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";
            webRequest.Headers["Authorization"] = authToken;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);
            var x = await client.GetStringAsync("https://graph.facebook.com/v2.8/me?fields=id,name");
            Cart.userToken = x;






            
             userToken = AzureManager.AzureManagerInstance.client.CurrentUser.MobileServiceAuthenticationToken;
             HttpWebRequest client = (HttpWebRequest)WebRequest.Create("https://graph.facebook.com/v2.8/me?fields=id,name");
             string authToken = "Bearer" + " " + userToken;

             client.Headers["Authorization"] = authToken;
             WebResponse x = client.BeginGetRequestStream();
             


        }*/



    }
}
