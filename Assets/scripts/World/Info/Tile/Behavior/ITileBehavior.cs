

using System;
using System.Collections.Generic;

public interface ITileBehavior
{
    public Dictionary<string, float> GetData();

    public float Price();
}
