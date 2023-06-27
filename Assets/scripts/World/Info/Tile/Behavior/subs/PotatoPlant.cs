using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPlant : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "powerOutage", 100 },
        { "powerstorageCap", 10 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 2500;
}
