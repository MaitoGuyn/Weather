﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Data
{
    public class WeatherStatus
    {
      

        public string Status { get; set; } 

        public override string ToString()
        {
            return Status;
        }

    }
}
