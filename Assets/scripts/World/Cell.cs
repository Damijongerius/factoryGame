using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2
{
    public bool isWater = false;
    public int height;

    public Cell2(bool isWater)
    {
        this.isWater = isWater;
    }

    public bool[] GetWaters(Cell2[,,] _grid, int x, int z)
    {
        bool[] water = { false, false, false, false };
        try
        {
            if (_grid[x, 0, z + 1].isWater)
            {
                water[0] = true;
            }
        }
        catch { }
        try
        {
            if (_grid[x + 1, 0, z].isWater)
            {
                water[1] = true;
            }
        }
        catch { }
        try
        {
            if (_grid[x, 0, z - 1].isWater)
            {
                water[2] = true;
            }
        }
        catch { }
        try
        {
            if (_grid[x - 1, 0, z].isWater)
            {
                water[3] = true;
            }
        }
        catch { }
        return water;
    }
}
