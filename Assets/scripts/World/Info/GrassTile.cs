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

    }
}
