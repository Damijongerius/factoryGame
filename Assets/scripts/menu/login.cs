using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
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
                    Debug.Log("wrong");
                    break;
                }
            case 5:
                {
                    //incorrect password
                    password.text = "";
                    Debug.Log("wrong");
                    break;
                }
            case 6:
                {
                    //sorry problem on our end pls play offline or try again later
                    Debug.Log("wrong");
                    break;
                }
        }
        return true;


        void Configure()
        {
            manager.StartCoroutine((IEnumerator)ws.GetProfiles(ConfigureResult));
        }
        bool ConfigureResult(string json)
        {
            JsonSaveLoad loader = new JsonSaveLoad();
            Debug.Log(json);
            packet p = new packet();
            p = JsonConvert.DeserializeObject<packet>(json);

            int id = 0;
            List<int> ids = new List<int>();
            for(int i = 0; i < p.profiles.Length; i++)
            {
                if (true)
                {
                    id = p.profiles[i].savefile_ID;
                    ids.Add(p.profiles[i].savefile_ID);
                }
            }
            if (ids.Count > 1)
            {
                manager.StartCoroutine((IEnumerator)ws.getSaveFiles(ids.ToArray(), SaveFileResult));
            }
            else if (id != 0)
            {
                manager.StartCoroutine((IEnumerator)ws.getSaveFile(id, SaveFileResult));
            }
            else
            {
                Debug.Log("nothing to load");
            }

            bool SaveFileResult(string json)
            {
                packet2 packet2 = new packet2();
                packet2.sfs = JsonConvert.DeserializeObject<SaveFile[]>(json);
                foreach(SaveFile sf in packet2.sfs)
                {
                    loader.Save(sf.profile.Name, sf);
                }
                return true;
            }
            return false;
        }
    }

    public class packet2
    {
        public SaveFile[] sfs;
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
            public string DateMade;
            public string DateSeen;
            public string TimePlayed;
            public DateTime DM;
            public DateTime DS;
            public TimeSpan TP;
        }
    }
}
