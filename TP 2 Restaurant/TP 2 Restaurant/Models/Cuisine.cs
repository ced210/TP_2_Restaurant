﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IRDB.Models
{
    public class Cuisine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Cuisine()
        {
            Name = "";
        }
    }
}