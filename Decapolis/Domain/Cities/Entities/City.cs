using System;
using Domain.Cities.Enums;

namespace Domain.Cities.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Decapolis Decapolis { get; set; }
    }
}