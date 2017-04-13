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
using RaysHotDogs.Adapters;
using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Service;
using RaysHotDogs.Fragments;

namespace RaysHotDogs
{
    [Activity(Label = "HotDogMenuActivity", MainLauncher = false)]
    public class HotDogMenuActivity : Activity
    {
        private ListView hotDogListView;
        private List<HotDog> allHotDogs;
        private HotDogDataService hotDogDataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView (Resource.Layout.HotDogMenuView);


            //Specifying that I want to use tabs navigation mode for action bar.
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab ("Favorites", new FavouriteHotDogFragment ());
            AddTab ("Meat Lovers", new MeatLoversFragment ());
            AddTab ("Veggi Lovers", new VeggieLoversFragment ());

            //hotDogListView = FindViewById<ListView>(Resource.Id.hotDogListView);

            //hotDogDataService = new HotDogDataService ();

            //allHotDogs = hotDogDataService.GetAllHotDogs ();

            //hotDogListView.Adapter = new HotDogListAdapter (this, allHotDogs);

            //hotDogListView.FastScrollEnabled = true;

            //hotDogListView.ItemClick += HotDogListViewOnItemClick;
        }

        private void AddTab (string tabText, Fragment view)
        {
            var tab = this.ActionBar.NewTab ();
            tab.SetText (tabText);
            //tab.SetIcon (iconRecordId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs args)
            {
                var fragment = this.FragmentManager.FindFragmentById (Resource.Id.fragmentContainer);
                if (fragment != null)
                {
                    args.FragmentTransaction.Remove (fragment);
                }
                args.FragmentTransaction.Add (Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs args)
            {
                args.FragmentTransaction.Remove (view);
            };

            this.ActionBar.AddTab (tab);

        }



        private void HotDogListViewOnItemClick (object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            var hotDog = allHotDogs[itemClickEventArgs.Position];

            var intent = new Intent();
            intent.SetClass (this, typeof(HotDogDetailActivity));
            intent.PutExtra ("selectedHotDogId", hotDog.HotDogId);

            StartActivityForResult (intent, 100);
        }

        protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult (requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                var selectedHotDog = hotDogDataService.GetHotDogById (data.GetIntExtra ("selectedHotDogId", 0));

                var dialog = new AlertDialog.Builder (this);
                dialog.SetTitle ("Confirmation");
                dialog.SetMessage (string.Format ("You've added {0} time(s) of the {1}", data.GetIntExtra ("amount", 0), selectedHotDog.Name));
                dialog.Show ();
            }
        }
    }
}