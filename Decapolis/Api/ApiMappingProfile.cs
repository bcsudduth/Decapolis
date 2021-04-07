using AutoMapper;
using Api.Models;
using Application.Cities.Queries.GetCitiesInDecapolis;
using Domain.Cities.Entities;

namespace Decapolis
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<GetCitiesInDecapolisQueryModel, GetCitiesInDecapolisQuery>();
            CreateMap<City, CityModel>();
        }
    }
}