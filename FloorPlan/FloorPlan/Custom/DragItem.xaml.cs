using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FloorPlan.Custom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DragItem : ContentView
    {
        public DragItem()
        {
            InitializeComponent();
        }

        public void AddDragIcon()
        {
            DragIcon.IsVisible = true;
        }

        public void RemoveDragIcon()
        {
            DragIcon.IsVisible = false;
        }
    }
}