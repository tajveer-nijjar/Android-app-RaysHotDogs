﻿using System;
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
    [Activity(Label = "Rays Hot Dogs", MainLauncher = true, Icon = "@drawable/icon")]
    public class MenuActivity : Activity
    {
        private Button orderButton;
        private Button cartButton;
        private Button aboutButton;
        private Button mapButton;
        private Button takePictureButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView (Resource.Layout.MainMenu);

            FindViews();

            HandleEvents ();
        }

        private void FindViews ()
        {
            orderButton = FindViewById<Button> (Resource.Id.orderButton);
            cartButton = FindViewById<Button>(Resource.Id.cartButton);
            aboutButton = FindViewById<Button>(Resource.Id.aboutButton);
            mapButton = FindViewById<Button>(Resource.Id.mapButton);
            takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
        }

        private void HandleEvents ()
        {
            orderButton.Click += OrderButtonOnClick;
            aboutButton.Click += AboutButtonOnClick;
            takePictureButton.Click += TakePictureButtonOnClick;
            mapButton.Click += MapButtonOnClick;
        }

        private void MapButtonOnClick (object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(RayMapActivity));
            StartActivity (intent);
        }

        private void TakePictureButtonOnClick (object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(TakePictureActivity));
            StartActivity (intent);
        }


        private void AboutButtonOnClick (object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(AboutActivity));
            StartActivity (intent);
        }

        private void OrderButtonOnClick (object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(HotDogMenuActivity));
            StartActivity (intent);

        }
    }
}