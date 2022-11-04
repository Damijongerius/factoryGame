using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Material[] atlas;
    public GameObject pref;
    public int[] size = new int[2];

    private static WorldManager instance;

    public Map2 map;
    void Awake()
    {
        instance = this;
        map = new Map2(pref,size, atlas);
    }

    public static WorldManager getInstance()
    {
        return instance;
    }

    public void init(GameObject obj, Vector3 pos)
    {
        GameObject newc = Instantiate(obj,transform,true);
        newc.transform.position = pos;
    }
}
