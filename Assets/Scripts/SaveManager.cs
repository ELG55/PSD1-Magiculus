using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {

    public static void Salvar(Savedata sv)
    {
        string path = Application.persistentDataPath + "/file1.qlo";
    }
}
