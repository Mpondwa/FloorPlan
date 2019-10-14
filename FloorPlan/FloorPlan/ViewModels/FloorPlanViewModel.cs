using FloorPlan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FloorPlan.ViewModels
{
    public class FloorPlanViewModel : BaseViewModel
    {
        public FloorPlanViewModel()
        {
            Devices = new ObservableCollection<DeviceModel>(GetDevices());
        }

        public ObservableCollection<DeviceModel> Devices { get; set; }

        public List<DeviceModel> GetDevices()
        {
            var devices = new List<DeviceModel>();

            devices.Add(new DeviceModel { Id = "1", Name = "Fan Coil Unit", Image = "fan_coil.png" });
            devices.Add(new DeviceModel { Id = "2", Name = "Diffuser", Image = "diffuser.png" });
            devices.Add(new DeviceModel { Id = "3", Name = "Fan", Image = "fan.png" });
            devices.Add(new DeviceModel { Id = "4", Name = "Water Meter", Image = "water_meter.png" });
            devices.Add(new DeviceModel { Id = "5", Name = "Power Meter", Image = "power_meter.png" });

            return devices;
        }
    }
}
