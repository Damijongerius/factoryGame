using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "powerCost", 0.5f},
        { "dataStorageCap", 30 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 500;
}