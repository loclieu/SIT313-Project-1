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
    [Activity(Label = "UserDetails", Icon = "@drawable/calendar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class UserDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UserDetails);

            // Change Title
            Title = "User Details";
            // Retrieve share preference if exist
            //Retrieve User Details
            EditText nameBox = FindViewById<EditText>(Resource.Id.txtName);
            EditText emailBox = FindViewById<EditText>(Resource.Id.txtEmail);
            EditText studyCodeBox = FindViewById<EditText>(Resource.Id.txtStudyCourse);
            var localUserDetails = Application.Context.GetSharedPreferences("MyDetails", FileCreationMode.Private);
            string name = localUserDetails.GetString("Name", null);
            string email = localUserDetails.GetString("Email", null);
            string study = localUserDetails.GetString("Study", null);
            nameBox.Text = name;
            emailBox.Text = email;
            studyCodeBox.Text = study;

            Button cancel = FindViewById<Button>(Resource.Id.btnCancel);
            cancel.Click +=  delegate
            {
                Finish();
            };
            Button save = FindViewById<Button>(Resource.Id.btnSave);
            save.Click += delegate
            {
                 name = nameBox.Text;
                 email = emailBox.Text;
                 study = studyCodeBox.Text;

                // add to share preferences
                localUserDetails = Application.Context.GetSharedPreferences("MyDetails", FileCreationMode.Private);
                var detailEdit = localUserDetails.Edit();
                detailEdit.PutString("Name", name);
                detailEdit.PutString("Email", email);
                detailEdit.PutString("Study", study);
                detailEdit.Commit();

                Toast.MakeText(this, "Saved Details", ToastLength.Short).Show();
            };
        }
    }
}