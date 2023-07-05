using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using Android.Content;

namespace VivaPaymentPOC.Droid
{
    [Activity(Label = "VivaPaymentPOC", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            DependencyService.Register<ICallbackHandler, CallbackHandler_Android>();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    [Activity(Label = "CallbackActivity",Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
      Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
      DataSchemes = new[] { "mycallbackscheme" },
      DataHost = "callback")]
    public class CallbackActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            ICallbackHandler callbackHandler = DependencyService.Get<ICallbackHandler>();   
            base.OnCreate(savedInstanceState);

            // Get the data sent by the calling application
            Intent intent = this.Intent;
            if (intent?.Data != null)
            {
                string callbackData = intent.Data.ToString();
                callbackHandler.HandleCallback(callbackData);
                System.Diagnostics.Debug.WriteLine(callbackData);   
                // Process the callback data as needed
            }

            // Close the activity
            //Finish();
        }
    }

}