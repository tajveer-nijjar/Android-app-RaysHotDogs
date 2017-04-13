using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Service;

namespace RaysHotDogs.Fragments
{
    public class BaseFragment : Fragment
    {
        protected ListView listView;
        protected HotDogDataService hotDogDataService;
        protected List<HotDog> hotdogs;

        public BaseFragment ()
        {
            hotDogDataService = new HotDogDataService ();
        }
        
        protected void FindViews ()
        {
            listView = this.View.FindViewById<ListView> (Resource.Id.hotDogListView);
        }

        protected void HandleEvents()
        {
            listView.ItemClick += ListViewOnItemClick;
        }


        private void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            var hotDog = hotdogs[itemClickEventArgs.Position];

            var intent = new Intent();
            intent.SetClass (this.Activity, typeof(HotDogDetailActivity));
            intent.PutExtra ("selectedHotDogId", hotDog.HotDogId);

            StartActivityForResult (intent, 100);
        }



    }
}