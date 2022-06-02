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

public class ObjOnCell
{
    //seek for obj next to this obj
    // 0up 1right 2down 3left
    public static int seek(int x, int z, string tag)
    {
        Cell cell;
        //cell = gridSys.grid[x, z];

            cell = gridSys.grid[x++, z];
        if (cell.obj.CompareTag(tag))
            return 0;
        else
            cell = gridSys.grid[x++, z];
        if (cell.obj.CompareTag(tag))
            return 1;
        else
            cell = gridSys.grid[x++, z];
        if (cell.obj.CompareTag(tag))
            return 2;
        else
            cell = gridSys.grid[x++, z];
        if (cell.obj.CompareTag(tag))
            return 3;
        else
            return -1;
    }
}
