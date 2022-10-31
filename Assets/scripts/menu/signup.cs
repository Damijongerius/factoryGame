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
    private GameObject Plane;

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
        Plane = sm.transform.Find("Plane").gameObject;
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

                    manager.StartCoroutine(ws.CreateUser(userGUID, userName.text, password.text, onResult));
                    Plane.SetActive(true);
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
                Login();
                break;

            default:
            {
                Debug.Log("Nee");
                Plane.SetActive(false);
                break;
            }
        } 
        return true;
    }

    private void Login()
    {
        User user = User.GetInstance();
        user.guid = this.userGUID;
        Debug.Log(this.userGUID);
        user.UserName = this.userName.text;
        manager.transform.parent.parent.gameObject.GetComponent<openCloseManager>().HandleClick();


    }

    private void requirements(Requirements req)
    {
        if (req == Requirements.UpperCase)
        {
            //show requirement
            Debug.Log("missing uppercase");
        } 
        if (req == Requirements.Digit)
        {
            //show requirement
            Debug.Log("missing Digit");
        }
        if(req == Requirements.Characters)
        {
            //show requirement
            Debug.Log("missing Characters");
        }
    }

    enum Requirements
    {
        Characters , UpperCase , Digit 
    }


}
