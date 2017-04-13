using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaysHotDogs.Core.Model;

namespace RaysHotDogs.Core.Repository
{
    public class HotDogRepository
    {
        private static List<HotDodGroup> hotDodGroups = new List<HotDodGroup> ()
        {
            new HotDodGroup ()
            {
                HotDogGroupId = 1,
                Title = "Meat Lovers",
                ImagePath = "",
                HotDogs = new List<HotDog> ()
                {
                    new HotDog ()
                    {
                        HotDogId = 1,
                        Name = "Regular Hot Dog",
                        ShortDescription = "The best one",
                        Description = "Smelly cheese with danish touch",
                        ImagePath = "hotdog1",
                        IsAvailable = true,
                        PrepTime = 10,
                        Ingredients = new List<string> () {"Regular bun", "Saucage", "Ketchup" },
                        Price = 2,
                        IsFavorite = true
                    },
                    new HotDog ()
                    {
                        HotDogId = 2,
                        Name = "HauteHot Dog",
                        ShortDescription = "The classic one",
                        Description = "Bacon and shit",
                        ImagePath = "hotdog2",
                        IsAvailable = true,
                        PrepTime = 15,
                        Ingredients = new List<string> () {"Regular bun", "Saucage", "Ketchup" },
                        Price = 3,
                        IsFavorite = false
                    },
                    new HotDog ()
                    {
                        HotDogId = 3,
                        Name = "Extra Long Hot Dog",
                        ShortDescription = "For big tummy",
                        Description = "Smelly cheese with danish touch",
                        ImagePath = "hotdog3",
                        IsAvailable = true,
                        PrepTime = 10,
                        Ingredients = new List<string> () {"Regular bun", "Saucage", "Ketchup" },
                        Price = 4,
                        IsFavorite = true
                    }
                }
            },
            new HotDodGroup ()
            {
                HotDogGroupId = 2,
                Title = "Veggie Lovers",
                ImagePath = "",
                HotDogs = new List<HotDog> ()
                {
                    new HotDog ()
                    {
                        HotDogId = 4,
                        Name = "Veggie Regular Hot Dog",
                        ShortDescription = "The best one",
                        Description = "Smelly cheese with danish touch",
                        ImagePath = "hotdog1",
                        IsAvailable = true,
                        PrepTime = 10,
                        Ingredients = new List<string> () {"Regular bun", "Saucage", "Ketchup" },
                        Price = 2,
                        IsFavorite = true
                    },
                    new HotDog ()
                    {
                        HotDogId = 5,
                        Name = "Vegie HauteHot Dog",
                        ShortDescription = "The classic one",
                        Description = "Bacon and shit",
                        ImagePath = "hotdog2",
                        IsAvailable = true,
                        PrepTime = 15,
                        Ingredients = new List<string> () {"Regular bun", "Saucage", "Ketchup" },
                        Price = 3,
                        IsFavorite = false
                    },
                    new HotDog ()
                    {
                        HotDogId = 6,
                        Name = "Vegie Extra Long Hot Dog",
                        ShortDescription = "For big tummy",
                        Description = "Smelly cheese with danish touch",
                        ImagePath = "hotdog3",
                        IsAvailable = true,
                        PrepTime = 10,
                        Ingredients = new List<string> () {"Regular bun", "Saucage", "Ketchup" },
                        Price = 4,
                        IsFavorite = true
                    }
                }
            }
        };

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
