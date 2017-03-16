using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace JarKon.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Card : ViewCell
    {
        
        #region propreties
        public static readonly BindableProperty IDProperty =
            BindableProperty.Create("ID", typeof(int), typeof(Card), 0);

        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly BindableProperty PositionProperty =
            BindableProperty.Create("Position", typeof(Position), typeof(Card), new Position());

        public Position Position
        {
            get { return (Position)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly BindableProperty LongitudeProperty =
            BindableProperty.Create("Longitude", typeof(string), typeof(Card), "");

        public string Longitude
        {
            get { return ((Position)GetValue(PositionProperty)).Longitude.ToString(); }
        }

        public static readonly BindableProperty LatitudeProperty =
            BindableProperty.Create("Latitude", typeof(double), typeof(Card), 0.0);

        public double Latitude
        {
            get { return ((Position)GetValue(PositionProperty)).Latitude; }
        }

        public static readonly BindableProperty SpeedProperty =
            BindableProperty.Create("Speed", typeof(double), typeof(Card), 0.0);

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public static readonly BindableProperty IgnitionProperty =
            BindableProperty.Create("Ignition", typeof(bool), typeof(Card), false);

        public bool Ignition
        {
            get { return (bool)GetValue(IgnitionProperty); }
            set { SetValue(IgnitionProperty, value); }
        }

        public static readonly BindableProperty InternalVoltageProperty =
                   BindableProperty.Create("InternalVoltage", typeof(double), typeof(Card), 0.0);

        public double InternalVoltage
        {
            get { return (double)GetValue(InternalVoltageProperty); }
            set { SetValue(InternalVoltageProperty, value); }
        }

        public static readonly BindableProperty ExternalVoltageProperty =
                  BindableProperty.Create("ExternalVoltage", typeof(double), typeof(Card), 0.0);

        public double ExternalVoltage
        {
            get { return (double)GetValue(ExternalVoltageProperty); }
            set { SetValue(ExternalVoltageProperty, value); }
        }

        public static readonly BindableProperty DriverNameProperty =
                  BindableProperty.Create("DriverName", typeof(string), typeof(Card), "");

        public string DriverName
        {
            get { return GetValue(DriverNameProperty) as string; }
            set { SetValue(DriverNameProperty, value); }
        }

        #endregion

        public Card()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
