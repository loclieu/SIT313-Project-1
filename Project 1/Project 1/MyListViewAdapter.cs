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
    class MyListViewAdapter : BaseAdapter<ClassList>
    {
        private List<ClassList> mItems;
        private Context mContext;
        
        public MyListViewAdapter(Context context, List<ClassList> items)
        {
            mItems = items;
            mContext = context;
        }  
        // how many row in list view
        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override ClassList this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview_row, null, false);
            }
            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = mItems[position].UnitCode;

            TextView txtType = row.FindViewById<TextView>(Resource.Id.txtType);
            txtType.Text = mItems[position].Type;

            TextView txtRoom = row.FindViewById<TextView>(Resource.Id.txtRoom);
            txtRoom.Text = mItems[position].Room;

            TextView txtTime = row.FindViewById<TextView>(Resource.Id.txtTime);
            txtTime.Text = mItems[position].Time ;

            TextView txtDay = row.FindViewById<TextView>(Resource.Id.txtDay);
            txtDay.Text = mItems[position].Day;

            return row;
        }

    }
}