using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteDish : ITileBehavior
{
    private Dictionary<string, float> mainData = new Dictionary<string, float>()
    {
        { "uploadSpeed", 10},
        { "upkeepCost", 1 },
    };
    public Dictionary<string, float> GetData() => mainData;

    public float Price() => 75;
}
