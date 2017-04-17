using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android;
using Android.Support.Design.Widget;
using JarKon.ViewModel;
using JarKon.Core;

namespace JarKon.Droid
{
    [Activity(Label = "JarKon",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static string[] LOCATION_PERMISSIONS =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessMockLocation
        };

        const int REQUEST_LOCATION = 0;


        Android.Views.View layout;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Provider.Instance.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            Provider.Instance.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

            layout = FindViewById(Android.Resource.Id.Content);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);

            if (CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
            {
                MapViewModel.Instance.DisableUserLocation();
                RequestCoarseLocation();
            }
            else
            {
                MapViewModel.Instance.EnableUserLocation();
            }

            LoadApplication(new JarKon.App());
        }

        private void RequestCoarseLocation()
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.AccessCoarseLocation))
            {

                // Provide an additional rationale to the user if the permission was not granted
                // and the user would benefit from additional context for the use of the permission.
                // For example, if the request has been denied previously.
                //Log.Info(TAG, "Displaying contacts permission rationale to provide additional context.");

                // Display a SnackBar with an explanation and a button to trigger the request.
                Snackbar.Make(layout, Resource.String.location_permission_description,
                    Snackbar.LengthIndefinite).SetAction(Resource.String.ok, new Action<Android.Views.View>(delegate (Android.Views.View obj)
                    {
                        ActivityCompat.RequestPermissions(this, LOCATION_PERMISSIONS, REQUEST_LOCATION);
                    })).Show();
            }
            else
            {
                // Contact permissions have not been granted yet. Request them directly.
                ActivityCompat.RequestPermissions(this, LOCATION_PERMISSIONS, REQUEST_LOCATION);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case REQUEST_LOCATION:
                    if (grantResults[0] == Permission.Granted)
                    {
                        MapViewModel.Instance.EnableUserLocation();
                    }
                    break;
            }
        }
    }
}

