using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class signingManager : MonoBehaviour
{
    private static signingManager instance;

    private Login login;
    private SignUp signUp;

    private Button switchLogging;
    private Button switchSigning;

    private Button logging;
    private Button signing;

    private WebServer ws;
    private void Start()
    {
        ws = new WebServer();
        setter();
    }

    private bool setter()
    {
        try
        {
            switchLogging = transform.Find("Login").GetComponent<Button>();
            switchSigning = transform.Find("SignUp").GetComponent<Button>();

            logging = transform.Find("loginButton").GetComponent<Button>();
            signing = transform.Find("signupButton").GetComponent<Button>();

            switchLogging.onClick.AddListener(Switch);
            switchSigning.onClick.AddListener(Switch);

            TMP_InputField password = GameObject.Find("passwordInput").GetComponent<TMP_InputField>();
            TMP_InputField username = GameObject.Find("userInput").GetComponent<TMP_InputField>();

            login = new Login(logging, username, password, this);
            signUp = new SignUp(signing, username, password, this);

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }

    }

    private void Switch()
    {
        if (switchSigning.IsActive())
        {
            switchSigning.gameObject.SetActive(false);
            signing.gameObject.SetActive(true);
        }
        else
        {
            switchSigning.gameObject.SetActive(true);
            signing.gameObject.SetActive(false);
        }
    }

    public void CreateUser(Guid GUID, string UserName, string password, Func<ReturnedData, bool> retrn)
    {
        //StartCoroutine(ws.CreateUser(GUID, UserName, password, retrn));
    }
}
