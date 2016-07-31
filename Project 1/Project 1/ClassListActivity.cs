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
        // Intialise variables to be used in this class
        private List<ClassList> mitems;
        private ListView mListView;
        MyListViewAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your application here
            SetContentView(Resource.Layout.ClassList);

            mListView = FindViewById<ListView>(Resource.Id.lvClassList);

            // Initialise the DB
            ORM.DBRepository dbr1 = new ORM.DBRepository();
          
            // Create the ListView and Link them with the DB Table
            mitems = new List<ClassList>(dbr1.AllClassList());
            adapter = new MyListViewAdapter(this, mitems);
            mListView.Adapter = adapter;
            mListView.ItemClick += OnListItemClick;
           
            Button btnAddClass = FindViewById<Button>(Resource.Id.btnAddClass);
            btnAddClass.Click += delegate
            {
                // Show dialog
                AddNewClassDialog();
            };

            /*
            Button btnEditClass = FindViewById<Button>(Resource.Id.btnEditClass);
            btnEditClass.Click += delegate
            {
                  ORM.DBRepository dbr = new ORM.DBRepository();
                   var result = dbr.GetAllRecords();
            Toast.MakeText(this, result, ToastLength.Short).Show();
            };
            */
            
        }

        void AddNewClassDialog()
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
            // Last Item in list
         
            // Buttons
            Button add = view.FindViewById<Button>(Resource.Id.btnConfirm);
            Button cancel = view.FindViewById<Button>(Resource.Id.btnCancel);
            // Cancel Click
            cancel.Click += delegate {
                builder.Dismiss();
                Toast.MakeText(this, "Alert dialog dismissed!", ToastLength.Short).Show();
            };
            // Add Click
            add.Click += delegate
            {
                // Get text from the form and add to the ClassList
                mitems.Add(new ClassList() { UnitCode = unitcode.Text, Type = type.Text, Room = room.Text, Time = time.Text, Day = day.Text });             
                ORM.DBRepository dbr = new ORM.DBRepository();
                string result = dbr.InsertRecord(unitcode.Text, type.Text, room.Text, time.Text, day.Text);
                builder.Dismiss();

                // Re-initialise the ListView, so that it refreshes
                // Ineffecient but I couldn't figure out a better way
                mitems = new List<ClassList>(dbr.AllClassList());
                adapter = new MyListViewAdapter(this, mitems);
                mListView.Adapter = adapter;
                // Tell users that item is successfully added.
                Toast.MakeText(this, "Successfully Added.", ToastLength.Short).Show();
            };
            builder.Show();
        }

        // When Selecting a single item in ListView
        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            // Get Item Position
            /*
            var t = mitems[e.Position];
            ORM.DBRepository dbr = new ORM.DBRepository();
            var delete = dbr.RemoveUnit(t.UnitId);
            // Re-initialise the ListView, so that it refreshes
            // Ineffecient but I couldn't figure out a better way
            mitems = new List<ClassList>(dbr.AllClassList());
            adapter = new MyListViewAdapter(this, mitems);
            mListView.Adapter = adapter;
            Toast.MakeText(this, delete, ToastLength.Short).Show();
            */
            // Past down the position value
            EditClassDialog(e.Position);
        }



        // Edit Class Dialog
        void EditClassDialog(int position)
        {
            //Inflate layout
            View view = LayoutInflater.Inflate(Resource.Layout.custom_dialog, null);
            AlertDialog builder = new AlertDialog.Builder(this).Create();
            builder.SetView(view);
            builder.SetCanceledOnTouchOutside(true);

            // Get Selected Item ID
            var item = mitems[position];
            ORM.DBRepository dbr = new ORM.DBRepository();
           
            // Input variable
            EditText UnitCode = view.FindViewById<EditText>(Resource.Id.txtUnitCode);
            EditText Room = view.FindViewById<EditText>(Resource.Id.txtRoom);
            EditText Time = view.FindViewById<EditText>(Resource.Id.txtTimes);
            EditText Day = view.FindViewById<EditText>(Resource.Id.txtDay);
            EditText Type = view.FindViewById<EditText>(Resource.Id.txtType);
            // Change input value to exist one
            UnitCode.Text = item.UnitCode;
            Room.Text = item.Room;
            Time.Text = item.Time;
            Day.Text = item.Day;
            Type.Text = item.Type;

            // Buttons
            Button Edit = view.FindViewById<Button>(Resource.Id.btnConfirm);
            Button Delete = view.FindViewById<Button>(Resource.Id.btnCancel);
            Edit.Text = "Edit";
            Delete.Text = "Delete";
            // Edit Click
            Edit.Click += delegate {
                var update = dbr.Updateitem(item.UnitId, UnitCode.Text, Type.Text, Room.Text, Time.Text, Day.Text);
                builder.Dismiss();
                // Re-initialise the ListView, so that it refreshes
                // Ineffecient but I couldn't figure out a better way
                mitems = new List<ClassList>(dbr.AllClassList());
                adapter = new MyListViewAdapter(this, mitems);
                mListView.Adapter = adapter;
                Toast.MakeText(this, update, ToastLength.Short).Show();
            };
            // Delete Click
            Delete.Click += delegate
            {
                var remove = dbr.RemoveUnit(item.UnitId);
                builder.Dismiss();
                // Re-initialise the ListView, so that it refreshes
                // Ineffecient but I couldn't figure out a better way
                mitems = new List<ClassList>(dbr.AllClassList());
                adapter = new MyListViewAdapter(this, mitems);
                mListView.Adapter = adapter;
                Toast.MakeText(this, remove, ToastLength.Short).Show();
            };
            builder.Show();
        }
    }
}
 