using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSaveLoad
{
    public SaveFile gameSave = SaveFile.GetInstance();
    public void SaveObjects()
    {
        Cell[,] grid = gridSys.grid;
        List<cells> newGrid = new();
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y].obj != null)
                {
                    cells cell = new cells();

                    cell.x = x;
                    cell.y = y;

                    cell.objType = GetType(grid[x, y].obj.tag);


                    cell.ObjInfo = GetInfo(cell.objType, grid, x, y);

                    newGrid.Add(cell);
                }
            }
        }

        gameSave.map.grid = newGrid;
    }

    public ObjectTypes GetType(string _tag)
    {
        return _tag switch
        {
            "dataMiner" => ObjectTypes.DATAMINER,
            "dataWire" => ObjectTypes.DATAWIRE,
            "uploadStation" => ObjectTypes.UPLOADSTATION,
            _ => ObjectTypes.DATAWIRE,
        };
    }

    public ObjInfo GetInfo(ObjectTypes _objType, Cell[,] _grid, int x, int y)
    {
        switch (_objType)
        {
            case ObjectTypes.DATAWIRE: return _grid[x, y].obj.GetComponent<dataWire>().wire;
            case ObjectTypes.DATAMINER: return _grid[x, y].obj.GetComponent<dataMiner>().miner;
            case ObjectTypes.UPLOADSTATION: return _grid[x, y].obj.GetComponent<uploadStation>().station;
            default:
                break;
        }
        return null;
    }
    
    public void LoadSavedObjects()
    {
        List<cells> grid = gameSave.map.grid;
        foreach (cells cell in grid)
        {

            SetObject(cell);
            SetInfo(cell, gridSys.grid[cell.x, cell.y].obj);
        }

        void SetInfo(cells cell, GameObject scem)
        {
            switch (cell.objType)
            {
                case ObjectTypes.DATAWIRE:
                    {
                        scem.GetComponent<dataWire>().wire = new Wires();
                        scem.GetComponent<dataWire>().wire.Settings(cell.ObjInfo);
                        scem.tag = "dataWire";
                    }
                    break;
                case ObjectTypes.DATAMINER:
                    {
                        scem.GetComponent<dataMiner>().miner = new Miner();
                        scem.GetComponent<dataMiner>().miner.Settings(cell.ObjInfo);
                    }
                    break;
                case ObjectTypes.UPLOADSTATION:
                    {
                        scem.GetComponent<uploadStation>().station = new UploadStation();
                        scem.GetComponent<uploadStation>().station.Settings(cell.ObjInfo);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void SetObject(cells cell)
    {
        GameObject[] placable = gridSys.GetInstance().placables;
        if(gridSys.grid[cell.x, cell.y].obj == null)
        {
            gridSys.grid[cell.x, cell.y].obj = gridSys.Instantiate(getType(), new Vector3(cell.x, 0.5f, cell.y), Quaternion.Euler(0, 0, 0));
        }
        

        GameObject getType()
        {
            return cell.objType switch
            {
                ObjectTypes.DATAWIRE => placable[0],
                ObjectTypes.DATAMINER => placable[1],
                ObjectTypes.UPLOADSTATION => placable[2],
                _ => null,
            };
        }
    }
}
