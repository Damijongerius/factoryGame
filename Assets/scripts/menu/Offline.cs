using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offline
{
    signingManager manager;

    public Offline(signingManager manager)
    {
        this.manager = manager;
    }


    public void Set()
    {
        User user = User.GetInstance();
        user.UserName = "local";
        user.guid = new Guid("aaaa1111-2022-2022-2022-aaaaaaaaaaa1");
    }
}
