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
        cell = gridSys.grid[x, z];
        if(cell.obj != null)
        {
            
            Cell cell0 = gridSys.grid[x++, z];
            Cell cell1 = gridSys.grid[x, z++];
            Cell cell2 = gridSys.grid[x--, z];
            Cell cell3 = gridSys.grid[x, z++];
            if (cell0.obj.tag == tag)
            {
                return 0;
            }
            else if(cell1.obj.tag == tag)
            {
                return 1;
            }
            else if(cell2.obj.tag == tag)
            {
                return 2;
            }
            else if(cell3.obj.tag == tag)
            {
                return 3;
            }
        }
        return -1;
    }
}
