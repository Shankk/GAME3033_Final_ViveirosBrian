using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int Scrap;
    public int EngineLevel;
    public int TiresLevel;
    public int FuelLevel;
    public int FrameLevel;

    public PlayerData (Car car)
    {
        Scrap = car.Scrap;
        EngineLevel = car.EngineLevel;
        TiresLevel = car.TiresLevel;
        FuelLevel = car.FuelLevel;
        FrameLevel = car.FrameLevel;
    }
}
