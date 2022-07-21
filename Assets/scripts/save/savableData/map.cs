using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class Map
{
    public Grid grid;
}


public class Grid
{
    public float xRange;
    public float yRange;

    public List<cells> grid;
}

public class cells
{
    public int x;
    public int y;

    public string ObjectName;
    public Pos pos;
    public ObjInfo info;
}

public class Pos
{
    public float x;
    public float y;
    public float z;
}