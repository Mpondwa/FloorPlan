﻿using AView = Android.Views;
using Android.Runtime;
using Android.Views;
using FloorPlan.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FloorPlan;
using System.ComponentModel;
using FloorPlan.Custom;

[assembly: ExportRenderer(typeof(DraggableView), typeof(DraggableViewRenderer))]
namespace FloorPlan.Droid.Renderers
{
    public class DraggableViewRenderer : VisualElementRenderer<Xamarin.Forms.View>
    {
        float originalX;
        float originalY;
        float dX;
        float dY;
        bool firstTime = true;
        bool touchedDown = false;

        public DraggableViewRenderer(Android.Content.Context context) : base (context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

          
            if(e.OldElement!=null)
            {
                LongClick -= HandleLongClick;
            }
            if (e.NewElement != null)
            {
                LongClick += HandleLongClick;
                var dragView = Element as DraggableView;
                dragView.RestorePositionCommand = new Command(() =>
                {
                    if (!firstTime)
                    {
                        SetX(originalX);
                        SetY(originalY);
                    }
                      
                });
            }
        
        }

        private void HandleLongClick(object sender, LongClickEventArgs e)
        {
            var dragView = Element as DraggableView;
            if (firstTime)
            {
                originalX = GetX();
                originalY = GetY();
                firstTime = false;
            }
            dragView.DragStarted();
            touchedDown = true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var dragView = Element as DraggableView;
            base.OnElementPropertyChanged(sender, e);
         
        }
        protected override void OnVisibilityChanged(AView.View changedView, [GeneratedEnum] ViewStates visibility)
        {
            base.OnVisibilityChanged(changedView, visibility);
            if(visibility == ViewStates.Visible)
            {
                
               
              
            }
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            float x = e.RawX;
            float y = e.RawY;
            var dragView = Element as DraggableView;
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    if (!touchedDown)
                    {
                        if (firstTime)
                        {
                            originalX = GetX();
                            originalY = GetY();
                            firstTime = false;
                        }
                        dragView.DragStarted();
                    }
                      
                        touchedDown = true;
                    dX = x - this.GetX();
                    dY = y - this.GetY();
                    break;
                case MotionEventActions.Move:
                    if (touchedDown)
                    {
                        SetX(x - dX);
                        SetY(y - dY);
                    }
                    break;
                case MotionEventActions.Up:
                    touchedDown = false;
                    dragView.DragEnded(new Point { X = GetX(), Y = GetY() });
                    break;
                case MotionEventActions.Cancel:
                    touchedDown = false;
                    break;
            }
            return base.OnTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent e)
        {
            BringToFront();
            return true;
        }
        
    }
    

}