using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using JarKon.Model;
using Xamarin.Forms;
using JarKon.ViewModel;
using static JarKon.ViewModel.ParkingViewModel;

namespace JarKon.View.Parking
{
	public partial class ZonesDetailView : ContentView
	{
        private Zone selectedZone;
        private StartParkingCommand startParkingCommand;

        public ZonesDetailView(Zone selectedZone, StartParkingCommand startParkingCommand)
        {
            InitializeComponent();

            CodeLabel.Text = selectedZone.zoneCode.ToString();
            NameLabel.Text = selectedZone.zoneName;
            TarifLabel.Text = selectedZone.zoneTarif + " Ft / óra";

            this.startParkingCommand = startParkingCommand;
        }

        private void OKButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
            startParkingCommand.Execute(null);
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}
