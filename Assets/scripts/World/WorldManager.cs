using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private SaveFile sf = SaveFile.GetInstance();

    public Material[] atlas;
    public GameObject pref;
    public int[] size = new int[2];
    private float[] seed;
    public GameObject[] placables;

    private static WorldManager instance;

    public Map2 map;

    private void Awake()
    {
        instance = this;
        if (!ProfileManager.playing)
        {
            Generate(false);
        }
    }

    public void Generate(bool load)
    {
        if (load)
        {
            if(sf.map.xRange != 0)
            {
                seed[0] = sf.map.xRange;
                seed[1] = sf.map.yRange;
                map = new Map2(pref, seed, size, atlas);
            }
            else
            {
                Debug.Log("este");
                map = new Map2(pref, size, atlas);
            }
        }
        else
        {
            Debug.Log("2de");
            map = new Map2(pref, size, atlas);
        }
    }

    public static WorldManager getInstance()
    {
        return instance;
    }

    public void init(GameObject obj, Vector3 pos)
    {
        GameObject combinedMesh = Instantiate(obj,transform,true);
        combinedMesh.transform.position = pos;
    }
}
