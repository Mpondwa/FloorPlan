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
    public class PanContainer : ContentView
    {

    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloorPlanPage : ContentPage
    {
        public FloorPlanPage()
        {
            InitializeComponent();

            this.BindingContext = new FloorPlanViewModel();
        }

        private void DevicesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DragSurface.IsVisible = true;
        }

        private void DragView_DragEnd(object sender, EventArgs e)
        {
            var draggableDevice = (Renderers.DraggableView)sender;

            var floorBounds = new Point { X = FloorPlanArea.X, Y = FloorPlanArea.Y };
        }
    }
}