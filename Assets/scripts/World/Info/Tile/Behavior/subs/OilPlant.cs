using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPlant : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "upkeepCost", 1f },
        { "powerOutage", 18 },
        { "powerstorageCap", 10 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 1250;
}
