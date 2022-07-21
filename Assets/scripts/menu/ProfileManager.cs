using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    public GameObject grid;
    private void Start()
    {
        grid.GetComponent<gridSys>().Generate(false);
        //InvokeRepeating(nameof(Upating), 60f, 60f);

    }


    public void Create(GameObject inputField)
    {
        string ProfileName = inputField.GetComponent<TextMeshProUGUI>().text;
        if(ProfileName.Length > 1)
        {
            JsonSaveLoad.Exsisting(ProfileName);
            JsonSaveLoad.CreateSaveData(ProfileName);
            JsonSaveLoad.Save(ProfileName);
            JsonSaveLoad.Load(ProfileName);
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.Log("to short");
        }
    }


    

}
