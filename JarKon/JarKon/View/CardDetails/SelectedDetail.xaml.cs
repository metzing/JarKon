using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JarKon.ViewModel;
using Xamarin.Forms;

namespace JarKon.View.CardDetails
{
    public partial class SelectedDetail : ContentView
    {
        public static BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(SelectedDetail), "");
        public static BindableProperty ValueProperty =
             BindableProperty.Create(nameof(Value), typeof(string), typeof(SelectedDetail), "");

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public SelectedDetail()
        {
            InitializeComponent();
            BindingContext = this;
        }


        public void SetTexts(DetailText item)
        {
            Title = item.top;
            Value = item.bottom;
        }
    }
}
