using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world : MonoBehaviour, iSaveData
{
    [SerializeField] private int level = 1;
    [SerializeField] private int xp = 100;
    public object CaptureState()
    {
        return new SaveData
        {
            level = level,
            xp = xp
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        level = saveData.level;
        xp = saveData.xp;
    }

    [Serializable]
    private struct SaveData
    {
        public int level;
        public int xp;
    }
}
