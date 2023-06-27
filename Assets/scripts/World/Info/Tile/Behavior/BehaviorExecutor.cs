using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using World;
using static UnityEngine.Rendering.DebugUI;

public class BehaviorExecutor : MonoBehaviour
{

    //calculated values
    //these values could change alot
    // //\\//\\//\\
    private float dataOutagePH;
    private float powerCost;
    private float upkeepCost;
    private static float actualDataOutage;
    private float powerOutage;
    private float powerStorageCap;
    private float dataStorageCap;
    private float uploadSpeed;
    private float actualMoneyOutage;
    // \\//\\//\\//

    private World.World world = World.World.GetInstance();
    private SaveFile saveFile = SaveFile.GetInstance();

    /*
     * subscribed to change delegate
     * roept behavior aan en bereken alles om naar waardes waarmee ik alle profeit etc kan neerzetten
     */

    private void Start()
    {
        world.change += async () => { await Task.Run(OnChange); };
        InvokeRepeating(nameof(ApplyValues), 1f, 1f);
    }
    private void ApplyValues()
    {
         
        StatisticsBar.SetDataPerHour(actualDataOutage);
        StatisticsBar.UpkeepPerHour(upkeepCost);
        StatisticsBar.SetMoneyPerHour(actualMoneyOutage);

        saveFile.profile.Statistics.Money += (long)actualMoneyOutage;
        saveFile.profile.Statistics.Money -= (long)upkeepCost;
    }


    private async Task<Task> OnChange()
    {
        /*
         * this will get all the dictionaries of data meaning
         * an object will store a dictionary of String and float
         * <"dataOutage", 1>
         * <"powerCost", 1>
         * <"upkeepCost", 0.1>
         */
        List<ITile> tiles = world.tiles;

        Debug.Log(tiles.Count + " :count");
        foreach (ITile tile in tiles)
        {
            Dictionary<string, float> singlePair = tile.Getbehavior().GetData();

            float value;
            if (singlePair.TryGetValue("dataOutage", out value))
            {
                dataOutagePH += value;
            }

            if (singlePair.TryGetValue("powerCost", out value))
            {
                powerCost += value;
            }

            if (singlePair.TryGetValue("upkeepCost", out value))
            {
                upkeepCost += value;
            }

            if (singlePair.TryGetValue("powerOutage", out value))
            {
                powerOutage += value;
            }

            if (singlePair.TryGetValue("powerstorageCap", out value))
            {
                powerStorageCap += value;
            }

            if (singlePair.TryGetValue("dataStorageCap", out value))
            {
                dataStorageCap += value;
            }

            if (singlePair.TryGetValue("uploadSpeed", out value))
            {
                uploadSpeed += value;
            }

            singlePair.Clear();
        }

        // (cost / 100) * input = effectiveness || effectiveness X outage
        actualDataOutage = MathF.Min((100 / powerCost) * powerOutage, 100) * dataOutagePH /100; 
        actualMoneyOutage = MathF.Min((100 / actualDataOutage) * uploadSpeed, 100) * actualDataOutage / 100;
        if (uploadSpeed == 0)
        {
            actualMoneyOutage = 0;
        }
        Debug.Log(actualMoneyOutage + "- uploadSpeed" + uploadSpeed + "- dataOutage" + actualDataOutage);
        return Task.CompletedTask;
    }

    public static float GetDataOutage() => actualDataOutage;
}
