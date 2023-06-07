using System.Collections;
using System.Collections.Generic;
using System.Web.Compilation;
using UnityEngine;

public class FactoryBehavior : ITileBehavior
{
    private ITile[,] items = new ITile[4,4];
    private float powerStorage;
    private float powerStored;
    private float processpower;
    private float dataStorage;
    private float dataStored;


    public void Execute(ITile tile, object obj)
    {
        //add tile
        if(obj is ITile)
        {
            
        }

        //add power
        if(obj is float)
        {
            if (powerStorage == 0) return;

            if(powerStored <= powerStorage + (float)obj)
            {

            }
        }
    }

    public void Initialize(ITile tile)
    {
        throw new System.NotImplementedException();
    }

    public void Run()
    {
        throw new System.NotImplementedException();
    }
}
