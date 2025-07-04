﻿using CouresBookingSystem.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Entities
{
     public class Instructor : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio {  get; set; }
        public string Specialization { get; set; }
    }
}
