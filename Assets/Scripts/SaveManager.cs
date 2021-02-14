using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {

    public static void Salvar(Savedata sv)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/file1.qlo";
        FileStream stream = new FileStream(path, FileMode.Create);

        Savedata data = sv;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Savedata Cargar()
    {
        string path = Application.persistentDataPath + "/file1.qlo";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Savedata sv = formatter.Deserialize(stream) as Savedata;
            stream.Close();

            return sv;
        }
        else
        {
            Debug.LogError("save error not found in: " + path);
            return null;
        }
    }
}
