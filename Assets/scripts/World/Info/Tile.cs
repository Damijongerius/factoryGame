using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile
{
    protected int x;
    protected int y;
    protected GameObject obj;
    protected List<Tile> Neighbours;
    public Tile(int X, int Y, GameObject gameObject)
    {
        this.x = X;
        this.y = Y;
        this.obj = gameObject;
    }

    public bool PosistionCheck(int X, int Y)
    {
        if (this.x == X && this.y == Y)
        {
            return true;
        }
        return false;
    }

    public Tile AddNeighbour(Tile tile)
    {
       Neighbours.Add(tile);
        return this;
    }
}
