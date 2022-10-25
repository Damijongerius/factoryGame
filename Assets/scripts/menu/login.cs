using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Login
{
    private readonly Button login;
    private readonly TMP_InputField userName;
    private readonly TMP_InputField password;

    private readonly signingManager manager;

    private readonly WebServer ws = new WebServer();

    public Login(Button login, TMP_InputField userName, TMP_InputField password, signingManager sm)
    {
        this.login = login;
        this.password = password;
        this.userName = userName;
        this.manager = sm;

        login.onClick.AddListener(LoggingIn);

    }

    private void LoggingIn()
    {
        Debug.Log("click");
        //manager.StartCoroutine(ws.loadUser(userName.text, password.text, onResult));
    }

    public bool onResult(ReturnedData json)
    {
        //set string as savefile
        
        //if logged in execute cloud sync
        //Test by last time logged in
        return true;
    }


    private void OnWrongPassword()
    {

    }

    private void OnUnexistingUser()
    {

    }
}
