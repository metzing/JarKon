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
using JarKon.View.ParkingDetails;
using JarKon.View.ParkingDetails;
using GalaSoft.MvvmLight.Command;

namespace JarKon.ViewModel
{
    public class ParkingViewModel : BindableObject
    {
        //Singleton
        private static ParkingViewModel instance;
        public static ParkingViewModel Instance
        {
            get { return instance ?? (instance = new ParkingViewModel()); }
        }

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

        private Zone selectedZone;

        public Zone SelectedZone
        {
            get { return selectedZone; }
            set
            {
                selectedZone = value;
                Provider.Instance.ParkingPage.OnSelectedZoneChanged(value);
            }
        }

        public RelayCommand DisplayZoneOptionsCommand { get; private set; }
        public RelayCommand ShowZoneDetailsCommand { get; private set; }
        public RelayCommand StopParkingCommand { get; private set; }
        public RelayCommand StartParkingCommand { get; private set; }

        ParkingViewModel()
        {
            DisplayZoneOptionsCommand = new RelayCommand(() => { Provider.Instance.ParkingPage.Navigation.PushPopupAsync(new ZonesPopup()); });
            ShowZoneDetailsCommand = new RelayCommand(() => { Provider.Instance.ParkingPage.Navigation.PushPopupAsync(new ZoneDetailsPopup()); });
            StopParkingCommand = new RelayCommand(StopParking);
            StartParkingCommand = new RelayCommand(StartParking);
        }

        private void StartParking()
        {
            if (SelectedZone == null)
            {
                App.DisplayAlert("Hiba", "Kérem, válasszon egy zónát", "OK");
                return;
            }

            (Provider.Instance.ParkingPage.Content as ParkingEnabledView).BottomContent.Content = new ParkingInProgressView();
        }

        private void StopParking()
        {
            (Provider.Instance.ParkingPage.Content as ParkingEnabledView).BottomContent.Content = new ParkingStoppedView(selectedZone);
        }

        public static void OnUserLoggedIn()
        {
            if (!ParkingViewModel.Instance.CheckParkingPermission())
            {
                //User doesn't have permission for the feature, disable it
                Provider.Instance.ParkingPage.Content = new ParkingDisabledView();
            }
            else
            {
                var desiredContent = new ParkingEnabledView();
                //User has permission for the feature, enable it
                if (ParkingViewModel.Instance.IsParkingInProgress())
                {
                    desiredContent.BottomContent.Content = new ParkingInProgressView();
                }
                else
                {
                    desiredContent.BottomContent.Content = new ParkingStoppedView();
                }
                Provider.Instance.ParkingPage.Content = desiredContent;
            }
        }

        private bool IsParkingInProgress()
        {
            //Ez majd lekérhető lesz az API-tól

            //return true;
            return false;
        }

        private bool CheckParkingPermission()
        {
            //ONLY FOR DEVELOPMENT PURPOSES
            return true;

            foreach (var item in Provider.Instance.CurrentUser.settings.permissions.funtionalities)
            {
                if (item == Model.Functionality.PARKING) return true;
            }
            return false;
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
                if (Instance.SelectedZone != null) listView.SelectedItem = Instance.SelectedZone;
            }

            private void ZoneTapped(object sender, ItemTappedEventArgs e)
            {
                Instance.SelectedZone = (Zone)e.Item;
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
                    Content = new ZonesDetailView(Instance.SelectedZone, Instance.StartParkingCommand)
                };
            }
        }
    }
}
