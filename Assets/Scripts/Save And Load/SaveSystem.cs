using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayerData (Vehicle car, bool SaveScrap)
    {
        PlayerData OldData = LoadPlayerData();
        PlayerData NewData = new PlayerData(car);
        PlayerData SaveData = new PlayerData();

        // Get Old Data And Transfer To New Save File
        if (OldData != null)
        {
            SaveData.Scrap = OldData.Scrap;
            for (int i = 0; i < 3; i++)
            {
                SaveData.VehiclePurchased[i] = OldData.VehiclePurchased[i];
                SaveData.EngineLevel[i] = OldData.EngineLevel[i];
                SaveData.TiresLevel[i] = OldData.TiresLevel[i];
                SaveData.FuelLevel[i] = OldData.FuelLevel[i];
                SaveData.FrameLevel[i] = OldData.FrameLevel[i];
            }
        }

        // Once Save Has Old Data, Input New Data To Save
        if (SaveScrap)
        {
            SaveData.Scrap = NewData.Scrap;
        }
        SaveData.SelectedVehicle = NewData.SelectedVehicle;
        SaveData.VehiclePurchased[car.VehicleNumber] = NewData.VehiclePurchased[car.VehicleNumber];
        SaveData.EngineLevel[car.VehicleNumber] = NewData.EngineLevel[car.VehicleNumber];
        SaveData.TiresLevel[car.VehicleNumber] = NewData.TiresLevel[car.VehicleNumber];
        SaveData.FuelLevel[car.VehicleNumber] = NewData.FuelLevel[car.VehicleNumber];
        SaveData.FrameLevel[car.VehicleNumber] = NewData.FrameLevel[car.VehicleNumber];

        BinaryFormatter formatter = new BinaryFormatter();
        //Application.persistentDataPath
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        // Save Data To File and Close It
        formatter.Serialize(stream, SaveData);
        stream.Close();

    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
