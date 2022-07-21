using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

// save en load class voor het saven van een profiel/lijst van profielen en laden
public class JsonSaveLoad
{
    //bestaat de file directory zo niet maakt hij hem ook meteen aan
    public static void Exsisting(string _name)
    {

        if (!Directory.Exists(Application.persistentDataPath + "/profile/" + _name))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/profile/" + _name);
            Debug.Log(Application.persistentDataPath + "/profile/" + _name);

        }

    }

    //hij maakt een nieuw profiel en zet die ook meteen als profiel in game
    //daarnaast zet die hem ook in de lijst
    public static void CreateSaveData(string _name)
    {
        SaveFile.saveFile.profile = new Profile();
        SaveFile.saveFile.profile.Name = _name;
        SaveFile.saveFile.profile.DateMade = System.DateTime.Now;
        SaveFile.saveFile.map = new Map();
        ListProfile(_name);
    }


    //stopt alle in game data in de file
    public static void Save(string _name)
    {
        
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/profile/" + _name + "/SaveFile.Factory", FileMode.OpenOrCreate, FileAccess.Write))
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter writer = new StreamWriter(fs))
            {
                serializer.Serialize(writer, SaveFile.saveFile);
            }
        }
    }

    //haalt alle data uit de files
    public static void Load(string _name)
    {
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/profile/" + _name + "/SaveFile.Factory", FileMode.OpenOrCreate, FileAccess.Read))
        {
            using(StreamReader reader = new StreamReader(fs))
            {
                string json = reader.ReadToEnd();
                SaveFile saveFile = JsonUtility.FromJson<SaveFile>(json);
            }
        }
    }

    //zet het nieuw aangemaakte profiel in de lijst van profielen
    public static void ListProfile(string _name)
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
    public static List<string> ReadListedProfiles()
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
}
