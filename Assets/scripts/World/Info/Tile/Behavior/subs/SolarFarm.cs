using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFarm : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "upkeepCost", 0.12f },
        { "powerstorageCap", 4 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 100;
}
    

