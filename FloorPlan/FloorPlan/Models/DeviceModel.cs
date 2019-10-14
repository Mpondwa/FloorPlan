using System;
using System.Collections.Generic;
using System.Text;

namespace FloorPlan.Models
{
    public class DeviceModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Image { get; set; }

        public bool IsPlaced { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    }
}
