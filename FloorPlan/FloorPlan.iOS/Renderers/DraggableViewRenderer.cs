using Foundation;
using UIKit;
using Xamarin.Forms;
using DragViewSample;
using DragViewSample.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;
using FloorPlan.Custom;

[assembly: ExportRenderer(typeof(DraggableView), typeof(DraggableViewRenderer))]
namespace DragViewSample.iOS.Renderers
{
    
    public class DraggableViewRenderer : VisualElementRenderer<View>
    {
        //bool longPress = false;
        bool firstTime = true;
        UIPanGestureRecognizer panGesture;
        CGPoint lastLocation;
        CGPoint originalPosition;
        UIGestureRecognizer.Token panGestureToken;
        void DetectPan()
        {
            var dragView = Element as DraggableView;

            if (panGesture.State == UIGestureRecognizerState.Began)
            {
                dragView.DragStarted();
                if (firstTime)
                {
                    originalPosition = Center;
                    firstTime = false;
                }

                CGPoint translation = panGesture.TranslationInView(Superview);
                var currentCenterX = Center.X;
                var currentCenterY = Center.Y;
                currentCenterX = lastLocation.X + translation.X;

                currentCenterY = lastLocation.Y + translation.Y;

                Center = new CGPoint(currentCenterX, currentCenterY);

                if (panGesture.State == UIGestureRecognizerState.Ended)
                {
                    dragView.DragEnded(new Point { X = Center.X, Y = Center.Y });
                }
            }
        }
 
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement!=null)
            {
                RemoveGestureRecognizer(panGesture);
                panGesture.RemoveTarget(panGestureToken);
            }
            if (e.NewElement != null)
            {
                var dragView = Element as DraggableView;
                panGesture = new UIPanGestureRecognizer();
                panGestureToken =panGesture.AddTarget(DetectPan);
                AddGestureRecognizer(panGesture);


                dragView.RestorePositionCommand = new Command(() =>
                {
                    if(!firstTime)
                    {
                        Center = originalPosition;
                    }
                });

            }
          
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var dragView = Element as DraggableView;
            base.OnElementPropertyChanged(sender, e);
            
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            Superview.BringSubviewToFront(this);
            lastLocation = Center;
        }
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
        }
    }
}