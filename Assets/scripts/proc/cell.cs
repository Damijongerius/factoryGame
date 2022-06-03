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

    public int CheckNeighbour(int x, int z, string tag)
    {
        int NeighbourCountBinair = 0;
        if(CheckCell(x, z + 1, tag))
        {
            NeighbourCountBinair += 1;
        }
        if (CheckCell(x + 1, z, tag))
        {
            NeighbourCountBinair += 2;
        }
        if (CheckCell(x, z - 1, tag))
        {
            NeighbourCountBinair += 4;
        }
        if (CheckCell(x - 1, z, tag))
        {
            NeighbourCountBinair += 8;
        }

        return NeighbourCountBinair;

    }

    
}

public class ObjInfo
{
    public bool powered;
    public bool exitPoint;

    [Range(0f, 100f)]
    public float powerStored;

    [Range(0, 50)]
    public int dataStored;

    public int Level;

    public int dataMined;
    public int age;

    public int upkeepCost;
}

