using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player p)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"
            + p.getUsername().ToLower() + ".fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(p);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(string name)
    {
        string path = Application.persistentDataPath + "/"
            + name.ToLower() + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Usuario no encontrado en el equipo");
            return null;
        }
    }
}
