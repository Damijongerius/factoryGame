using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorldObjects;

public class TileGroup : ITile
{
    private Dictionary<Vector2, GameObject> positions = new();
    private WorldObjects.Order type;
    private ITileBehavior behavior;
    public bool ContainsPositions(List<Vector2> poss)
    {
        foreach (Vector2 pos in poss)
        {
            if (positions.ContainsKey(pos))
            {
                return true;
            }
        }
        return false;
    }

    public ITileBehavior Getbehavior()
    {
        return behavior;
    }

    public Dictionary<Vector2, GameObject> getObjectPositions()
    {
        return positions;
    }

    public List<Vector2> GetPosition()
    {
        return positions.Keys.ToList();
    }

    public void Init(Dictionary<Vector2, GameObject> content, ITileBehavior behavior, Order tpye)
    {
        this.positions = content;
        this.behavior = behavior;
        this.type = tpye;
    }

    public void SetBehavior(ITileBehavior behavior)
    {
        this.behavior = behavior;
    }

    Order ITile.GetType()
    {
        return this.type;
    }
}
