using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public static int chunksize;

    private int x;
    private int y;
    public IEnumerable<IBunk> Bunks = new List<IBunk>();
    public HashSet<Cell> Cells = new HashSet<Cell>();

    // // \\ // \\ // \\
    public Chunk(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    // \\ // \\ // \\ //

    //if someone needs a cell it wil get it if non existing create one
    // // \\ // \\ // \\
    public Cell GetCell(int x, int y)
    {
        if(Cells.Count > 0)
        {
            foreach(Cell cell in Cells)
                if (cell.x == x && cell.y == y) return cell;
        }
        

        Cell newCell = new Cell();
        newCell.x = x;
        newCell.y = y;

        Cells.Add(newCell);
        return newCell;
    }
    // \\ // \\ // \\ //

    // check for existing cell
    // // \\ // \\ // \\
    public bool Exists(int x, int y)
    {
        foreach (Cell cell in Cells)
            if (cell.x == x && cell.y == y) return true;
        
        return false;
    }
    // \\ // \\ // \\ //

    //try to see if x and y are in chunk range
    // // \\ // \\ // \\
    public bool InRange(int _x, int _y)
    {
        if (_x == x && _y == y)
        {
            return true;
        }
        return false;
    }
    // \\ // \\ // \\ //


    //find bunk of cell
    // // \\ // \\ // \\
    public IBunk GetCellBunk(Cell _cell)
    {
        foreach(IBunk bunk in Bunks)
        {
            
        }
        return null;
    }
    // \\ // \\ // \\ //
}
