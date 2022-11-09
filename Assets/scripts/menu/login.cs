using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Login
{
    private readonly Button login;
    private readonly TMP_InputField userName;
    private TMP_InputField password;

    private int loading;
    private int loaded;
    private GameObject plane;

    private readonly signingManager manager;

    private readonly WebServer ws = new WebServer();

    public Login(Button login, TMP_InputField userName, TMP_InputField password, signingManager sm)
    {
        this.login = login;
        this.password = password;
        this.userName = userName;
        plane = sm.transform.Find("Plane").gameObject;
        this.manager = sm;

        login.onClick.AddListener(LoggingIn);

    }

    private void LoggingIn()
    {
        plane.SetActive(true);
        manager.StartCoroutine(ws.loadUser(userName.text, password.text, onResult));
    }

    public bool onResult(ReturnedData rd)
    {
        switch (rd.status)
        {
            case 4:
                {
                    //correct password
                    Debug.Log(rd.Message);
                    User user = User.GetInstance();

                    user.guid = new Guid(rd.info.GUID);
                    user.UserName = rd.info.UserName;

                    Configure();
                    break;
                }
            case 3:
                {
                    //if usersavefile contains this profile delete
                    plane.SetActive(false);
                    Debug.Log("wrong");
                    break;
                }
            case 5:
                {
                    //incorrect password
                    password.text = "";
                    plane.SetActive(false);
                    Debug.Log("wrong");
                    break;
                }
            case 6:
                {
                    //sorry problem on our end pls play offline or try again later
                    Debug.Log("wrong");
                    plane.SetActive(false);
                    break;
                }
        }
        return true;
    }

    void Configure()
    {
        manager.StartCoroutine(ws.GetProfiles(ConfigureResultAsync));
    }
    bool ConfigureResultAsync(string json)
    {
        JsonSaveLoad loader = new JsonSaveLoad();
        Debug.Log(json);
        packet p = new packet();
        p = JsonConvert.DeserializeObject<packet>(json);

        List<int> ids = new List<int>();
        for (int i = 0; i < p.profiles.Length; i++)
        {
            SaveFile savedfile;
            try
            {
                savedfile = JsonConvert.DeserializeObject<SaveFile>(loader.Load(p.savefiles[i].SaveName, false));
                if (savedfile.profile.DateSeen < p.profiles[i].DateSeen)
                {
                    ids.Add(p.profiles[i].savefile_ID);
                }
            }
            catch
            {
                ids.Add(p.profiles[i].savefile_ID);
            }

        }
        if (ids.Count != 0)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                loading++;
                manager.StartCoroutine(ws.GetSaveFile(ids[i], SaveFileResult));
            }
        }
        else
        {
            manager.transform.parent.parent.gameObject.GetComponent<openCloseManager>().HandleClick();
        }
        return false;
    }

    bool SaveFileResult(string json)
    {
        loaded++;
        JsonSaveLoad loader = new JsonSaveLoad();
        SaveFile sf = JsonConvert.DeserializeObject<SaveFile>(json);
        new SaveFile(sf);
        loader.Save(sf.profile.Name, sf, false);
        loader.ListProfile(sf.profile.Name, false);

        if (loaded == loading)
        {
            manager.transform.parent.parent.gameObject.GetComponent<openCloseManager>().HandleClick();
        }
        return true;
    }
}

public class packet
{
    public SaveFile[] savefiles;
    public Profile[] profiles;
    public class SaveFile
    {
        public int ID;
        public string SaveName;
        public string users_UsersId;
    }

    public class Profile
    {
        public int savefile_ID;
        public DateTime DateMade;
        public DateTime DateSeen;
        public DateTime TimePlayed;
    }
}
