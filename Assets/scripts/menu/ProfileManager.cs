using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Web.Profile;

public class ProfileManager : MonoBehaviour
{
    public Canvas loadingscreen;

    public static bool playing = false;
    public GameObject grid;
    public WebServer ws = new WebServer(); 
    private SaveFile gameSave = SaveFile.GetInstance();
    //public ObjectSaveLoad objects = new ObjectSaveLoad();
    public JsonSaveLoad jsonSL = new();

    private static DateTime startPlaying;

    private static ProfileManager profileManager;
    private void Start()
    {
        profileManager = this;
        JsonSaveLoad loader = new();
    }
    

    public void Create(GameObject inputField)
    {
        string ProfileName = inputField.GetComponent<TextMeshProUGUI>().text;
        if(ProfileName.Length > 1)
        {
            jsonSL.Exsisting(ProfileName);
            jsonSL.CreateSaveData(ProfileName);

            loadingscreen.gameObject.SetActive(true);
        }
        else
        Debug.Log("to short");
    }

    public void Delete(GameObject profileObject)
    {
        //load clicked profile in load screen
        string profileName = profileObject.transform.Find("ProfileName").GetComponent<TextMeshProUGUI>().text;
        


        if (User.GetInstance().guid == new Guid("aaaa1111-2022-2022-2022-aaaaaaaaaaa1"))
        {
            Debug.Log("false");
            jsonSL.DeleteProfile(profileName, false);
        }
        else
        {
            StartCoroutine(ws.DeleteSaveFile(profileName));
            jsonSL.DeleteProfile(profileName, true);
        }
        

    }

    public void DeleteUser()
    {
        User user = User.GetInstance();
        if (user.UserName != null)
        {
            StartCoroutine(ws.DeleteUser());

            jsonSL.DeleteUserData(user.guid);
            user = null;

        }
    }

    public void Continue() 
    {
        //for all the saves find the latest(can only be done if dates are saved in savefile)

        if(jsonSL.ReadListedProfiles().lastPlayed != null)
        {
           jsonSL.Load(jsonSL.ReadListedProfiles().lastPlayed, true);
            

            loadingscreen.gameObject.SetActive(true);
        }
        else
        {
            //else say that there is no profile to load
            Debug.Log("there is no profile to load");
        }


    }

    public void Load(GameObject profileObject)
    {
        //load clicked profile in load screen
        string ProfileName = profileObject.transform.Find("ProfileName").GetComponent<TextMeshProUGUI>().text;

            jsonSL.Load(ProfileName, true);

        loadingscreen.gameObject.SetActive(true);

    }

    public void Save()
    {
        //getting profile name 
        string ProfileName = gameSave.profile.Name;
        gameSave.profile.TimePlayed += System.DateTime.Now - startPlaying;
        gameSave.profile.DateSeen = System.DateTime.Now;
        //objects.SaveObjects();

        if (User.GetInstance().guid == new Guid("aaaa1111-2022-2022-2022-aaaaaaaaaaa1"))
        {
            jsonSL.Save(ProfileName, gameSave, false);
        }
        else
        {
            jsonSL.Save(ProfileName, gameSave, true);
        }
        
    }

    public void BackToMainMenu()
    {
        Save();
        playing = false;
        SceneManager.LoadScene("MainMenu");

    }

   // private void Awake()
   // {
    //    if (playing == true)
    //    {
     //       grid.GetComponent<WorldManager>().Generate(true);
    //        
     //   }
   // }

    public static ProfileManager getObject()
    {
        return profileManager;
    }

    public void StartSendCoroutine(string data)
    {
        StartCoroutine(ws.sendSaveFile(data));
        Debug.Log("starting court");
    }
    public void StartDeleteCoroutine(string data)
    {
        StartCoroutine(ws.DeleteSaveFile(data));
        Debug.Log("starting court");
    }

    public void ActualLoad()
    {
        SceneManager.LoadScene("GameScene");
        startPlaying = System.DateTime.Now;
        playing = true;
    }




}
