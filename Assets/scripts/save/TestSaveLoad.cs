using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestSaveLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SaveData.current.profile.currency += 1;
        Debug.Log(SaveData.current.profile.currency);
    }

    public void OnGameSomething()
    {
        SaveData.current = (SaveData)SerManager.Load(Application.persistentDataPath + "/saves/Save.save");
    }
}
