using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldObjects;

public interface ITile
{
    public bool IsNeighbour(Vector3 pos);

    public Boolean AddNeighbour(ITile tile);

    public void RemoveNeighbour(ITile tile);

    public List<ITile> GetNeighbours();

    public void configureBehavior(ITileBehavior behavior);

    public void runBehavior(object obj);
    public WorldObjects.Order GetType();
    public List<Vector2> GetPosition();

    public Boolean ContainsPosition(Vector3 pos);
}

/*
 public interface Tile
{
    protected WorldObjects.Order type;
    protected GameObject obj;
    protected Vector3 pos;
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
*/