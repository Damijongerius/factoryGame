using UnityEngine;

public class Cell
{
    public bool isWater;
    public GameObject obj;
    public bool bought;

    public Cell(bool isWater)
    {
        this.isWater = isWater;
    }
}
