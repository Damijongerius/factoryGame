using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    public GrassTile(int X, int Y, GameObject gameObject) : base(X, Y, gameObject)
    {
        this.x = X;
        this.y = Y;
        this.obj = gameObject;
    }
}
