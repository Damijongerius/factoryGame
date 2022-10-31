using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class signingManager : MonoBehaviour
{
    private static signingManager instance;

    private Login login;
    private SignUp signUp;
    private Offline offline;

    private Button switchLogging;
    private Button switchSigning;

    private Button logging;
    private Button signing;

    private WebServer ws;
    private void Start()
    {
        ws = new WebServer();
        try
        {
            switchLogging = transform.Find("login").GetComponent<Button>();
            switchSigning = transform.Find("signup").GetComponent<Button>();

            logging = transform.Find("loginButton").GetComponent<Button>();
            signing = transform.Find("signupButton").GetComponent<Button>();

            switchLogging.onClick.AddListener(Switch);
            switchSigning.onClick.AddListener(Switch);

            TMP_InputField password = GameObject.Find("passwordInput").GetComponent<TMP_InputField>();
            TMP_InputField username = GameObject.Find("userInput").GetComponent<TMP_InputField>();

            login = new Login(logging, username, password, this);
            signUp = new SignUp(signing, username, password, this);
            offline = new Offline(this);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        
    }

    public void GoOffline()
    {
        offline.Set();
    }

    private void Switch()
    {
        if (switchSigning.IsActive())
        {
            switchSigning.gameObject.SetActive(false);
            logging.gameObject.SetActive(false);
        }
        else
        {
            switchSigning.gameObject.SetActive(true);
            logging.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        if(User.GetInstance().UserName != null)
        {
            transform.parent.parent.gameObject.GetComponent<openCloseManager>().HandleClick();
        }
    }
}
