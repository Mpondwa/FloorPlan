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

            //PanGestureRecognizer panningGesture = new PanGestureRecognizer();
            //panningGesture.PanUpdated += PanningGesture_PanUpdated;
            //DevicesList.GestureRecognizers.Add(panningGesture);

        }

        Image panImage = null;

        private void PanningGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var view = ((VisualElement)sender);
            var xPosition = view.X;
            var yPosition = view.Y;
            var imageData = (Models.DeviceModel)view.BindingContext;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    AbsoluteFloor.IsVisible = true;

                    while (view != null)
                    {
                        xPosition += view.X;
                        yPosition += view.Y;

                        if (view.Parent.GetType() == typeof(App))
                            view = null;
                        else
                            view = (VisualElement)view.Parent;
                    }

                    panImage = new Image() { Source = imageData.Image };
                    panImage.BindingContext = imageData;
                    panImage.HeightRequest = 40;
                    panImage.WidthRequest = 40;

                    //AbsoluteFloor.Children.Add(panImage,
                    //    new Rectangle { X = xPosition, Y = yPosition, Size = new Size { Height = 40, Width = 40 } },
                    //    AbsoluteLayoutFlags.PositionProportional);
                    break;
                case GestureStatus.Running:

                    while (view != null)
                    {
                        xPosition += view.X;
                        yPosition += view.Y;

                        if (view.Parent.GetType() == typeof(App))
                            view = null;
                        else
                            view = (VisualElement)view.Parent;
                    }

                    //AbsoluteLayout.SetLayoutBounds(panImage, new Rectangle { X = xPosition, Y = yPosition, Size = new Size { Height = 40, Width = 40 } });
                    break;
                case GestureStatus.Completed:
                    AbsoluteFloor.IsVisible = false;
                    break;
                case GestureStatus.Canceled:
                    AbsoluteFloor.IsVisible = false;
                    break;
                default:
                    AbsoluteFloor.IsVisible = false;
                    break;
            }
        }

        private void Image_Focused(object sender, FocusEventArgs e)
        {

        }

        private void DevicesList_Focused(object sender, FocusEventArgs e)
        {

        }
    }
}