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
    [Activity(Label = "Class List", Icon = "@drawable/calendar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
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


            Button btnBack = FindViewById<Button>(Resource.Id.btnEditClass);
            btnBack.Click += delegate
            {
                // Finish Activity
               Finish();
            };


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
            EditText type = view.FindViewById<EditText>(Resource.Id.txtType);
            Spinner spinner = view.FindViewById<Spinner>(Resource.Id.spinnerDay);
           
            // Apply the string array to Spinner
            var Spinneradapter = ArrayAdapter.CreateFromResource(this, Resource.Array.day_name, Resource.Layout.custom_spinner_item);
            Spinneradapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = Spinneradapter;
           
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
                mitems.Add(new ClassList() { UnitCode = unitcode.Text, Type = type.Text, Room = room.Text, Time = time.Text, Day = spinner.SelectedItem.ToString() });             
                ORM.DBRepository dbr = new ORM.DBRepository();
                string result = dbr.InsertRecord(unitcode.Text, type.Text, room.Text, time.Text, spinner.SelectedItem.ToString());
                builder.Dismiss();

                // Re-initialise the ListView, so that it refreshes
                // Ineffecient but I couldn't figure out a better way
                mitems = new List<ClassList>(dbr.AllClassList());
                adapter = new MyListViewAdapter(this, mitems);
                mListView.Adapter = adapter;
                // Tell users that item is successfully added.
                Toast.MakeText(this, "Successfully Added." + spinner.SelectedItem.ToString(), ToastLength.Short).Show();
            };
            builder.Show();
        }

        // When Selecting a single item in ListView
        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
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
            EditText Type = view.FindViewById<EditText>(Resource.Id.txtType);
            Spinner spinner = view.FindViewById<Spinner>(Resource.Id.spinnerDay);
            TextView title = view.FindViewById<TextView>(Resource.Id.titleAddClass);
            
            // Apply the string array to Spinner
            var SpinnerAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.day_name, Resource.Layout.custom_spinner_item);
            SpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = SpinnerAdapter;

            // Change title of dialog, since I reuse the Add Class Dialog, so I need to change 
            // it to edit Class
            title.Text = "Edit Class";

            // Change input value to exist on
            UnitCode.Text = item.UnitCode;
            Room.Text = item.Room;
            Time.Text = item.Time;
            spinner.SetSelection(SpinnerAdapter.GetPosition(item.Day));
            Type.Text = item.Type;

            // Buttons
            Button Edit = view.FindViewById<Button>(Resource.Id.btnConfirm);
            Button Delete = view.FindViewById<Button>(Resource.Id.btnCancel);

            // Change text of button to match with current dialog.
            Edit.Text = "Edit";
            Delete.Text = "Delete";
            // Edit Click
            Edit.Click += delegate {
                
                var update = dbr.Updateitem(item.UnitId, UnitCode.Text, Type.Text, Room.Text, Time.Text, spinner.SelectedItem.ToString());
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
 