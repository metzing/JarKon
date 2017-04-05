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
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            InitializeComponent();
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
        public List<CustomPin> CustomPins { get; set; }

        public CustomMap()
        {
            CustomPins = new List<CustomPin>();
        }
    }

    public class CustomPin
    {
        public Pin Pin { get; set; }
        public bool isSelected;

        public CustomPin()
        {
            Pin = new Pin();
        }
    }
}
