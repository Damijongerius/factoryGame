using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private readonly SaveFile sf = SaveFile.GetInstance();

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
            Generate(true);
        }
        else
        {
            Generate(true);
        }
    }

    public void Generate(bool load)
    {
        if (load)
        {
            Vector2 seed = new Vector2(0, 0);
            if (sf.map != null && sf.map.GetSeed() != null)
            {
                seed = sf.map.GetSeed();
            }
            if (seed.x != 0)
            {
                Debug.Log(seed.x + "," + seed.y);
                map = new Map2(pref, seed, size, atlas);
            }
            else
            {
                map = new Map2(pref, size, atlas);
            }
        }
        else
        {
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
