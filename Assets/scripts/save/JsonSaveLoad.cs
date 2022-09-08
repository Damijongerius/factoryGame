using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
                //string json = reader.ReadToEnd();
                // Debug.Log(json);
                //var format = "dd/MM/yyyy"; // your datetime format
                //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
                //SaveFile safefile = SaveFile.saveFile;
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
}
