using JarKon.Core;
using JarKon.Model;
using JarKon.ViewModel;
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
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            Provider.Instance.MapsPage = this;
            BindingContext = MapViewModel.Instance;
        }

        public CustomMap Map
        {
            get
            {
                return map;
            }
        }
    }

    public class CustomMap : Map
    {
        public new List<CustomPin> Pins { get; set; }

        public CustomMap()
        {
            Pins = new List<CustomPin>();
        }
    }

    public class CustomPin
    {
        public Pin Pin { get; set; }
        public Vehicle Vehicle { get; set; }
        public int Id { get { return Vehicle.vehicleId; } }

        public CustomPin()
        {
            Pin = new Pin();
        }
    }
}
