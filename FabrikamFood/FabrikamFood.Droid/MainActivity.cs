using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using FabrikamFood.UserData;
using Plugin.Permissions;
using Android.Locations;
using Android.Content;
using FabrikamFood.Views;

namespace FabrikamFood.Droid {
    [Activity(Label = "FabrikamFood", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate {
        // Define a authenticated user.
        private MobileServiceUser user;
        public LocationManager locMgr;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults) {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async Task<bool> Authenticate() {
            var success = false;
            var message = string.Empty;
            try {
                // Sign in with Facebook login using a server-managed flow.
                user = await AzureManager.AzureManagerInstance.client.LoginAsync(this,
                   MobileServiceAuthenticationProvider.Facebook);

                if (user != null) {
                    message = string.Format("you are now signed-in as {0}.",
                        user.UserId);
                    success = true;
                }
            } catch (Exception ex) {
                message = ex.Message;
            }

            Cart.userObject = user;
            Cart.userId = user.UserId;
            Cart.loggedIn = true;
            //Cart.getUserInfo();

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();
            
            
            return success;
        }

        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            // Initialize the authenticator before loading the app.
            App.Init((IAuthenticate)this);
            base.OnCreate(bundle);
            locMgr = GetSystemService(Context.LocationService) as LocationManager;
            MapsPage.locMgr = locMgr;
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

    }
}

