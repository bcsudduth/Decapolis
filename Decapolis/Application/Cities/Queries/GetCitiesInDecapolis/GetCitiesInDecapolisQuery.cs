using System.Collections.Generic;
using Domain.Cities.Entities;
using Domain.Cities.Enums;
using MediatR;

namespace Application.Cities.Queries.GetCitiesInDecapolis
{
    public class GetCitiesInDecapolisQuery : IRequest<IList<City>>
    {
        public Decapolis Decapolis { get; set; }
    }
}