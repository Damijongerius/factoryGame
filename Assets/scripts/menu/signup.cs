using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUp
{
    private readonly TMP_InputField userName;
    private readonly TMP_InputField password;


    private readonly signingManager manager;

    private readonly WebServer ws = new WebServer();
    private Guid userGUID;

    public SignUp(TMP_InputField userName, TMP_InputField password, signingManager sm)
    {
        this.password = password;
        this.userName = userName;
        this.manager = sm;

    }

    public void SigningUp()
    {
        if (password.text.Length > 8)
        {
            if (password.text.Any(char.IsUpper))
            {
                if (password.text.Any(char.IsDigit))
                {
                    userGUID = Guid.NewGuid();

                    manager.StartCoroutine(ws.CreateUser(userGUID, userName.text, password.text, onResult));
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


                break;

        case 1:

                Login();
                break;

            default:
            {

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
