using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodiumSulphurBattery : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "powerCost", 0.01f},
        { "upkeepCost", 1f },
        { "powerstorageCap", 120 },
        { "dataStorageCap", 35 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 10000;
}