using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map
{
    public Grid grid { get; set; }  = new Grid();
}

//contains all map info needed
public class Grid
{
    //seed
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
    public ObjInfo info = new ObjInfo();
}
