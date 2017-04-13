using JarKon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JarKon.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParkingPage : ContentPage
	{
		public ParkingPage ()
		{
			InitializeComponent ();
            Provider.Instance.ParkingPage = this;
		}
	}
}
