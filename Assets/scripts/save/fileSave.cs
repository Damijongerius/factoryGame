using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public class fileSave : MonoBehaviour
{
    private string SavePath => $"{Application.persistentDataPath}/save.txt";

    [ContextMenu("Load")]
    private void Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }

    [ContextMenu("Save")]
    private void Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    private void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    public object CaptureState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<saveEntity>())
        {
            if (state.TryGetValue(saveable.ID, out object value))
            {
                state[saveable.ID] = saveable.CaptureState();
            }
        }
        return state;
    }

    public void RestoreState(Dictionary<string,object> state)
    {
       foreach(var saveable in FindObjectsOfType<saveEntity>())
        {
            if (state.TryGetValue(saveable.ID, out object value))
            {
                saveable.RestoreState(value);
            }

        }
    }
}
