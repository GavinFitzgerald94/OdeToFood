using System;
using System.Collections.Generic;
using System.Text;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
          IEnumerable<Restaurant> GetRestaurantsByName(string name);
    }

    public class InMemoryRestaurantData : IRestaurantData {

        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>(){
                new Restaurant{ id = 1, Name = "Gavins Kitchen", Location = "Rathmines", Cuisine = CuisineType.Italian},
                new Restaurant{ id = 2, Name = "Keralas Spot", Location = "Donegal", Cuisine = CuisineType.Indian},
                new Restaurant{ id = 3, Name = "El Diablo", Location = "Poolbeg", Cuisine = CuisineType.Mexican}
            };
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name){
            return from r in restaurants
                    where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                    orderby r.Name
                    select r;
        }
    }
}