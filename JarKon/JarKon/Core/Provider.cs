using JarKon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarKon.Core
{
    public class Provider
    {
        public List<Vehicle> Vehicles { get; private set; }
        public List<VehicleState> VehicleStates { get; private set; }
        public User CurrentUser { get; set; }

        public Provider()
        {
            Vehicles = new List<Vehicle>{
                new Vehicle
                {
                    vehicleId = 0,
                    plateNumber = "ASD-123",
                    make = "make",
                    type = "type"
                },
                new Vehicle
                {
                    vehicleId = 1,
                    plateNumber = "ASD-234",
                    make = "make",
                    type = "type"
                },
                new Vehicle
                {
                    vehicleId = 2,
                    plateNumber = "ASD-345",
                    make = "make",
                    type = "type"
                },
                new Vehicle
                {
                    vehicleId = 3,
                    plateNumber = "ASD-456",
                    make = "make",
                    type = "type"
                },
                new Vehicle
                {
                    vehicleId = 4,
                    plateNumber = "ASD-567",
                    make = "make",
                    type = "type"
                }

            };
            VehicleStates = new List<VehicleState>
            {
                new VehicleState
                {
                    vehicleId = 0,
                    position = new Position
                    {
                        lat = 47.472999f,
                        lng = 19.052566f
                    },
                    speed = 69,
                    ignition = true,
                    extBattVolt = 1,
                    intBattVolt = 2,
                    driver = "Squirtle"
                },

                new VehicleState
                {
                    vehicleId = 1,
                    position = new Position
                    {
                        lat = 47.408770f,
                        lng = 19.017055f
                    },
                    speed = 420,
                    ignition = false,
                    intBattVolt = 3,
                    extBattVolt = 4,
                    driver = "Pikachu"
                },
                new VehicleState
                {
                    vehicleId = 2,
                    position = new Position
                    {
                        lat = 47.466183f,
                        lng = 19.007558f
                    },
                    speed = 420,
                    ignition = false,
                    intBattVolt = 3,
                    extBattVolt = 4,
                    driver = "Mew"
                },
                new VehicleState
                {
                    vehicleId = 3,
                    position = new Position
                    {
                        lat = 47.541071f,
                        lng = 19.043243f
                    },
                    speed = 420,
                    ignition = false,
                    intBattVolt = 3,
                    extBattVolt = 4,
                    driver = "Mewtwo"
                },
                new VehicleState
                {
                    vehicleId = 4,
                    position = new Position
                    {
                        lat = 46.032752f,
                        lng = 18.155835f
                    },
                    speed = 420,
                    ignition = false,
                    intBattVolt = 3,
                    extBattVolt = 4,
                    driver = "Charmander"
                }
            };
        }
    }
}
