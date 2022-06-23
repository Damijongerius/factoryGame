using UnityEngine;

public class Cell
{
    public bool isWater;
    public bool bought;


    public GameObject obj;

    public Cell(bool isWater)
    {
        this.isWater = isWater;

    }


    public bool CheckCell(int x, int z, string tag)
    {
        if (gridSys.grid[x, z].obj != null)
        {
            if (gridSys.grid[x, z].obj.CompareTag(tag))
            {
                return true;
            }
            return false;
        }
        return false;
    }

    public GameObject GetCellObj(int x, int z, string tag)
    {
        if (gridSys.grid[x, z].obj != null)
        {
         return gridSys.grid[x, z].obj;
        }
        return null;
    }

    public bool[] CheckNeighbour(int x, int z, string tag)
    {
        bool[] NeighbourCountBinair = new bool[4] { false, false, false, false};
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

    public GameObject[] CheckPowered(int x, int z, string tag)
    {
        GameObject[] PoweredObjs = new GameObject[4] {null,null,null,null};

        bool[] Neighbours = CheckNeighbour(x, z, tag);
        if (Neighbours[0])
        {
            PoweredObjs[0] = GetCellObj(x, z + 1, tag);
        }
        if (Neighbours[1])
        {
            PoweredObjs[1] = GetCellObj(x + 1, z, tag);
        }
        if (Neighbours[2])
        {
            PoweredObjs[2] = GetCellObj(x, z - 1, tag);
        }
        if (Neighbours[3])
        {
            PoweredObjs[3] = GetCellObj(x - 1, z, tag);
        }

        return PoweredObjs;
    }

    
}


