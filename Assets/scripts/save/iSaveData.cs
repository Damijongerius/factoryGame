using UnityEngine;

public interface iSaveData
{
    object CaptureState();
    void RestoreState(object state);
}