﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentMeal.Domain {
    public class Meal {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public Student Cook { get; set; }

        public byte MaxGuests { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [Required]
        public List<Student> Guests { get; set; } = new List<Student>();

        public int GuestCount {
            get {
                return Guests.Count;
            }
        }
    }
}
