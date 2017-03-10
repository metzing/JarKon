using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace JarKon
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
            get { return (Position)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly BindableProperty SpeedProperty =
            BindableProperty.Create("Speed", typeof(float), typeof(Card), 0.0f);

        public float Speed
        {
            get { return (float)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly BindableProperty IgnitionProperty =
            BindableProperty.Create("Ignition", typeof(bool), typeof(Card), false);

        public bool Ignition
        {
            get { return (bool)GetValue(IgnitionProperty); }
            set { SetValue(IgnitionProperty, value); }
        }

        public static readonly BindableProperty InternalVoltageProperty =
                   BindableProperty.Create("InternalVoltage", typeof(float), typeof(Card), 0.0f);

        public float InternalVoltage
        {
            get { return (float)GetValue(InternalVoltageProperty); }
            set { SetValue(InternalVoltageProperty, value); }
        }

        public static readonly BindableProperty ExternalVoltageProperty =
                  BindableProperty.Create("ExternalVoltage", typeof(float), typeof(Card), 0.0f);

        public float ExternalVoltage
        {
            get { return (float)GetValue(ExternalVoltageProperty); }
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
    }
}
