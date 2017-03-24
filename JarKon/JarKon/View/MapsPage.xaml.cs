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
	public partial class MapsPage : ContentPage
	{
		public MapsPage ()
		{
			InitializeComponent ();
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
