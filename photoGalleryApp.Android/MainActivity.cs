using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using photoGalleryApp.Interfaces;

namespace photoGalleryApp.Droid
{
    [Activity(Label = "photoGalleryApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Current { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Current = this;
            _ = new Bootstrapper();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        [Obsolete]
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == 33)
            {
                var importer = (PhotoImporter)Resolver.Resolve<IPhotoImporter>();
                importer.ContinueWithPermission(grantResults[0] == Permission.Granted);
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
