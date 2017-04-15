using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RaysHotDogs.Core.Model;

namespace RaysHotDogs.Core.Repository
{
    public class HotDogRepository
    {
        private static List<HotDodGroup> hotDodGroups = new List<HotDodGroup> ();

         string url = 
            "http://gillcleerenpluralsight.blob.core.windows.net/files/hotdogs.json";

        public HotDogRepository ()
        {
            Task.Run (() => this.LoadDataAsync (url)).Wait ();
        }

        private async Task LoadDataAsync (string uri)
        {
            if (hotDodGroups != null)
            {
                string responseJsonString = null;

                using (var httpClient = new HttpClient ())
                {
                    try
                    {
                        Task<HttpResponseMessage> getResponse = httpClient.GetAsync (uri);

                        HttpResponseMessage response = await getResponse;

                        responseJsonString = await response.Content.ReadAsStringAsync ();

                        hotDodGroups = JsonConvert.DeserializeObject<List<HotDodGroup>> (responseJsonString);
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                }
            }
        }

        public List<HotDog> GetAllHotDogs ()
        {
            IEnumerable<HotDog> hotDogs = hotDodGroups
                .SelectMany (h => h.HotDogs)
                .ToList ();

            return hotDogs.ToList ();
        }

        public HotDog GetHotDogById (int hotDogId)
        {
            HotDog hotDog = hotDodGroups
                .SelectMany (h => h.HotDogs)
                .SingleOrDefault(h => h.HotDogId == hotDogId)
                ;

            return hotDog;
        }

        public List<HotDodGroup> GetGroupedHotDogs ()
        {
            return hotDodGroups;
        }

        public List<HotDog> GetHotDogsForGroup (int hotDogGroupId)
        {
            var group = hotDodGroups
                .Where (h => h.HotDogGroupId == hotDogGroupId)
                //.ToList ()
                ;

            if (group != null)
            {
                return group.SelectMany (g => g.HotDogs).ToList();
            }

            return null;
        }

        public List<HotDog> GetFavoritehHotDogs ()
        {
            IEnumerable<HotDog> hotDogs = hotDodGroups
                .SelectMany (g => g.HotDogs)
                .Where (h => h.IsFavorite)
                .ToList ();

            return hotDogs.ToList();
        }
    }
}
