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


                    cell.info = GetInfo(cell.objType, grid, x, y);

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

    public void LoadObjects()
    {
        List<cells> grid = gameSave.map.grid;

        foreach (cells cell in grid)
        {
            Debug.Log("load object");
            if (cell != null)
            {
                GameObject scem = SetObject(cell);
                SetInfo(cell, scem);
            }
        }


        void SetInfo(cells cell, GameObject scem)
        {
            switch (cell.objType)
            {
                case ObjectTypes.DATAWIRE:
                    {
                        scem.GetComponent<dataWire>().wire = new Wires();
                        scem.GetComponent<dataWire>().wire.Settings(cell.info);
                    }
                    break;
                case ObjectTypes.DATAMINER:
                    {
                        scem.GetComponent<dataMiner>().miner = new Miner();
                        scem.GetComponent<dataMiner>().miner.Settings(cell.info);
                    }
                    break;
                case ObjectTypes.UPLOADSTATION:
                    {
                        scem.GetComponent<uploadStation>().station = new UploadStation();
                        scem.GetComponent<uploadStation>().station.Settings(cell.info);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public GameObject SetObject(cells cell)
    {
        GameObject[] placable = gridSys.GetInstance().placables;
        Debug.Log(placable);
        Debug.Log(getType());
        GameObject scem = gridSys.Instantiate(getType(), new Vector3(cell.x, 0.5f, cell.y), Quaternion.Euler(0, 0, 0));
        gridSys.grid[cell.x, cell.y].obj = scem;

        return scem;
        GameObject getType()
        {

            switch (cell.objType)
            {
                case ObjectTypes.DATAWIRE: return placable[0];
                case ObjectTypes.DATAMINER: return placable[1];
                case ObjectTypes.UPLOADSTATION: return placable[2];
                default:
                    return null;
            }
        }
    }
}
