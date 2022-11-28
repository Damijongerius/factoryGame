using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
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

                    Debug.Log(cell.objType);
                    try
                    {
                        Debug.Log(grid[x, 0, y].obj.GetComponent<dataWire>().wire);
                    }
                    catch { }
                    try
                    {
                        Debug.Log(grid[x, 0, y].obj.GetComponent<dataMiner>().miner);
                    }
                    catch { }
                    try
                    {
                        Debug.Log(grid[x, 0, y].obj.GetComponent<uploadStation>().station);
                    }
                    catch { }
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
            case ObjectTypes.DATAWIRE: return _grid[x, 0, y].obj.GetComponent<dataWire>().wire;
            case ObjectTypes.DATAMINER: return _grid[x, 0, y].obj.GetComponent<dataMiner>().miner;
            case ObjectTypes.UPLOADSTATION: return _grid[x, 0, y].obj.GetComponent<uploadStation>().station;
            default:
                break;
        }
        return null;
    }
    
    public Cell2[,,] LoadSavedObjects(Cell2[,,] grid2)
    {
        List<cells> grid = gameSave.map.grid;

        foreach (cells cell in grid)
        {

            grid2 = SetObject(cell, grid2);

            
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
        return grid2;
    }

    public Cell2[,,] SetObject(cells cell, Cell2[,,] grid)
    {
        GameObject[] placable = WorldManager.getInstance().placables;
        if (grid[cell.x, 0, cell.y].obj == null)
        {
            grid[cell.x, 0, cell.y].obj = WorldManager.Instantiate(getType(), new Vector3(cell.x, 0.5f, cell.y), Quaternion.Euler(0, 0, 0));
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

        return grid;
    }
}