using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Jobs.LowLevel.Unsafe;

// save en load class voor het saven van een profiel/lijst van profielen en laden
public class JsonSaveLoad
{

    public SaveFile gameSave = SaveFile.GetInstance();
    //bestaat de file directory zo niet maakt hij hem ook meteen aan
    public void Exsisting(string _name)
    {

        if (!Directory.Exists(Application.persistentDataPath + "/profile/" + _name))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/profile/" + _name);
            Debug.Log(Application.persistentDataPath + "/profile/" + _name);

        }

    }

    //hij maakt een nieuw profiel en zet die ook meteen als profiel in game
    //daarnaast zet die hem ook in de lijst
    public void CreateSaveData(string _name)
    {
        gameSave.profile = new Profile();
        gameSave.profile.Name = _name;
        gameSave.profile.DateMade = System.DateTime.Now;
        gameSave.map = new Map();
        ListProfile(_name);
    }


    //stopt alle in game data in de file
    public void Save(string _name)
    {
        
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/profile/" + _name + "/SaveFile.Factory", FileMode.OpenOrCreate, FileAccess.Write))
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter writer = new StreamWriter(fs))
            {
                serializer.Serialize(writer, gameSave);
            }
        }
    }

    //haalt alle data uit de files
    public void Load(string _name)
    {
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/profile/" + _name + "/SaveFile.Factory", FileMode.OpenOrCreate, FileAccess.Read))
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader reader = new StreamReader(fs))
            {
                string temp = reader.ReadToEnd();
                Debug.Log(temp);
                new SaveFile(JsonConvert.DeserializeObject<SaveFile>(temp));
            }
        }
    }

    //zet het nieuw aangemaakte profiel in de lijst van profielen
    public void ListProfile(string _name)
    {
        Listed listed = new Listed();

            List<string> profiles = ReadListedProfiles();
            try
            {
                foreach (string profile in profiles)
                {
                    listed.profiles.Add(profile);
                }
            }
            catch
            {
                Debug.Log("no profiles saved in file");
            }    
            listed.profiles.Add(_name);


            using (FileStream fs2 = new FileStream(Application.persistentDataPath + "/profile/Profiles.Manager", FileMode.OpenOrCreate, FileAccess.Write))
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamWriter writer = new StreamWriter(fs2))
                {
                    serializer.Serialize(writer, listed);
                }
            }
        
    }
    
    //vraagt om alle profielen in de file
    public List<string> ReadListedProfiles()
    {
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/profile/Profiles.Manager", FileMode.OpenOrCreate, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                string json = reader.ReadToEnd();

                Listed listed = JsonUtility.FromJson<Listed>(json);
                try
                {
                    return listed.profiles;
                }
                catch
                {
                    return null;
                }
            }
        }
    }

    public void SaveObjects()
    {
        Cell[,] grid = gridSys.grid;
        List<cells> newGrid = new();
        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x,y].obj != null)
                {
                    cells cell = new cells();

                    cell.x = x;
                    cell.y = y;

                    cell.objType = GetType(grid[x,y].obj.tag);


                    cell.info = GetInfo(cell.objType, grid, x, y);

                    newGrid.Add(cell);
                } 
            }
        }

        gameSave.map.grid.grid = newGrid;
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

    public ObjInfo GetInfo(ObjectTypes _objType, Cell[,] _grid, int x,int y)
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
        Debug.Log("load");
        List<cells> grid = gameSave.map.grid.grid;

            Debug.Log(grid.Count);
        foreach (cells cell in grid)
        {
            if (cell != null)
            {
                Debug.Log("not null");
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
