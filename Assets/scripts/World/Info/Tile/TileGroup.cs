using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGroup : ITile
{
    private readonly WorldObjects.Order type;
    private List<ITile> neighbours;
    private List<Vector2> items;
    private int layer;
    private int value;

    public TileGroup(WorldObjects.Order type, List<ITile> neighbours, List<Vector2> items, int value)
    {
        this.type = type;
        this.neighbours = neighbours;
        this.items = items;
        this.value = value;
    }

    public Boolean AddNeighbour(ITile tile)
    {
        if (tile.GetType() == type)
        {
            foreach(Vector2 pos in tile.GetPosition())
            {
                items.Add(pos);
            }
            World.OnDelete();
            return false;
        }

        neighbours.Add(tile);
        return true;
    }

    public bool ContainsPosition(Vector3 pos)
    {
        foreach(Vector2 position in items)
        {
            if(position == (Vector2) pos)
            {
                return true;
            }
        }
        return false;
    }

    public List<Vector2> GetPosition()
    {
        throw new NotImplementedException();
    }

    public new WorldObjects.Order GetType() => type;

    public bool IsNeighbour(Vector3 pos)
    {
        foreach (Vector2 item in  items)
        {

        }

        return false;
    }

    public void RemoveNeighbour(ITile tile)
    {
        throw new NotImplementedException();
    }

    public void RunBehavior()
    {
        throw new NotImplementedException();
    }
}
