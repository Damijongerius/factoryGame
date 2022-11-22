using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSaveLoad
{
    public SaveFile gameSave = SaveFile.GetInstance();
    public void SaveObjects()
    {
        Cell2[,,] grid = WorldManager.getInstance().map.Grid;
        List<cells> newGrid = new();
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(2); y++)
            {
                if (grid[x,0, y].obj != null)
                {
                    cells cell = new cells();

                    cell.x = x;
                    cell.y = y;

                    cell.objType = GetType(grid[x, 0, y].obj.tag);


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

    public ObjInfo GetInfo(ObjectTypes _objType, Cell2[,,] _grid, int x, int y)
    {
        switch (_objType)
        {
            case ObjectTypes.DATAWIRE: return _grid[x,0, y].obj.GetComponent<dataWire>().wire;
            case ObjectTypes.DATAMINER: return _grid[x,0, y].obj.GetComponent<dataMiner>().miner;
            case ObjectTypes.UPLOADSTATION: return _grid[x,0, y].obj.GetComponent<uploadStation>().station;
            default:
                break;
        }
        return null;
    }
    
    public void LoadSavedObjects(Cell2[,,] grid2)
    {
        WorldManager.getInstance().hallo();
        List<cells> grid = gameSave.map.grid;

        foreach (cells cell in grid)
        {

            SetObject(cell);

            SetInfo(cell, grid2[cell.x, 0, cell.y].obj);
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
        GameObject[] placable = WorldManager.getInstance().placables;
        if(WorldManager.getInstance().map.Grid[cell.x,0, cell.y].obj == null)
        {
            WorldManager.getInstance().map.Grid[cell.x,0, cell.y].obj = WorldManager.Instantiate(getType(), new Vector3(cell.x, 0.5f, cell.y), Quaternion.Euler(0, 0, 0));
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
