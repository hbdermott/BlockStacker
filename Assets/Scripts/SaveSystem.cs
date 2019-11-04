
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveUser(UserData user)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        UserData data = new UserData(user);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static UserData LoadUser()
    {
        string path = Application.persistentDataPath + "/user.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            UserData data = formatter.Deserialize(stream) as UserData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file at " + path + " not found");
            return null;
        }
    }
}
