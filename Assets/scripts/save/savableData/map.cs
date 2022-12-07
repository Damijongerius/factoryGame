        using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map
{

    public float xRange = 0;
    public float yRange = 0;

    public List<cells> grid = new();
}

//contains an object if not it wont save
public class cells
{
    public int x;
    public int y;

    public ObjectTypes objType;
}
