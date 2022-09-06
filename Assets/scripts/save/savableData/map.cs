using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class Map
{
    public Grid grid = new Grid();
}


public class Grid
{
    public float xRange = 0;
    public float yRange = 0;

    public List<cells> grid = new List<cells>();
}

public class cells
{
    public int x;
    public int y;

    public string ObjectName;
    public Pos pos = new Pos();
    public ObjInfo info = new ObjInfo();
}

public class Pos
{
    public float x;
    public float y;
    public float z;
}