using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JarKon.View.ParkingDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParkingEnabledView : ContentView
    {
        public ContentView BottomContent { get { return bottomContent; } }
        public ParkingEnabledView()
        {
            InitializeComponent();
        }
    }
}
