using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {

    public static void Salvar(SaveFile sv)
    {
        
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/file"+sv.slot.ToString()+".qlo";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveFile data = sv;
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void Borrar(int casilla)
    {

        string path = Application.persistentDataPath + "/file" + casilla.ToString() + ".qlo";
        Debug.Log("momentos antes de la funacion");
        File.Delete(path);
        Debug.Log("momentos despues de la funacion");

    }

    public static SaveFile Cargar(int casilla)
    {
        string path = Application.persistentDataPath + "/file"+casilla+".qlo";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveFile sv = formatter.Deserialize(stream) as SaveFile;
            stream.Close();
            return sv;
        }
        else
        {
            Debug.Log("save error not found in: " + path);
            return null;
        }
    }

    public static void SalvarConfig(Config cf)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/config.ini";
        FileStream stream = new FileStream(path, FileMode.Create);

        Config data = cf;
        formatter.Serialize(stream, data);
        stream.Close();
    }



    /*public static Config CargarConfig()
    {

    }*/

}
