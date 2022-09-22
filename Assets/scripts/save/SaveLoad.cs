//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Xml;
//using System.Xml.Serialization;
//using System.IO;

//[XmlRootAttribute("PlayerData", Namespace = "FactoryGame",
//IsNullable = false)]

//public class SaveLoad
//{
//    public static SaveLoad sl = new SaveLoad();
//    public static void Exsisting(string directory)
//    {
        
//        if(!Directory.Exists(Application.persistentDataPath + "/" + directory))
//        {
//            Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);
//            Debug.Log(Application.persistentDataPath + "/" + directory);
//        }
        
//    }

//    public static void CreateP(string directory, string Name)
//    {
        
//        XmlSerializer serializer = new XmlSerializer(typeof(Profile));
//        Profile profile = new Profile();
//        profile.Name = Name;
//        profile.DateMade = System.DateTime.Now;
//        SaveFile.saveFile.profile = profile;

//        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/" + directory))
//        {
//            serializer.Serialize(writer, profile);
//        }
//    }

//    public static void SaveP(string directory)
//    {
//        if (!File.Exists(directory))
//        {
//        }
//        else
//        {
//            using (FileStream fs = new FileStream(Application.persistentDataPath + "/" + directory, FileMode.OpenOrCreate, FileAccess.Write))
//            {
//                XmlSerializer serializer = new XmlSerializer(typeof(Profile));
//                Profile profile = SaveFile.saveFile.profile

//                using (StreamWriter writer = new StreamWriter(fs))
//                {
//                    serializer.Serialize(writer, profile);
//                }
//            }
//        }
//    }

//    public static void CreateM(string directory)
//    {
//        XmlSerializer serializer = new XmlSerializer(typeof(Map));
//        GameObject.FindWithTag("gridsystem").GetComponent<gridSys>().Generate();
//        Map map = new Map();
//        ProfileManager.created = true;
//        GameManager.map = map;

//        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/" + directory))
//        {
//            serializer.Serialize(writer, map);
//        }
//    }


//    public static void SaveM(string directory)
//    {
//        if (!File.Exists(Application.persistentDataPath + "/" + directory))
//        {
//            using (FileStream fs = new FileStream(Application.persistentDataPath + "/" + directory, FileMode.OpenOrCreate, FileAccess.Write))
//            {
//                XmlSerializer serializer = new XmlSerializer(typeof(Map));
//                Map map = GameManager.map;
//                using (StreamWriter writer = new StreamWriter(fs))
//                {
//                    serializer.Serialize(writer, map);
//                }
//            }
//        }
//        else
//        {
//            using (FileStream fs = new FileStream(Application.persistentDataPath + "/" + directory, FileMode.OpenOrCreate, FileAccess.Write))
//            {
//                XmlSerializer serializer = new XmlSerializer(typeof(Map));
//                Map map = GameManager.map;
//                try
//                {
//                    map.grid.grid = Order(map.grid.grid, gridSys.grid);
//                }
//                catch
//                {
//                }
//                Debug.Log("hey");
//                using (StreamWriter writer = new StreamWriter(fs))
//                {
//                    serializer.Serialize(writer, map);
//                }
//            }
//        }
//        //Order(map.grid.grid, gridSys.grid);

//        List<cells> Order(List<cells> grid, Cell[,] gridCells)
//        {
//            for (int x = 0; x < 100; x++)
//            {
//                for (int y = 0; y < 100; y++)
//                {
//                    if (gridCells[x, y].obj != null)
//                    {
//                        cells cell = new cells();
//                        cell.x = x;
//                        cell.y = y;

//                        grid.Add(cell);
//                    }
//                }
//            }
//            return grid;
//        }

//    }
//}
