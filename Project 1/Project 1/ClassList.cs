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
    class ClassList
    {
        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public string Room { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }
        public string Type { get; set; }
    }
}