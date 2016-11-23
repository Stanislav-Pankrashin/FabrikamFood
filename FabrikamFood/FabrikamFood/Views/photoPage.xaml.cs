using Android.Content.PM;
using Android.Views;
using Microsoft.ProjectOxford.Emotion;
using Plugin.Media;
using System;
using System.Collections.Generic;
using FabrikamFood.DataModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using FabrikamFood.UserData;

namespace FabrikamFood.Views {
    public partial class photoPage : ContentPage {
        public photoPage() {
            InitializeComponent();
        }

        public async void TakePicture_Clicked(object Sender, EventArgs e) {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                await DisplayAlert("No Camera", ":( No camera available", "OK");
                return;
            }
            //Else
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() {
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                Directory = "Moodify",
                Name = $"{DateTime.UtcNow}.jpg",
                CompressionQuality = 92

            });
            //If user cancels or whatever, end the method
            if (file == null) {
                return;
            }
            try {
                //Let a loading thing appear on the screen so the user knows something is happening
                UploadingIndicator.IsRunning = true;

                //Api key
                string emotionKey = "a54ea89bcd78448c80881ec86fcf55f8";

                //Create a service client which will handle our api calls
                EmotionServiceClient emotionClient = new EmotionServiceClient(emotionKey);

                //Get the response from our photo that we took
                var emotionResults = await emotionClient.RecognizeAsync(file.GetStream());

                //Remove the spinny loading thing
                UploadingIndicator.IsRunning = false;

                //Retrieve our scores
                var temp = emotionResults[0].Scores;

                double Anger = temp.Anger;
                double Contempt = temp.Contempt;
                double Disgust = temp.Disgust;
                double Fear = temp.Fear;
                double Happiness = temp.Happiness;
                double Neutral = temp.Neutral;
                double Sadness = temp.Sadness;
                double Surprise = temp.Surprise;
                double[] resultsList = { Anger, Contempt, Disgust, Fear, Happiness, Neutral, Sadness, Surprise };
                double max = 0;
                for(int i = 0; i < resultsList.Length; i++) {
                    if(resultsList[i] > max) {
                        max = resultsList[i];
                    }

                }

                if (max == temp.Anger) {
                    itemClicked("Chicken Tikka");
                }
                else if (max == temp.Contempt) {
                    itemClicked("Seekh Kabab");
                } else if (max == temp.Disgust) {
                    itemClicked("Paneer tikka");
                } else if (max == temp.Fear) {
                    itemClicked("Butter Chicken");
                } else if (max == temp.Happiness) {
                    itemClicked("Beef Vindaloo");
                } else if (max == temp.Neutral) {
                    itemClicked("Lamb Rogan Josh");
                } else if (max == temp.Sadness) {
                    itemClicked("Chicken Tikka Masala");
                } else if (max == temp.Surprise) {
                    itemClicked("Navrattan Korma");
                } else{
                    itemClicked("Butter Chicken");
                }


                //await DisplayAlert("Success!", "okay", "ok");
            } catch {
                await DisplayAlert("Oops...", "Looks like A face Wasn't Detected", "ok"); }
        }

        public async void itemClicked(string dishname) {
            List<DataModels.Menu> menuItems = await AzureManager.AzureManagerInstance.getSpecific("");
            string itemName = dishname;
            string itemDescription;
            double itemprice;
            bool addItem;
            DataModels.Menu item;

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
                addItem = await DisplayAlert("We Recommend: " + itemName, itemDescription + "\n\n" + "$" + itemprice, "Add To Order", "no thanks");
                if (addItem) {
                    Cart.CartInstance.addItem(itemName, itemprice);
                }
            } catch {
                await DisplayAlert("Oh no!", "item not found", "ok");
            }




        }
    }
}
