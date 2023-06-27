using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPlant : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "upkeepCost", 0.6f },
        { "powerOutage", 7 },
        { "powerstorageCap", 6 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 690;
}
