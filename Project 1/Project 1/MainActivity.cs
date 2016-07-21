using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Project_1
{
    [Activity(Label = "Project_1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
       

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestWindowFeature(WindowFeatures.NoTitle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            // Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            Button classes = FindViewById<Button>(Resource.Id.btnClass);

            classes.Click += delegate
           {
               StartActivity(typeof(ClassListActivity));
           };

            Button userDetail = FindViewById<Button>(Resource.Id.btnDetails);
            userDetail.Click += delegate
            {
                StartActivity(typeof(UserDetailsActivity));
            };


        }

    }
}

