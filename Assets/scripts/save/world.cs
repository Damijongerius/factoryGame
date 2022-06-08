using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world : MonoBehaviour, iSaveData
{
    public object CaptureState()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreState(object state)
    {
        throw new System.NotImplementedException();
    }

    [Serilizable]
}
