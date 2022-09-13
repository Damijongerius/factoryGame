using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    public static bool playing = false;
    public GameObject grid;
    private SaveFile gameSave = SaveFile.GetInstance();
    public ObjectSaveLoad objects = new ObjectSaveLoad();

    private static ProfileManager profileManager;
    private void Start()
    {
        profileManager = this;
    }
    

    public void Create(GameObject inputField)
    {
        string ProfileName = inputField.GetComponent<TextMeshProUGUI>().text;
        if(ProfileName.Length > 1)
        {
            JsonSaveLoad loader = new JsonSaveLoad();
            loader.Exsisting(ProfileName);
            loader.CreateSaveData(ProfileName);
            loader.Save(ProfileName, gameSave);
            SceneManager.LoadScene("GameScene");
            playing = true;
        }
        else
        {
            Debug.Log("to short");
        }
    }

    public void Continue() 
    {
        //for all the saves find the latest(can only be done if dates are saved in savefile)
    }

    public void Load(GameObject profileObject)
    {
        //load clicked profile in load screen
        string ProfileName = profileObject.transform.Find("ProfileName").GetComponent<TextMeshProUGUI>().text;
        JsonSaveLoad loader = new();
        playing = true;


        loader.Load(ProfileName);
        SceneManager.LoadScene("GameScene");
    }

    public void Save()
    {
        
        //getting profile name 
        string ProfileName = gameSave.profile.Name;
        JsonSaveLoad saver = new();
        objects.SaveObjects();

        saver.Save(ProfileName, gameSave);
    }

    private void Awake()
    {  
        if (playing == true)
        {
            grid.GetComponent<gridSys>().Generate();
            JsonSaveLoad loader = new();
            objects.LoadObjects();
        }
    }

    public static ProfileManager getObject()
    {
        return profileManager;
    }




}
