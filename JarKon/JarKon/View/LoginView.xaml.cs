﻿using JarKon.Core;
using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace JarKon.View
{
	public partial class LoginView : ContentView
	{
		public LoginView ()
		{
			InitializeComponent ();
            BindingContext = LoginManager.Instance;
		}
	}
}
