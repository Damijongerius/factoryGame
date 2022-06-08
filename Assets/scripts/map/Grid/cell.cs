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

    public int[] CheckNeighbour(int x, int z, string tag)
    {
        int[] NeighbourCountBinair = new int[4];
        if(CheckCell(x, z + 1, tag))
        {
            NeighbourCountBinair[0] += 1;
        }
        if (CheckCell(x + 1, z, tag))
        {
            NeighbourCountBinair[1] += 1;
        }
        if (CheckCell(x, z - 1, tag))
        {
            NeighbourCountBinair[2] += 1;
        }
        if (CheckCell(x - 1, z, tag))
        {
            NeighbourCountBinair[3] += 1;
        }

        return NeighbourCountBinair;

    }

    
}


