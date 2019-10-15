using FloorPlan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FloorPlan.ViewModels
{
    public class FloorPlanViewModel
    {
        public FloorPlanViewModel()
        {
            Devices = new ObservableCollection<DeviceModel>(GetDevices());

            OpenFloorDevicesCommand = new Xamarin.Forms.Command(OpenFloorDevices);
        }

        private void OpenFloorDevices(object obj)
        {
            if (App.Current.Properties.ContainsKey("devices"))
            {
                List<Models.DeviceModel> placedDevices = App.Current.Properties["devices"] as List<Models.DeviceModel>;
            }
            
            //TODO: Render saved floor plan devices on screen
        }

        public ICommand OpenFloorDevicesCommand { get; set; }

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
