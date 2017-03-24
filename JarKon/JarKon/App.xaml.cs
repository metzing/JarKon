﻿using JarKon.ViewModel;
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
	}
}
