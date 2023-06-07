using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldObjects;

public class PowerLineBehavior : ITileBehavior
{
    private ITile parent;




    public void Execute(ITile tile, object obj)
    {
        
    }

    public void Initialize(ITile tile)
    {
        parent = tile;
    }

    public void Run()
    {
        //all the stupid behavior that is gong to take a shit ton of time :)
        if(parent == null && parent.GetNeighbours().Count != 0)
        {
            Debug.Log("no parent/neighbours found line 21 PowerLineBehavior");
            return;
        }

        List<ITile> passtrough = new List<ITile>();
        List<ITile> change = new List<ITile>();
        foreach (ITile neighbour in parent.GetNeighbours())
        {
            switch (neighbour.GetType())
            {
                case Order.StoryFactory1:
                    passtrough.Add(neighbour);
                    break;
                case Order.PowerDemper:
                case Order.PowerAmplifier:
                case Order.PowerMerger:
                case Order.PowerSplitter:
                    change.Add(neighbour);
                    break;
            }
        }
        Passing pass = new((ITile)this, 1 / (passtrough.Count + change.Count));
        foreach(ITile neighbour in passtrough)
        {
            neighbour.RunBehavior(parent,(object)(1 / (passtrough.Count + change.Count)));
        }

        foreach(ITile neighbour in change)
        {
            neighbour.RunBehavior(parent,(object)pass);
        }
    }
    class Passing
    {
        public Passing(ITile _tile, float _amount) 
        {
            Tile = _tile;
            Amount = _amount;
        }
        public ITile Tile { get; set;}
        public float Amount { get; set;}
    }
}
