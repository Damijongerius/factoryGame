using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int year = 0;
    public int month = 1;
    public int day = 1;
    public int hour = 8;
    public int minute = 30;
    public float time;
    public new GameObject light;
    public float Rotation;
    public float changespeed;

    public bool daytime;

    // Update is called once per frame
    void Update()
    {
        if(daytime)
        {
            changespeed = Mathf.Lerp(changespeed, 1f, 0.001f);
        }
        else
        {
            changespeed = Mathf.Lerp(changespeed, 0, 0.001f);
        }
        light.transform.rotation = Quaternion.Euler(Mathf.Lerp(light.transform.localRotation.x, Rotation, changespeed), -30, 0);
        //light.transform.Rotate(new Vector3(1,0,0));

        time += Time.deltaTime;
        if(time >= 2)
        {
            minute += 10;
            time= 0;
        }

        if(minute == 60)
        {
            hour++;
            minute = 0;
        }

        if(hour == 24)
        {
            day++;
            hour = 0;
        }

        if(daytime && hour == 22)
        {
            daytime = false;
        }

        if (!daytime && hour == 6)
        {
            daytime = true;
        }

        if (hour < 10)
        {
            StatisticsBar.Time("Date: 0" + hour + ":" + minute + "-" + day + "/" + month + "/" + year);
        }
        else
        {
            StatisticsBar.Time("Date: " + hour + ":" + minute + "-" + day + "/" + month + "/" + year);
        }
    }
}
