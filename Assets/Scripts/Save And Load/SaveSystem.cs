using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveVehicle (Car car)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Application.persistentDataPath
        string path = "C:/Users/brian/Desktop/College Files/UnityProjects/GAME3033_Final_ViveirosBrian/Save" + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(car);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadVehicle()
    {
        string path = "C:/Users/brian/Desktop/College Files/UnityProjects/GAME3033_Final_ViveirosBrian/Save" + "/player.data";
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
