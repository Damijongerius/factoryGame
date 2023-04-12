using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGroup
{
    private readonly Types type;
    private List<Tile> Neighbours;
    private Dictionary<Vector2,Tile> items;
    private int value;

    public TileGroup(Types type, List<Tile> neighbours, Dictionary<Vector2, Tile> items, int value)
    {
        this.type = type;
        Neighbours = neighbours;
        this.items = items;
        this.value = value;
    }

    public Types GetType()
    {
        return type;
    }

    public void GetNeighbours() 
    {

    }

    public void GetItems()
    {

    }

    public void GetItemTile(Vector2 pos)
    {

    }

    public void UpdateConnections()
    {

    }

    public void AddValue(int value) => this.value += value < items.Count ? value : 0; 

    public void Update()
    {

    }

    public enum Types
    {
        POWERLINE,
        DATACABLE,
        WATERPIPE,
        POWERCABLE
    }
}
