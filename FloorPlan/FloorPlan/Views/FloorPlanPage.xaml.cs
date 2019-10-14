using FloorPlan.Models;
using FloorPlan.Custom;
using FloorPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FloorPlan.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloorPlanPage : ContentPage
    {
        public FloorPlanPage()
        {
            InitializeComponent();

            this.BindingContext = new FloorPlanViewModel();
            devicesPlaced = new List<DeviceModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DisplayAlert("Alert", "Tap on the device item to begin placing on the floor plan", "Dismiss");
        }

        List<DeviceModel> devicesPlaced;

        private void DragView_DragEnd(object sender, EventArgs e)
        {
            var device = ((DraggableView)sender).BindingContext as DeviceModel;
            device.IsPlaced = true;

            if (!devicesPlaced.Contains(device))
            {
                device.X = ((DraggableView)sender).NewPosition.X;
                device.Y = ((DraggableView)sender).NewPosition.Y;

                devicesPlaced.Add(device);
            }

            //TODO: Save to disk
            if (App.Current.Properties.ContainsKey("devices"))
            {
                App.Current.Properties["devices"] = devicesPlaced;
            }
            else
            {
                App.Current.Properties.Add("devices", devicesPlaced);
            }
        }

        private void DevicesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var device = (DeviceModel)e.Item;

            if (ParentLayoutGrid.Children.Count > 0)
            {
                var lastUnplacedDevice = ParentLayoutGrid.Children.Where(d => d.GetType() == typeof(DraggableView))
                    .FirstOrDefault(c => ((c as DraggableView).BindingContext as DeviceModel).IsPlaced == false);

                if (lastUnplacedDevice != null)
                {
                    lastUnplacedDevice.BindingContext = device;
                }
                else
                {
                    var dragView = new DraggableView { BindingContext = device };
                    dragView.DragEnd += DragView_DragEnd;
                    ParentLayoutGrid.Children.Add(dragView, 1, 0);
                } 
            }
        }

        private void DeleteDevicesBtn_Clicked(object sender, EventArgs e)
        {
            for (int i = ParentLayoutGrid.Children.Count-1; i >= 0; i--)
            {
                if (ParentLayoutGrid.Children[i].GetType() == typeof(DraggableView))
                {
                    ParentLayoutGrid.Children.Remove(ParentLayoutGrid.Children[i]);

                    break;
                }
            }
        }
    }
}