using JarKon.Core;
using JarKon.Model;
using JarKon.View;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using JarKon.View.Parking;

namespace JarKon.ViewModel
{
    public class ParkingViewModel
    {
        private static List<Zone> MockZones = new List<Zone>
        {
            new Zone
            {
                zoneCode = 123,
                zoneName = "Pécs",
                zoneTarif = 400
            },
            new Zone
            {
                zoneCode = 234,
                zoneName = "Budapest",
                zoneTarif = 500
            },
            new Zone
            {
                zoneCode = 345,
                zoneName = "Győr",
                zoneTarif = 600
            },
            new Zone
            {
                zoneCode = 456,
                zoneName = "Debrecen",
                zoneTarif = 700
            },
            new Zone
            {
                zoneCode = 567,
                zoneName = "Kiskunfélegyháza",
                zoneTarif = 800
            }
        };

        private static ZonesPopup zonePicker = null;

        public static ZonesPopup ZonePicker
        {
            get { return zonePicker ?? (zonePicker = new ZonesPopup()); }
        }

        private static Zone selectedZone;

        public static Zone SelectedZone
        {
            get { return selectedZone; }
            set
            {
                selectedZone = value;
                OnSelectedZoneChanged();
            }
        }

        private static Button SelectZoneButton;

        private static void OnSelectedZoneChanged()
        {
            if (selectedZone == null) return;

            SelectZoneButton.Text = "Zóna:" + selectedZone.zoneCode;
        }

        public static void OnUserLoggedIn()
        {
            if (!CheckParkingPermission())
            {
                //User doesn't have permission for the feature, disable it
                Provider.Instance.ParkingPage.Content = new View.Parking.ParkingDisabledView();
            }
            else
            {
                //User has permission for the feature, enable it
                if (IsParkingInProgress())
                {
                    Provider.Instance.ParkingPage.Content = BuildParkingInProgressView();
                }
                else
                {
                    Provider.Instance.ParkingPage.Content = BuildParkingStoppedView();
                }
            }
        }

        private static Xamarin.Forms.View BuildParkingStoppedView()
        {
            var view = new ParkingEnabledView();
            var layout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 25
            };

            layout.Children.Add(SelectZoneButton = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("1a335c"),
                TextColor = Color.White,
                Text = "Válasszon parkolási zónát...",
                Command = new DisplayZoneOptionsCommand(),
                CommandParameter = ZonePicker,
                WidthRequest = 300
            });

            layout.Children.Add(
                new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Text = "Biztosan el akarja indítani a parkolást?",
                    FontSize = 20
                });

            layout.Children.Add(
                new Button
                {
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.FromHex("1a335c"),
                    TextColor = Color.White,
                    Text = "Start",
                    Command = new StartParkingCommand(),
                    WidthRequest = 300
                });

            layout.Children.Add(
                new Button
                {
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White,
                    BorderColor = Color.FromHex("1a335c"),
                    BorderWidth = 2,
                    TextColor = Color.FromHex("1a335c"),
                    Text = "Mégse",
                    WidthRequest = 300
                });

            view.BottomContent.Content=layout;

            return view;
        }

        private static Xamarin.Forms.View BuildParkingInProgressView()
        {
            var view = new ParkingEnabledView();

            var layout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 30
            };

            layout.Children.Add(
                new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Text = "A parkolás kezdete óta eltelt idő:",
                    FontSize = 30
                });

            layout.Children.Add(
                new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Text = "1 : 45 : 48",
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 30
                });

            layout.Children.Add(
                new Button
                {
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.FromHex("1a335c"),
                    TextColor = Color.White,
                    Text = "Stop",
                    WidthRequest = 300,
                    Command = new StopParkingCommand()
                });

            view.BottomContent.Content = layout;
            return view;
        }

        private static bool IsParkingInProgress()
        {
            //Ez majd lekérhető lesz az API-tól

            //return true;
            return false;
        }

        private static bool CheckParkingPermission()
        {
            //ONLY FOR DEVELOPMENT PURPOSES
            return true;

            foreach (var item in Provider.Instance.CurrentUser.settings.permissions.funtionalities)
            {
                if (item == Model.Functionality.PARKING) return true;
            }
            return false;
        }

        public class StartParkingCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                App.DisplayAlert("Button pressed", "Start parking", "OK");
                Provider.Instance.ParkingPage.Content = BuildParkingInProgressView();
            }
        }
        private class StopParkingCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Provider.Instance.ParkingPage.Content = BuildParkingStoppedView();
            }
        }
        private class DisplayZoneOptionsCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Provider.Instance.ParkingPage.Navigation.PushPopupAsync(new ZonesPopup());
            }
        }
        public class ZonesPopup : PopupPage
        {
            private ListView listView;
            public ZonesPopup()
            {
                CloseWhenBackgroundIsClicked = true;

                Content = new ContentView
                {
                    Padding = new Thickness(30, 200),
                    Content = listView = new ListView
                    {
                        ItemsSource = MockZones,
                        BackgroundColor = Color.White
                    }
                };

                listView.ItemTapped += ZoneTapped;
                if (SelectedZone != null) listView.SelectedItem = SelectedZone;
            }

            private void ZoneTapped(object sender, ItemTappedEventArgs e)
            {
                SelectedZone = (Zone)e.Item;
                Navigation.PopPopupAsync();
                Navigation.PushPopupAsync(new ZoneDetailsPopup());
            }
        }
        public class ZoneDetailsPopup : PopupPage
        {
            public ZoneDetailsPopup()
            {
                CloseWhenBackgroundIsClicked = true;

                Content = new ContentView
                {
                    Padding = new Thickness(30, 200),
                    Content = new ZonesDetailView(SelectedZone, new StartParkingCommand())
                };
            }
        }
    }

    
}
