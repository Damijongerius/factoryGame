using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Cell(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    private int x;
    private int y;

    public Vector2 GetPos()
    {
        return new Vector2(x, y);
    }


}
