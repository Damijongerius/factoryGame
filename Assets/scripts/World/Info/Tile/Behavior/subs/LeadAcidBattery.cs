using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadAcidBattery : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "powerCost", 0.01f},
        { "upkeepCost", 0.3f },
        { "powerstorageCap", 20 },
        { "dataStorageCap", 5 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 200;
}
