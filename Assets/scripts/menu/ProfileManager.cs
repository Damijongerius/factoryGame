using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ProfileManager : MonoBehaviour
{
    public static bool playing = false;
    public GameObject grid;
    public WebServer ws = new WebServer(); 
    private SaveFile gameSave = SaveFile.GetInstance();
    public ObjectSaveLoad objects = new ObjectSaveLoad();

    private DateTime startPlaying;

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
            startPlaying = System.DateTime.Now;
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
        startPlaying = System.DateTime.Now;
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
        gameSave.profile.TimePlayed += System.DateTime.Now - startPlaying;
        gameSave.profile.DateSeen = System.DateTime.Now;
        JsonSaveLoad saver = new();
        objects.SaveObjects();

        saver.Save(ProfileName, gameSave);
    }

    private void Awake()
    {
        gameSave.profile.Statistics.Money = 200;
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

    public void StartSendCoroutine(string data)
    {
        StartCoroutine(ws.sendSaveFile(data));
        Debug.Log("starting court");
    }




}
