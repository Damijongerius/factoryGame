using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldObjects;

public class BasicTile : ITile
{

    private readonly WorldObjects.Order type;
    private ITileBehavior tileBehavior;
    private List<ITile> neighbours;
    private List<Vector2> items;
    private List<GameObject> objects;
    private int layer;
    private int value;

    public BasicTile(WorldObjects.Order type, List<ITile> neighbours, List<Vector2> items, int layer, List<GameObject> objects)
    {
        this.type = type;
        this.neighbours = neighbours;
        this.items = items;
        this.layer = layer;
        this.objects = objects;
    }

    public bool AddNeighbour(ITile tile)
    {
        neighbours.Add(tile);
        return true;
    }

    public bool ContainsPosition(Vector3 pos)
    {
        foreach (Vector2 position in items)
        {
            if (position == (Vector2)pos)
            {
                return true;
            }
        }
        return false;
    }

    public List<Vector2> GetPosition()
    {
        return items;
    }

    public new WorldObjects.Order GetType() => type;

    public bool IsNeighbour(Vector3 pos)
    {
        foreach (Vector2 item in items)
        {
            if (item == new Vector2(pos.x, pos.z))
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveNeighbour(ITile tile)
    {
        foreach (Vector2 pos in tile.GetPosition())
        {
            items.Remove(pos);
        }
    }

    public void ConfigureBehavior(ITileBehavior behavior)
    {
        tileBehavior = behavior;
    }

    public List<ITile> GetNeighbours()
    {
        return neighbours;
    }

    public void RunBehavior(ITile tile, object obj)
    {
        tileBehavior.Execute(this,obj);
    }

    public object GetSavedData()
    {
        throw new NotImplementedException();
    }
}
