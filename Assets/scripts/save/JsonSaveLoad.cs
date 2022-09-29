using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System;

// save en load class voor het saven van een profiel/lijst van profielen en laden
public class JsonSaveLoad
{
    public Listed listed = new Listed();
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

    //save file to filestream
    public bool Save(string _saveName, SaveFile _saveData)
    {
        string prePath = Application.persistentDataPath + "/profile/" + _saveName;

        if (!Directory.Exists(prePath))
        {
            Directory.CreateDirectory(prePath);
        }

        string path = Application.persistentDataPath + "/profile/" + _saveName + "/Save.saveFile";

        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

        BinaryWriter writer = new BinaryWriter(file);


        using (Aes personalAes = Aes.Create())
        {
            personalAes.Key = KeyCheck();
            personalAes.GenerateIV();

            string fileContent = JsonConvert.SerializeObject(_saveData);
            ProfileManager.getObject().StartSendCoroutine(fileContent);
            Debug.Log(fileContent);

            byte[] encrypted = EncryptBytes(fileContent, personalAes.Key, personalAes.IV);
            writer.Write(personalAes.IV.Length);
            Debug.Log(personalAes.IV.Length + "iv Written");
            Debug.Log(Encoding.UTF8.GetString(personalAes.IV));
            writer.Write(personalAes.IV);
            writer.Write(encrypted.Length);
            Debug.Log(encrypted.Length + "enc written");
            writer.Write(encrypted);

        }
        writer.Close();
        file.Close();

        return true;
    }

    //loads data stored in savefile
    public string Load(string _saveName)
    {
        listed.lastPlayed = _saveName;
        string path = Application.persistentDataPath + "/profile/" + _saveName + "/Save.saveFile";

        string decrypterdContent = null;
        if (!File.Exists(path))
        {
            return null;
        }

        FileStream file = File.Open(path, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(file);
        using (Aes personalAes = Aes.Create())
        {
            personalAes.Key = KeyCheck();

            try
            {

                int IVLength = reader.ReadInt32();
                byte[] IV = reader.ReadBytes(IVLength);

                int encryptedBytesLength = reader.ReadInt32();
                byte[] encryptedBytes = reader.ReadBytes(encryptedBytesLength);



                personalAes.IV = IV;
                decrypterdContent = DecryptBytes(encryptedBytes, personalAes.Key, personalAes.IV);
                reader.Close();
                file.Close();

                Debug.Log(decrypterdContent);
                SaveFile temp = JsonConvert.DeserializeObject<SaveFile>(decrypterdContent);
                new SaveFile(temp);
                return decrypterdContent;

            }
            catch
            {
                reader.Close();
                file.Close();
                return null;
            }
        }
    }

    public void DeleteProfile(string _name)
    {
        string path = Application.persistentDataPath + "/profile/" + _name;

        Directory.Delete(path, true);

        DeleteListedProfile(_name);

        LoadListed.GetInstance().UpdateList();
    }

    public void DeleteListedProfile(string _name)
    {
        List<string> profiles = ReadListedProfiles().profiles;
        profiles.Remove(_name);

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

        using (FileStream fs2 = new FileStream(Application.persistentDataPath + "/profile/Profiles.Manager", FileMode.Create, FileAccess.Write))
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter writer = new StreamWriter(fs2))
            {
                serializer.Serialize(writer, listed);
            }
        }

    }

    //zet het nieuw aangemaakte profiel in de lijst van profielen
    public void ListProfile(string _name)
    {
        string temp = listed.lastPlayed;
        listed = new Listed();
        listed.lastPlayed = temp;

        List<string> profiles = ReadListedProfiles().profiles;
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
        listed.lastPlayed = _name;


        using (FileStream fs2 = new FileStream(Application.persistentDataPath + "/profile/Profiles.Manager", FileMode.Create, FileAccess.Write))
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter writer = new StreamWriter(fs2))
            {
                serializer.Serialize(writer, listed);
            }
        }
        
    }

    
    //vraagt om alle profielen in de file
    public Listed ReadListedProfiles()
    {
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/profile/Profiles.Manager", FileMode.OpenOrCreate, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                string json = reader.ReadToEnd();

                Listed listed = JsonUtility.FromJson<Listed>(json);
                try
                {
                    return listed;
                }
                catch(Exception e)
                {
                    Debug.Log(e);
                    return null;
                }
            }
        }
    }

    public static byte[] EncryptBytes(string _content, byte[] _key, byte[] _IV)
    {
        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            // assign the keys;
            aesAlg.Key = _key;
            aesAlg.IV = _IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write bytes to the stream.
                        swEncrypt.Write(_content);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public static string DecryptBytes(byte[] _cipherText, byte[] _Key, byte[] _IV)
    {

        string content;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = _Key;
            aesAlg.IV = _IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(_cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {

                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        content = srDecrypt.ReadToEnd();


                    }
                }
            }
        }

        return content;
    }

    public static byte[] KeyCheck()
    {
        string prePath = Application.persistentDataPath + "/saves";

        if (!Directory.Exists(prePath))
        {
            Directory.CreateDirectory(prePath);
        }
        string path = Application.persistentDataPath + "/saves/key.Lock";


        if (File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(file);
            byte[] key = reader.ReadBytes(16);
            reader.Close();
            file.Close();


            return key;
        }
        else
        {
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(file);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateKey();
                writer.Write(aesAlg.Key);
                writer.Close();
                file.Close();
                return aesAlg.Key;
            }
        }





    }
}
