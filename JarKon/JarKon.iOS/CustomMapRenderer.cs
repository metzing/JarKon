using JarKon.iOS;
using JarKon.View;
using MapKit;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using System;
using CoreGraphics;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace JarKon.iOS
{
    class CustomMapRenderer : MapRenderer
    {
        UIView customPinView;
        List<CustomPin> customPins;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = null;
                nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
            }

            if(e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                customPins = formsMap.Pins;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if(annotation is MKUserLocation)
            {
                return null;
            }

            var anno = annotation;
            var customPin = GetCustomPin(anno);
            if(customPin == null)
            {
                //throw new Exception("Custom pin not found");
                return null;
            }

            annotationView = mapView.DequeueReusableAnnotation(customPin.Pin.Label);
            if(annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, customPin.Pin.Label);
                annotationView.Image = UIImage.FromFile("vehicle_bubble_13_green.png");
                annotationView.CalloutOffset = new CoreGraphics.CGPoint(0, 0);
                annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
            }
            annotationView.CanShowCallout = true;

            return annotationView;
        }

        private CustomPin GetCustomPin(IMKAnnotation anno)
        {
            return customPins.Find(p => locationPeredicate(anno,p));
        }

        private bool locationPeredicate(IMKAnnotation anno, CustomPin p)
        {
            return Math.Abs(anno.Coordinate.Latitude - p.Pin.Position.Latitude) < 0.01f &&
                   Math.Abs(anno.Coordinate.Longitude - p.Pin.Position.Longitude) < 0.01f;
        }

        private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }

        private void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;

            customPinView = new UIView();
            customPinView.Frame = new CGRect(0, 0, 200, 84);

            var image = new UIImageView(new CGRect(0, 0, 200, 84));
            image.Image = UIImage.FromFile("vehicle_bubble_13_green.png");

            customPinView.AddSubview(image);
            customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
            e.View.AddSubview(customPinView);
        }

        private void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
        }
    }

    internal class CustomMKAnnotationView : MKAnnotationView
    {
        public string ID;

        public CustomMKAnnotationView(IMKAnnotation annotation, string reuseIdentifier) : base(annotation, reuseIdentifier)
        {
            ID = reuseIdentifier;
        }
    }
}