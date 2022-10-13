using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SignUp
{
    private readonly Button signUp;
    private readonly InputField userName;
    private readonly InputField password;

    private readonly signingManager manager;

    private readonly WebServer ws = new WebServer();
    private Guid userGUID;

    public SignUp(Button signingup,InputField userName, InputField password, signingManager sm)
    {
        this.signUp = signingup;
        this.password = password;
        this.userName = userName;
        this.manager = sm;

        signUp.onClick.AddListener(SigningUp);

    }

    private void SigningUp()
    {
        string user = userName.text.ToString();
        string userpassword = password.text.ToString();

        if(userpassword.Length > 8)
        {
            if (userpassword.Any(char.IsUpper))
            {
                if (userpassword.Any(char.IsDigit))
                {
                    userGUID = Guid.NewGuid();

                    manager.StartCoroutine(ws.CreateUser(userGUID, user, userpassword));
                }
                else
                {
                    requirements();
                }
            }
            else
            {
                requirements();
            }
        }
        else
        {
            requirements();
        }
    }

    private void AlreadyExists()
    {
        //mention problem
    }

    private void requirements()
    {
        // mention problem
    }


}
