using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int Scrap = 0;
    public int      SelectedVehicle = 0;
    public bool[]   VehiclePurchased;
    public int[]    EngineLevel;
    public int[]    TiresLevel;
    public int[]    FuelLevel;
    public int[]    FrameLevel;

    public PlayerData()
    {
        Scrap = 0;
        VehiclePurchased = new bool[3];
        EngineLevel = new int[3];
        TiresLevel = new int[3];
        FuelLevel = new int[3];
        FrameLevel = new int[3];
    }

    public PlayerData (Vehicle car)
    {
        Scrap = car.Scrap;
        SelectedVehicle = car.VehicleNumber;
        VehiclePurchased = new bool[3];
        EngineLevel = new int[3];
        TiresLevel = new int[3];
        FuelLevel = new int[3];
        FrameLevel = new int[3];

        VehiclePurchased[car.VehicleNumber] = car.VehiclePurchased;
        EngineLevel[car.VehicleNumber] = car.EngineLevel;
        TiresLevel[car.VehicleNumber] = car.TiresLevel;
        FuelLevel[car.VehicleNumber] = car.FuelLevel;
        FrameLevel[car.VehicleNumber] = car.FrameLevel;
    }
}
