﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Model.DTO
{
    public class CreateVillaNumberRequest
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }


        public string SpecialDetails { get; set; }


    }
}