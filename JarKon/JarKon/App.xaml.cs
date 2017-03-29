using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace JarKon
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new JarKon.View.MainPage();
		}

		protected override void OnStart ()
		{
            MapsPageViewModel mapsVM = new MapsPageViewModel();
            mapsVM.LoadPins();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public class ListDataViewCell : ViewCell
        {
            public ListDataViewCell()
            {
                var label = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Default),
                    TextColor = Color.Blue
                };
                label.SetBinding(Label.TextProperty, new Binding("TextValue"));
                label.SetBinding(Label.ClassIdProperty, new Binding("DataValue"));
                View = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Padding = new Thickness(12, 8),
                    Children = { label }
                };
            }
        }

      
    }
}
