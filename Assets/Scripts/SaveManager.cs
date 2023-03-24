
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        int data = PlayerData.level;
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadGame()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = int.Parse(formatter.Deserialize(stream).ToString());
            return data;
        }
        else
        {
            return 0;
        }
    }
}
