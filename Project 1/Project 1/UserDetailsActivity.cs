using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project_1
{
    [Activity(Label = "UserDetails")]
    public class UserDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UserDetails);

            this.Title = "Custom";
            Button cancel = FindViewById<Button>(Resource.Id.btnCancel);
            cancel.Click +=  delegate
            {
                Finish();
            };
        }
    }
}