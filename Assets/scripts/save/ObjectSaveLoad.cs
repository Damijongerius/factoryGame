using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using UnityEngine;
using World;

public class ObjectSaveLoad
{

    private SaveFile sf = SaveFile.GetInstance();
    private PlacementManager placementManager = PlacementManager.GetInstance();
    private NavigationBar navBar = NavigationBar.GetInstance();
    public void LoadObjects()
    {
        TileObject[] objects = sf.map.GetPreparedObjects();

        foreach(TileObject tileObj in objects)
        {
            GameObject obj = navBar.getObject(tileObj.order);
            var answer = WorldObjects.Order.StoryFactory1;
            Enum.TryParse<WorldObjects.Order>(tileObj.order, out answer);
            placementManager.PlaceItem(obj,answer,tileObj.x,tileObj.y);
        }
    }
}