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
            Button userDetail = FindViewById<Button>(Resource.Id.btnDetails);
            TextView txtWeek = FindViewById<TextView>(Resource.Id.txtWeek);

            classes.Click += delegate
           {
               StartActivity(typeof(ClassListActivity));
           };

           
            userDetail.Click += delegate
            {
                StartActivity(typeof(UserDetailsActivity));
            };


            DateTime now = DateTime.Now.ToLocalTime();
            int currentDay = 16;    // End of week 1 is day 15
            int currentWeek = 1;
        
            if (now.Day > currentDay && now.Month <= 10) // if now is > than 16 then add 1 to current week, and 7 to currentDate to make a new week
            {
                currentDay+= 7; // new week checker
                currentWeek ++;
                txtWeek.Text = "Week: " + currentWeek.ToString();
            }
            else
            {
                txtWeek.Text = "Week: Exam Period";
            }
     
        }

     
            
         

    }
}

