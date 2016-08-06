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
    class UserInfo
    {
        private string study;

        public UserInfo(string name, string email, string study)
        {
            this.name = name;
            this.email = email;
            this.study = study;
        }

        public string name { get; set; }
        public string email { get; set; }
        public string courseCode { get; set; }

        public string getFirstName()
        {
            string firstname = "";
            firstname = name.Split(' ')[0];

            return firstname;
        }
    }
}