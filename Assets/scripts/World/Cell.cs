using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2
{
    public bool isWater = false;
    public int height;
    public GameObject obj;

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

    public bool CheckCell(int x, int z, string tag)
    {
        if (WorldManager.getInstance().map.Grid[x,0, z].obj != null)
        {
            if (WorldManager.getInstance().map.Grid[x,0, z].obj.CompareTag(tag))
            {
                return true;
            }
            return false;
        }
        return false;
    }

    public GameObject GetCellObj(int x, int z, string tag)
    {
        if (WorldManager.getInstance().map.Grid[x, 0, z].obj != null && WorldManager.getInstance().map.Grid[x, 0, z].obj.CompareTag(tag))
        {
            return WorldManager.getInstance().map.Grid[x, 0, z].obj;
        }
        return null;

    }

    public bool[] CheckNeighbour(int x, int z, string tag)
    {
        bool[] NeighbourCountBinair = new bool[4] { false, false, false, false };
        if (CheckCell(x, z + 1, tag))
        {
            NeighbourCountBinair[0] = true;
            //Debug.Log("up");
        }
        if (CheckCell(x + 1, z, tag))
        {
            NeighbourCountBinair[1] = true;
            //Debug.Log("right");
        }
        if (CheckCell(x, z - 1, tag))
        {
            NeighbourCountBinair[2] = true;
            //Debug.Log("down");
        }
        if (CheckCell(x - 1, z, tag))
        {
            NeighbourCountBinair[3] = true;
            //Debug.Log("left");
        }

        return NeighbourCountBinair;

    }

    public List<GameObject> GetObject(int x, int z, string tag)
    {
        List<GameObject> Objects = new();

        Objects.Add(GetCellObj(x, z + 1, tag));
        Objects.Add(GetCellObj(x + 1, z, tag));
        Objects.Add(GetCellObj(x, z - 1, tag));
        Objects.Add(GetCellObj(x - 1, z, tag));

        return Objects;
    }
}
