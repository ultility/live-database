using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace live_database
{
    [Activity(Label = "ActuallyMainActivity", MainLauncher = true)]
    public class ActuallyMainActivity : Activity
    {
        Button start, create;
        EditText id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            id = FindViewById<EditText>(Resource.Id.id);
            start = FindViewById<Button>(Resource.Id.enter);
            create = FindViewById<Button>(Resource.Id.start);
            start.Click += Start_Click;
            create.Click += Create_Click;
            // Create your application here
        }

        private void Create_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            i.PutExtra("room", int.Parse(id.Text));
            i.PutExtra("invitee", true);
            StartActivity(i);
        }
    }
}