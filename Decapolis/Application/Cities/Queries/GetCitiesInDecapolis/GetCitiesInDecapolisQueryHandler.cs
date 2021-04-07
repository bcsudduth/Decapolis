using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Domain.Cities.Entities;
using Domain.Cities.Enums;

namespace Application.Cities.Queries.GetCitiesInDecapolis
{
    public class GetCitiesInDecapolisQueryHandler : AbstractQueryHandler<GetCitiesInDecapolisQuery, IList<City>>
    {
        private List<City> _cities = new List<City>();
        public GetCitiesInDecapolisQueryHandler()
        {
            //Hack for now to get data from a list as opposed to a database
            FillCities();
        }
        public override async Task<IList<City>> Handle(GetCitiesInDecapolisQuery request, CancellationToken cancellationToken)
        {
            return _cities
                .Where(c => c.Decapolis == request.Decapolis).ToList();
        }

        private void FillCities()
        {
            string[] GREEK_DECAPOLIS = 
            {
                "Abila",
                "Capitolis",
                "Gadara",
                "Gerasa",
                "Hippos",
                "Konata",
                "Pella",
                "Philadelphia",
                "Philoterio",
                "Scythopolis"
            };
            
            string[] MOST_POPULATED_US_CITIES =
            {
                "New York",
                "Los Angeles",
                "Chicago",
                "Houston",
                "Phoenix",
                "Philadelphia",
                "San Antonio",
                "San Diego",
                "Dallas",
                "San Jose"
            };

            City city;
            
            foreach (string ancientGreekCity in GREEK_DECAPOLIS)
            {
                city = new City();
                city.Id = Guid.NewGuid();
                city.Name = ancientGreekCity;
                city.Decapolis = Decapolis.AncientGreece;
                _cities.Add(city);
            }

            foreach (string largeCity in MOST_POPULATED_US_CITIES)
            {
                city = new City();
                city.Id = Guid.NewGuid();
                city.Name = largeCity;
                city.Decapolis = Decapolis.UsaLargest;
                _cities.Add(city);
            }

        }

    }
}