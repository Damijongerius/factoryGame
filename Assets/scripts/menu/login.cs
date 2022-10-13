using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Login
{
    private readonly Button login;
    private readonly InputField userName;
    private readonly InputField password;

    private readonly signingManager manager;

    private readonly WebServer ws = new WebServer();

    public Login(Button login,InputField userName, InputField password, signingManager sm)
    {
        this.login = login;
        this.password = password;
        this.userName = userName;
        this.manager = sm;

        login.onClick.AddListener(LoggingIn);

    }

    private void LoggingIn()
    {
        string user = userName.text.ToString();
        string userpassword = password.text.ToString();
        manager.StartCoroutine(ws.loadUser(user,userpassword, onResult));
    }

    public bool onResult(string json)
    {
        //set string as savefile
        return true;
    }


    private void OnWrongPassword()
    {

    }

    private void OnUnexistingUser()
    {

    }
}
