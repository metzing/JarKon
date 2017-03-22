using JarKon.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JarKon.ViewModel
{
    class CardsViewModel
    {
        public static ObservableCollection<VehicleState> Vehicles = GetDummyData();


        public static ObservableCollection<VehicleState> GetDummyData()
        {
            ObservableCollection<VehicleState> retu = new ObservableCollection<VehicleState> {(

            new VehicleState
            {
                vehicleId = 1,
                position = new Position{
                    lat=47.472999f,
                    lng=19.052566f},
                speed = 69,
                ignition = true,
                extBattVolt = 1,
                intBattVolt = 2,
                driver = "John Doe"
            }),

            new VehicleState
            {
                vehicleId = 2,
                position = new Position{
                    lat=47.408770f,
                    lng =19.017055f},
                speed = 420,
                ignition = false,
                intBattVolt = 3,
                extBattVolt = 4,
                driver = "Pikachu"
            },
            new VehicleState
            {
                vehicleId = 2,
                position = new Position{
                    lat=47.408770f,
                    lng =19.017055f},
                speed = 420,
                ignition = false,
                intBattVolt = 3,
                extBattVolt = 4,
                driver = "Pikachu"
            },
            new VehicleState
            {
                vehicleId = 2,
                position = new Position{
                    lat=47.408770f,
                    lng =19.017055f},
                speed = 420,
                ignition = false,
                intBattVolt = 3,
                extBattVolt = 4,
                driver = "Pikachu"
            },
            new VehicleState
            {
                vehicleId = 2,
                position = new Position{
                    lat=47.408770f,
                    lng =19.017055f},
                speed = 420,
                ignition = false,
                intBattVolt = 3,
                extBattVolt = 4,
                driver = "Pikachu"
            }
            };

            return retu;
        }

    }
}
