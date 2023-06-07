using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGroup : ITile
{
    private readonly WorldObjects.Order type;
    private ITileBehavior tileBehavior;
    private List<ITile> neighbours;
    private List<Vector2> items;
    private List<GameObject> objects;
    private int layer;
    private int value;

    public TileGroup(WorldObjects.Order type, List<ITile> neighbours, List<Vector2> items, int layer, List<GameObject> objects)
    {
        this.type = type;
        this.neighbours = neighbours;
        this.items = items;
        this.layer = layer;
        this.objects = objects;
    }

    public Boolean AddNeighbour(ITile tile)
    {
        if (tile.GetType() == type)
        {
            foreach(Vector2 pos in tile.GetPosition())
            {
                items.Add(pos);
            }
            World.World.GetInstance().OnDelete(tile);
            return false;
        }

        neighbours.Add(tile);
        return true;
    }
    public void ConfigureBehavior(ITileBehavior behavior) =>tileBehavior = behavior;
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
    public List<ITile> GetNeighbours() => neighbours;
    public List<Vector2> GetPosition() => items;

    public object GetSavedData()
    {
        throw new NotImplementedException();
    }

    public new WorldObjects.Order GetType() => type;
    public bool IsNeighbour(Vector3 pos)
    {
        foreach (Vector2 item in  items)
        {
            if(item == new Vector2(pos.x,pos.z))
            {
                return true;
            }
        }
        return false;
    }
    public void RemoveNeighbour(ITile tile)
    {
        foreach(Vector2 pos in tile.GetPosition())
        {
            items.Remove(pos);
        }
    }

    public void RunBehavior(ITile tile, object obj) => tileBehavior.Execute(tile, obj);
    
}
