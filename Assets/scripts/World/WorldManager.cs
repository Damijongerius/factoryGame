using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Material atlas;
    public int[] size = new int[2];

    private static WorldManager instance;

    public Map2 map;
    void Awake()
    {
        instance = this;
        map = new Map2(size,atlas);
    }

    public static WorldManager getInstance()
    {
        return instance;
    }
}
