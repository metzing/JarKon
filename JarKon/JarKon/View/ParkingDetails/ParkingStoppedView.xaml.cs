using JarKon.Model;
using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace JarKon.View.ParkingDetails
{
    public partial class ParkingStoppedView : ContentView
    {
        public ParkingStoppedView()
        {
            InitializeComponent();
            BindingContext = ParkingViewModel.Instance;
        }

        public ParkingStoppedView(Zone selectedZone)
        {
            InitializeComponent();
            BindingContext = ParkingViewModel.Instance;

            OnSelectedZoneChanged(selectedZone);
        }

        public void OnSelectedZoneChanged(Zone selectedZone)
        {
            if (selectedZone == null) return;

            SelectZoneButton.Text = "Zóna: " + selectedZone.zoneCode;
        }
    }
}
