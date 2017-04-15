using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RaysHotDogs
{
    [Activity(Label = "Visit Ray's Store")]
    public class RayMapActivity : Activity
    {
        private Button externalMapButton;
        private FrameLayout mapFrameLayout;
        private MapFragment mapFragment;
        private GoogleMap googleMap;
        private LatLng rayLocation;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            rayLocation = new LatLng (50.846704, 4.3524460);

            SetContentView (Resource.Layout.RayMapView);
            // Create your application here

            FindViews ();

            HandleEvents ();

            CreateMapFragment ();

            UpdateMapView ();
        }

        private void UpdateMapView ()
        {
            var mapReadyCallback = new LocalMapReady ();

            mapReadyCallback.MapReady += (sender, args) =>
            {
                googleMap = (sender as LocalMapReady).Map;

                if (googleMap != null)
                {
                    MarkerOptions markerOptions = new MarkerOptions ();
                    markerOptions.SetPosition (rayLocation);
                    markerOptions.SetTitle ("Ray's Hot Dogs");
                    googleMap.AddMarker (markerOptions);

                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom (rayLocation, 15);
                    googleMap.MoveCamera (cameraUpdate);
                }
            };

            mapFragment.GetMapAsync (mapReadyCallback);
        }

        private void CreateMapFragment ()
        {
            mapFragment = FragmentManager.FindFragmentByTag ("map") 
                as MapFragment;

            if (mapFragment == null)
            {
                var googleMapOptions = new GoogleMapOptions ()
                    .InvokeMapType (GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled (true)
                    .InvokeCompassEnabled (true)
                    ;

                FragmentTransaction fragmentTransaction =
                    FragmentManager.BeginTransaction ();

                mapFragment = MapFragment.NewInstance (googleMapOptions);

                fragmentTransaction.Add (Resource.Id.mapFrameLayout, mapFragment, "map");

                fragmentTransaction.Commit ();

            }
        }

        private void FindViews ()
        {
            externalMapButton = FindViewById<Button> (Resource.Id.externalMapButton);
        }

        private void HandleEvents ()
        {
            externalMapButton.Click += ExternalMapButtonOnClick;
        }

        private void ExternalMapButtonOnClick (object sender, EventArgs eventArgs)
        {
            Android.Net.Uri rayLocationUri =
                Android.Net.Uri.Parse ("geo:50.846704, 4.3524460");

            Intent mapIntent = new Intent(Intent.ActionView, rayLocationUri);
            StartActivity (mapIntent);
        }
    }

    internal class LocalMapReady : Java.Lang.Object, IOnMapReadyCallback
    {
        public GoogleMap Map { get; set; }
        public event EventHandler MapReady;

        public void OnMapReady (GoogleMap googleMap)
        {
            Map = googleMap;
            var handler = MapReady;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}