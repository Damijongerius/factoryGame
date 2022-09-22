using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public sealed class SaveFile
{
    private static SaveFile saveFile;

    public SaveFile(SaveFile s) { 
        saveFile = s;
    }

    private SaveFile() { }
    public static SaveFile GetInstance()
    {
        if (saveFile == null)
        {
            saveFile = new SaveFile();
        }
        return saveFile;
    }
    



    [JsonProperty("profile")]
    public Profile profile = new Profile();
    [JsonProperty("map")]
    public Map map = new Map();
    
}
