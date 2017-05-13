using JarKon.Core;
using JarKon.Model;
using JarKon.ViewModel;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
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
            Map.PinClicked += Map_PinClicked;
            BindingContext = MapViewModel.Instance;
        }

        private void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            Provider.Instance.MapsPage.Navigation.PushPopupAsync(new CardPopup(Provider.Instance.Vehicles.Find(a => a.plateNumber == e.Pin.Label)));
        }

        public Map Map
        {
            get
            {
                return map;
            }
        }
    }
}
