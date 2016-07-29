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
using System.IO;

namespace Project_1
{
    [Activity(Label = "ClassListActivity")]
    public class ClassListActivity : Activity
    {
        private List<ClassList> mitems;
        private ListView mListView;
        MyListViewAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your application here
            SetContentView(Resource.Layout.ClassList);

            mListView = FindViewById<ListView>(Resource.Id.lvClassList);

            ORM.DBRepository dbr1 = new ORM.DBRepository();
            // var result1 = dbr1.CreateTable();
            // Toast.MakeText(this, result1, ToastLength.Short).Show();
            //  dbr1.GetAllRecords();
          
           
        
            mitems = new List<ClassList>(dbr1.AllClassList());
            /*
            mitems.Add(new ClassList() { UnitCode = "SIT313", Type ="Practical", Room = "T1.001", Time = "10:00", Day="Tuesday" });
            mitems.Add(new ClassList() { UnitCode = "SIT255", Type = "Lecture", Room = "B4.012", Time = "14:00", Day = "Wednesday" });
            mitems.Add(new ClassList() { UnitCode = "SIT313", Type = "Practical", Room = "T1.001", Time = "12:00", Day = "Friday" });
             */
            adapter = new MyListViewAdapter(this, mitems);
            mListView.Adapter = adapter;
            mListView.ItemClick += OnListItemClick;
           
            Button btnAddClass = FindViewById<Button>(Resource.Id.btnAddClass);
            btnAddClass.Click += delegate
            {
                //  ORM.DBRepository dbr = new ORM.DBRepository();
                // string result = dbr.InsertRecord(mitems[0].UnitCode, mitems[0].Room, mitems[0].Time, mitems[0].Day, mitems[0].Type);
                //  Toast.MakeText(this, result, ToastLength.Short).Show();
                fnShowCustomAlertDialog();
            };

            Button btnEditClass = FindViewById<Button>(Resource.Id.btnEditClass);
            btnEditClass.Click += delegate
            {
                  ORM.DBRepository dbr = new ORM.DBRepository();
                   var result = dbr.GetAllRecords();
            Toast.MakeText(this, result, ToastLength.Short).Show();
            };

            
        }

        void fnShowCustomAlertDialog()
        {
           
            
            
            //Inflate layout
            View view = LayoutInflater.Inflate(Resource.Layout.custom_dialog, null);
            AlertDialog builder = new AlertDialog.Builder(this).Create();
            builder.SetView(view);
            builder.SetCanceledOnTouchOutside(false);

            // Input variable
            EditText unitcode = view.FindViewById<EditText>(Resource.Id.txtUnitCode);
            EditText room = view.FindViewById<EditText>(Resource.Id.txtRoom);
            EditText time = view.FindViewById<EditText>(Resource.Id.txtTimes);
            EditText day = view.FindViewById<EditText>(Resource.Id.txtDay);
            EditText type = view.FindViewById<EditText>(Resource.Id.txtType);
            
            // Button
            Button add = view.FindViewById<Button>(Resource.Id.btnConfirm);
            Button cancel = view.FindViewById<Button>(Resource.Id.btnCancel);
            cancel.Click += delegate {
                builder.Dismiss();
                Toast.MakeText(this, "Alert dialog dismissed!", ToastLength.Short).Show();
            };
            add.Click += delegate
            {
                mitems.Add(new ClassList() { UnitCode = unitcode.Text, Type = type.Text, Room = room.Text, Time = time.Text, Day = day.Text });
                mListView.Adapter = adapter;
                ORM.DBRepository dbr = new ORM.DBRepository();
                string result = dbr.InsertRecord(unitcode.Text, type.Text, room.Text, time.Text, day.Text);
                Toast.MakeText(this, result, ToastLength.Short).Show();
                builder.Dismiss();
                Toast.MakeText(this, "Successfully Added.", ToastLength.Short).Show();
            };
            builder.Show();
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = mitems[e.Position];

            Toast.MakeText(this, t.UnitCode, Android.Widget.ToastLength.Short).Show();
        }
    }
}
 