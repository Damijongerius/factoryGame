using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUp
{
    private readonly Button signUp;
    private readonly TMP_InputField userName;
    private readonly TMP_InputField password;
    private GameObject FailBackground;

    private readonly signingManager manager;

    private readonly WebServer ws = new WebServer();
    private Guid userGUID;

    public SignUp(Button signingup, TMP_InputField userName, TMP_InputField password, signingManager sm)
    {
        this.signUp = signingup;
        this.password = password;
        this.userName = userName;
        this.manager = sm;

        FailBackground = sm.transform.Find("FailBackground").gameObject;
        signUp.onClick.AddListener(SigningUp);

    }

    private void SigningUp()
    {
        if (password.text.Length > 8)
        {
            if (password.text.Any(char.IsUpper))
            {
                if (password.text.Any(char.IsDigit))
                {
                    userGUID = Guid.NewGuid();

                    manager.StartCoroutine(ws.CreateUser(Guid.NewGuid(), userName.text, password.text, onResult));
                    //manager.CreateUser(userGUID, userName.text, password.text, onResult);
                }
                else
                {
                    requirements(Requirements.Digit);
                }
            }
            else
            {
                requirements(Requirements.UpperCase);
            }
        }
        else
        {
            requirements(Requirements.Characters);
        }
    }

    public bool onResult(ReturnedData data)
    {
        switch (data.status)
        {
            case 0:
            Debug.Log("error");
                FailBackground.gameObject.SetActive(true);
                break;

        case 1:
        Debug.Log("succes");
                break;

            default:
                Debug.Log("Nee");
                break;
        } 
        return true;
    }

    private void AlreadyExists()
    {
        //mention problem
    }

    private void requirements(Requirements req)
    {
        if (req == Requirements.UpperCase)
        {
            Debug.Log("missing uppercase");
        } 
        if (req == Requirements.Digit)
        {
            Debug.Log("missing Digit");
        }
        if(req == Requirements.Characters)
        {
            Debug.Log("missing Characters");
        }
    }

    enum Requirements
    {
        Characters , UpperCase , Digit 
    }


}
