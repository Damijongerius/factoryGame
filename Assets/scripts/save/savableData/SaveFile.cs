using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SaveFile
{
    private static SaveFile _saveFile;
    public static SaveFile saveFile 
    { 
        get 
        { 
            if(_saveFile == null)
            {
                _saveFile = new SaveFile();
            }
            return _saveFile; 
        } 
    }

    public Profile profile = new Profile();
    public Map map = new Map();
    
}
