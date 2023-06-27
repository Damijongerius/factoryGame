using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "upkeepCost", 0.2f },
        { "powerOutage", 3 },
        { "powerstorageCap", 2.5f },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 300;
}
