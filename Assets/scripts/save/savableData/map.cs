using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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

    public List<cells> grid = new List<cells>();
}

//contains an object if not it wont save
public class cells
{
    public int x;
    public int y;

    public string ObjectName;
    public Pos pos = new Pos();
    public ObjInfo info = new ObjInfo();
}

//vector3 to 3 ints
public class Pos
{
    public float x;
    public float y;
    public float z;
}