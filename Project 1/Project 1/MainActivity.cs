using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Project_1
{
    [Activity(Label = "Project_1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        // Intialise variables to be used in this class
        private List<ClassList> mitems;
        private ListView mListView;
        MyListViewAdapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestWindowFeature(WindowFeatures.NoTitle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Create Database if it doesnt exist
            ORM.DBRepository dbr1 = new ORM.DBRepository();
            var result1 = dbr1.CreateTable();
            // Button Names
            Button classes = FindViewById<Button>(Resource.Id.btnClass);
            Button userDetail = FindViewById<Button>(Resource.Id.btnDetails);
            TextView txtWeek = FindViewById<TextView>(Resource.Id.txtWeek);
            txtWeek.Text = DateTime.Now.ToShortDateString ();
            classes.Click += delegate
           {
               StartActivity(typeof(ClassListActivity));
           };

           
            userDetail.Click += delegate
            {
                StartActivity(typeof(UserDetailsActivity));
            };





            // Create the ListView and Link them with the DB Table
            mListView = FindViewById<ListView>(Resource.Id.lvTodayClass);
            mitems = new List<ClassList>(dbr1.TodayClassList());
            if (mitems.Count == 0)
            {
                mitems.Add(new ClassList() { UnitCode = "No Class Today" });
            }
            adapter = new MyListViewAdapter(this, mitems);
            mListView.Adapter = adapter;
            mListView.ItemClick += OnListItemClick;
        }

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Refresh List View on tapped
            ORM.DBRepository dbr = new ORM.DBRepository();
            mListView = FindViewById<ListView>(Resource.Id.lvTodayClass);
            mitems = new List<ClassList>(dbr.TodayClassList());
            if (mitems.Count == 0)
            {
                mitems.Add(new ClassList() { UnitCode = "No Class Today" });
            }
            adapter = new MyListViewAdapter(this, mitems);
            mListView.Adapter = adapter;
        }
    }
}

