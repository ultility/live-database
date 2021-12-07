using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Threading;
using System;

namespace live_database
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Button p1, p2;
        GameState gt;
        TextView tv;
        bool invitee;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            p1 = FindViewById<Button>(Resource.Id.p1);
            p2 = FindViewById<Button>(Resource.Id.p2);
            tv = FindViewById<TextView>(Resource.Id.tv);
            gt = new GameState(false, Intent.GetIntExtra("room", new Random().Next()));
            tv.Text = "room: " + gt.id;
            invitee = Intent.GetBooleanExtra("invitee", false);
            if (!invitee || FirebaseHelper.Get(gt.)
            {
                await FirebaseHelper.Add(gt);
            }
            p1.Click += P1_Click;
            p2.Click += P2_Click;
            ThreadStart threadStart = new ThreadStart(timer);
            Thread t = new Thread(threadStart);
            t.Start();
        }

        private void P2_Click(object sender, System.EventArgs e)
        {
            gt.state = false;
            FirebaseHelper.Update(gt);
        }

        private void P1_Click(object sender, System.EventArgs e)
        {
            gt.state = true;
            FirebaseHelper.Update(gt);
        }

        private async void timer()
        {
            while (true)
            {
                gt = await FirebaseHelper.Get(gt.id);
                RunOnUiThread(() => {
                    if (gt.state)
                    {
                        if (invitee)
                        {
                            p2.Visibility = Android.Views.ViewStates.Visible;
                        }
                        else
                        {
                            p1.Visibility = Android.Views.ViewStates.Invisible;
                        }
                    }
                    else
                    {
                        if (invitee)
                        {
                            p2.Visibility = Android.Views.ViewStates.Invisible;
                        }
                        else
                        {
                            p1.Visibility = Android.Views.ViewStates.Visible;
                        }
                    }
                    
               });
               Thread.Sleep(100);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}