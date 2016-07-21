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
    [Activity(Label = "ClassListActivity")]
    public class ClassListActivity : Activity
    {
        private List<ClassList> mitems;
        private ListView mListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ClassList);
            mListView = FindViewById<ListView>(Resource.Id.lvClassList);

            mitems = new List<ClassList>();
            mitems.Add(new ClassList() { UnitCode = "SIT313", Type ="Practical", Room = "T1.001", Time = "10:00", Day="Tuesday" });
            mitems.Add(new ClassList() { UnitCode = "SIT255", Type = "Lecture", Room = "B4.012", Time = "14:00", Day = "Wednesday" });
            mitems.Add(new ClassList() { UnitCode = "SIT313", Type = "Practical", Room = "T1.001", Time = "12:00", Day = "Friday" });

            MyListViewAdapter adapter = new MyListViewAdapter(this, mitems);
            mListView.Adapter = adapter;

        }
    }
}