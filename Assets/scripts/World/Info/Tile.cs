using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile
{
    protected int x;
    protected int y;
    protected GameObject obj;
    protected Tile[] Neighbours = new Tile[4];
}
