using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static ReturnedData;

public class LoginSave
{
    
    public void makeLog(Info info)
    {
        string path = Application.persistentDataPath + "/UserSave";

        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

        BinaryWriter writer = new BinaryWriter(file);

        string fileContent = JsonConvert.SerializeObject(info);

        writer.Close();
        file.Close();
    }
}