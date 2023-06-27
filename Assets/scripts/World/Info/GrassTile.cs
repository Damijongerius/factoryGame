using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorldObjects;

public class GrassTile : Tile
{
    public GrassTile(int X, int Y, GameObject gameObject, int index) : base(X, Y, gameObject, index)
    {
        this.x = X;
        this.y = Y;
        this.obj = gameObject;

        if (index == 0)
        {
            this.structure = new Miner();
        }
        if (index == 1)
        {
            this.structure = new Wire();
        }
        if (index == 2)
        {
            this.structure = new Station();
        }
    }

    public void UpdateGenPath(List<Tile> list)
    {
        Debug.Log("update" + list.Count + "," + Neighbours.Count);

        list.Add(this);
        if (this.structure is Wire)
        {
            if (Neighbours.Count == 0)
            {
                return;
            }
            foreach (GrassTile tile in Neighbours.Cast<GrassTile>())
            {
                if (!list.Contains(tile))
                {
                    tile.UpdateGenPath(list);
                }
            }
            return;
        } 
        if(this.structure is Station)
        {
            Debug.Log("return" + list);
            Miner m = (Miner)list[0].structure;
            m.AddPath(list);
            return;
        }
    }

    public void FindPath(List<Tile> closedList)
    {
        Debug.Log("finding" + closedList.Count);
        if (this.structure is Station && closedList.Count > 1)
        {
            Station s = (Station)this.structure;
            s.cycle = new List<Cycle>();
            return;
        }

        if (this.structure is Miner)
        {
            if (Neighbours.Count == 0)
            {
                return;
            }
            foreach (GrassTile tile in Neighbours.Cast<GrassTile>())
            {
                tile.UpdateGenPath(new List<Tile> { this });
                tile.structure.OnCalculate();
                return;
            }
        }

        //continue check
        closedList.Add(this);
        foreach (GrassTile tile in Neighbours.Cast<GrassTile>())
        {
            if (!closedList.Contains(tile))
            {
                tile.FindPath(closedList);
            }
        }
        return;

    }
}
