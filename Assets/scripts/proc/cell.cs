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
        Cell cell0 = gridSys.grid[x, z++];
        Cell cell1 = gridSys.grid[x++, z];
        Cell cell2 = gridSys.grid[x--, z];
        Cell cell3 = gridSys.grid[x, z--];
        return cell0.obj.CompareTag(tag) ? 0 : (cell1.obj.CompareTag(tag) ? 1 : (cell2.obj.CompareTag(tag) ? 2 : (cell3.obj.CompareTag(tag) ? 3 : -1)));
    }
}
