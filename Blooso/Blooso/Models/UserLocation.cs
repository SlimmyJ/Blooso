﻿using System.ComponentModel.DataAnnotations;

namespace Blooso.Models
{
    public class UserLocation
    {
        public UserLocation(int areaCode = 8000) => AreaCode = areaCode;

        [Key] public int Id { get; set; }

        public int AreaCode { get; set; }
    }
}