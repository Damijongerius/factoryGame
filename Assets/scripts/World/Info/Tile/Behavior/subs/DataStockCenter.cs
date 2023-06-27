using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStockCenter : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "upkeepCost", 0.7f },
        { "dataStorageCap", 10 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 400;
}
