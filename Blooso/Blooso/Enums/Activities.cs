﻿using Blooso.Models;
using System.Collections.Generic;

namespace Blooso
{
    public class Activity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Activity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public ICollection<User> Users { get; set; }
    }

    public enum Activities
    {
        Unknown = 0,
        Running = 1,
        Walking = 2,
        Tennis = 3,
        Golf = 4,
        Padel = 5,
        Minigolf = 6,
        Camping = 7,
        Basketball = 8,
        Cycling = 9,
        Handball = 10,
        Climbing = 11,
        Squash = 12,
        Fitness = 13,
        Sauna = 14,
        Lacrosse = 15,
        Polo = 16,
        Football = 17,
        Yoga = 18,
        Dancing = 19,
        Pilates = 20
    }
}