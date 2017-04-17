using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace JarKon.View.ParkingDetails
{
	public partial class ParkingInProgressView : ContentView
	{
		public ParkingInProgressView ()
		{
			InitializeComponent ();
            BindingContext = ParkingViewModel.Instance;
            CounterLabel.Text = "1 : 64 : 72";
		}
	}
}
