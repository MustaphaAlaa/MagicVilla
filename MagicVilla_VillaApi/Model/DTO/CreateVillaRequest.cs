﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Model.DTO
{
    public class CreateVillaRequest
    {

        public string Name { get; set; }
        public string Details { get; set; }
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}
