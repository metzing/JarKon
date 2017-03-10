using JarKon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JarKon
{
	public partial class MainPage : TabbedPage
	{
        public static List<Vehicle> Vehicles;

		public MainPage()
		{
			InitializeComponent();
            Vehicles = Vehicle.GetDummyData();
		}
	}
}
