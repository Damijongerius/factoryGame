using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "dataOutage", 5},
        { "powerCost", 5},
        { "upkeepCost", 0.5f },
        { "powerstorageCap", 4 },
        { "dataStorageCap", 4 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 300;
}
