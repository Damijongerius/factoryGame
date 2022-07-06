using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRootAttribute("PlayerData", Namespace = "FactoryGame",
IsNullable = false)]
public class Tmp
{
    [XmlAttribute]
    public string Name;
    public string Description;

    [XmlElementAttribute(IsNullable = false)]
    public string extraInfo;
}
public class SaveLoad
{
    public static SaveLoad sl = new SaveLoad();
    public static void Exsisting(string directory)
    {
        
        if(!Directory.Exists(directory))
        {
            //sl.
        }
        
    }

    private void CreateP(string directory)
    {
        XmlSerializer serializer =
        new XmlSerializer(typeof(GameManager.Profile));
        TextWriter writer = new StreamWriter(directory);
        GameManager.Profile profile = new GameManager.Profile();


    }
}
