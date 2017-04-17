using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using JarKon.Data;

using Xamarin.Forms;

namespace JarKon.View
{
	public class Page1 : ContentPage
	{
		public Page1 ()
		{
            
            Title = "Simple";
            Padding = new Thickness(0, 20, 0, 0);
            var listView = new ListView();
            listView.ItemsSource = DataFactory.StringList;


		
		}
	}
}
