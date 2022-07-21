using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Profile
{
    public string Name = "0";
    public DateTime DateMade;
    public DateTime DateSeen;
    public string TimePlayed = "0";
    public Statistics Statistics = new Statistics();
}

public class Statistics
{
    public int networth;
    public int money;
    public int data;
    public int xp;
    public int Level;
}




