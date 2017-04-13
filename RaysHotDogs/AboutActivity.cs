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

namespace RaysHotDogs
{
    [Activity(Label = "About Ray's Hot Dogs")]
    public class AboutActivity : Activity
    {
        private TextView phoneNumberTextView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView (Resource.Layout.AboutView);

            FindViews ();

            HandleEvents ();
        }

        private void HandleEvents ()
        {
            phoneNumberTextView.Click += PhoneNumberTextViewOnClick;
        }

        private void PhoneNumberTextViewOnClick (object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(Intent.ActionCall);
            intent.SetData (Android.Net.Uri.Parse ("tel:" + phoneNumberTextView.Text));

            StartActivity (intent);
        }

        private void FindViews ()
        {
            phoneNumberTextView = FindViewById<TextView> (Resource.Id.phoneNumberTextView);
        }
    }
}