using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldObjects;

public abstract class Tile
{
    protected WorldObjects.Order type;
    protected GameObject obj;
    protected int x;
    protected int y;
    protected List<Tile> Neighbours = new();

    public Structures structure;
    public Tile(int X, int Y, GameObject gameObject, int index)
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

    public Vector3 Pos()
    {
        return new Vector2(this.x, this.y);
    }

    public Tile AddNeighbour(Tile tile)
    {
       Neighbours.Add(tile);
        return this;
    }
}
