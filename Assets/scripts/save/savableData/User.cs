using configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class User
{
    private static User user;

    public User(User u)
    {
        user = u;
    }

    private User() { }
    public static User GetInstance()
    {
        user ??= new User();
        return user;
    }


    public string UserName;
    public Guid guid;

    public Configuration config;
}

