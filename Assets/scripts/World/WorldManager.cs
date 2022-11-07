using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

public class WorldManager : MonoBehaviour
{
    private SaveFile sf = SaveFile.GetInstance();

    public Material[] atlas;
    public GameObject pref;
    public int[] size = new int[2];
    private float[] seed;

    private static WorldManager instance;

    public Map2 map;
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!ProfileManager.playing)
        {
            Generate(false);
        }
    }

    public void Generate(bool load)
    {
        if (load)
        {
            seed[0] = sf.map.xRange;
            seed[1] = sf.map.yRange;
            map = new Map2(pref, seed, size, atlas);
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
