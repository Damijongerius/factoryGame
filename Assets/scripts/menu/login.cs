using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;
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
                    manager.transform.parent.parent.gameObject.GetComponent<openCloseManager>().HandleClick();
                    break;
                }
            case 3:
                {
                    //if usersavefile contains this profile delete
                    break;
                }
            case 5:
                {
                    //incorrect password
                    password.text = "";
                    break;
                }
            case 6:
                {
                    //sorry problem on our end pls play offline or try again later
                    break;
                }
        }
        return true;
    }
}
