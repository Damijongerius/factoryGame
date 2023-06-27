using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LithiumIonBattery : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "powerCost", 0.01f},
        { "upkeepCost", 0.6f },
        { "powerstorageCap", 60 },
        { "dataStorageCap", 20 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 800;
}
