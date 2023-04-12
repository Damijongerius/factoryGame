using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour
{
    public WorldObjects.WorldObjects world;

    private GameObject Inventory;
    private GameObject Content;
    private List<Image> ContentList;
    private Dictionary<string, GameObject> newContent;


    public void Awake()
    {
        Inventory = transform.GetChild(0).gameObject;
        Content = transform.GetChild(1).gameObject;

        ContentList = Content.transform.GetComponentsInChildren<Image>().ToList();
    }

    public void OnItemClick(string type)
    {
        if (Content.activeSelf)
        {
            Inventory.SetActive(false);
            Content.SetActive(false);
            return;
        }

        Inventory.SetActive(true);
        Content.SetActive(true);

        PlacementType pl = (PlacementType)Enum.Parse(typeof(PlacementType), type, true);
        switch (pl)
        {
            case PlacementType.Factories : newContent = world.GetFactory(); break;
            case PlacementType.Electrical : newContent = world.GetElectrical(); break;
            case PlacementType.Storages : newContent = world.GetStorages(); break;
            case PlacementType.Data: newContent = world.GetData(); break;
            case PlacementType.PowerSources: newContent = world.GetPowerSources(); break;
            case PlacementType.Water: newContent = world.GetWater(); break;
            default:
                break;
        }

        foreach (var Placeable in newContent)
        {
            Debug.Log($"{Placeable.Key}: {Placeable.Value}");
        }
    }

    public void OnButtonClick(int id)
    {
        var items = newContent.ToArray();
        if(items != null && id < items.Length)
        {
            Debug.Log($"{items[id].Key}: {items[id].Value}");
            PlacementManager.GetInstance().AddPlacable(items[id].Value);
        }
       
    }


}
