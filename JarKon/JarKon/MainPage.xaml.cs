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
        public static IEnumerable<Vehicle> Vehicles;

		public MainPage()
		{
            Vehicles = Vehicle.GetDummyData();

            InitializeComponent();
		}
	}
}
