using JarKon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace JarKon.View.CardDetails
{
    public partial class Card : ContentView
    {
        const int SELECTED_DETAIL_COLUMN_COUNT = 3;
        const int SELECTED_DETAIL_ROW_COUNT = 2;

        private bool IsExpanded;
        private bool IsInflated;

        public static BindableProperty DataProperty =
            BindableProperty.Create(nameof(Data), typeof(CardData), typeof(Card));

        public CardData Data
        {
            get { return (CardData)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public Card()
        {
            InitializeComponent();
            BindingContext = this;

            IsExpanded = false;
            IsInflated = false;
            ArrowImage.Source = "expand_arrow.png";

            var recognizer = new TapGestureRecognizer();
            recognizer.Tapped += ToggleExpand;
            Button.GestureRecognizers.Add(recognizer);
        }

        private void ToggleExpand(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public async void SetData(CardData data)
        {
            Data = data;
            if (IsInflated)
            {
                RefreshData(data);
                return;
            }

            IsInflated = true;

            //Inflate selected details
            for (int i = 0; i < SELECTED_DETAIL_ROW_COUNT; i++)
            {
                for (int j = 0; j < SELECTED_DETAIL_COLUMN_COUNT; j++)
                {
                    var view = new SelectedDetail();
                    view.SetTexts(data.SelectedDetails[i * 3 + j]);
                    SelectedDetailsGrid.Children.Add(view, j, i);
                }
            }

            //Inflate not selected details
            foreach (var item in data.ExpandedTextList)
            {
                var view = new NotSelectedDetail();
                view.SetTexts(item);
                NotSelectedDetails.Children.Add(view);
            }
        }

        private void RefreshData(CardData data)
        {
            for (int i = 0; i < SELECTED_DETAIL_ROW_COUNT; i++)
            {
                for (int j = 0; j < SELECTED_DETAIL_COLUMN_COUNT; j++)
                {
                    var view = SelectedDetailsGrid.Children[i * 3 + j];
                    (view as SelectedDetail).Value = data.SelectedDetails[i * 3 + j].bottom;
                }
            }

            for (int i = 0; i < data.ExpandedTextList.Count; i++)
            {
                (NotSelectedDetails.Children[i] as NotSelectedDetail).Value = data.ExpandedTextList[i].bottom;
            }
        }
    }
}
