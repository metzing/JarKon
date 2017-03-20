using JarKon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarKon.ViewModel
{
    class CardsViewModel
    {
        public static List<Vehicle> Vehicles = GetDummyData();


        public static List<Vehicle> GetDummyData()
        {
            List<Vehicle> retu = new List<Vehicle> {(

            new Vehicle
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

            new Vehicle
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
            new Vehicle
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
            new Vehicle
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
            new Vehicle
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
