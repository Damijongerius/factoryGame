using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMillBehavior : ITileBehavior
{
    private ITile parent;

    private float storedPower;

    private float powerGain = 1;
    private float storageLimit = 10;
    private float transmitAmount = 1.2f;
    public void Execute(ITile tile,object obj)
    {
        return;
    }

    public void Initialize(ITile tile)
    {
        parent = tile;
    }

    public void Run()
    {
        if(storedPower < powerGain + storageLimit)
        {
            storedPower += powerGain;
        }

        List<ITile> powerNeighbours = new List<ITile>();
        List<ITile> neighbours = parent.GetNeighbours();
        neighbours.ForEach(neighbour => {
            if(neighbour.GetType() == WorldObjects.Order.PowerCable) powerNeighbours.Add(neighbour);
        });

        powerNeighbours.ForEach(neighbour => {
            neighbour.RunBehavior(parent,Mathf.Min(storedPower, transmitAmount) / powerNeighbours.Count);
            storedPower -= Mathf.Min(storedPower, transmitAmount) / powerNeighbours.Count;
        });
    }
}
