using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataMiner : MonoBehaviour
{
    //public Cell cell;
    public Miner miner;

    public bool[] directions;
    public List<GameObject> objects;
    public List<GameObject> wires;

    public int sides = 0;
    public GameObject chosenWire;

    // Start is called before the first frame update
    void Awake()
    {
        // get grid pos
        if(miner == null)
        {
            miner = new Miner
            {
                powered = true
            };
        }
        InvokeRepeating(nameof(Run), 2f, 1.5f);
    }

    //run the following actions
    public void Run()
    {
        // looks for data wire in direction
        directions = WorldManager.getInstance().map.Grid[(int)transform.position.x,0, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataWire");
        objects = WorldManager.getInstance().map.Grid[(int)transform.position.x,0, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
        wires.Clear();
        foreach(GameObject obj in objects)
        {
            if (obj != null)
            {

                wires.Add(obj);
            }
        }
        if(wires.Count > 0)
        {
            sides++;
        }
        

        if(sides == 1)
        {
            chosenWire = wires[0];
        }
        else if(sides == 2)
        {
            chosenWire = wires[1];
        }
        else if (sides == 3)
        {
            chosenWire = wires[2];
        }
        else if (sides == 4)
        {
            chosenWire = wires[3];
        }

        if(sides == wires.Count)
        {
            sides = 0;
        }

        if (miner.powered)
        {
            if (miner.powerStored  < 10f)
            {
                miner.powerStored += 1.5f;
            }

            if(miner.powerStored > 2f  && miner.dataStored < 10f)
            {
                miner.powerStored -= 1.5f;
                miner.dataStored += 1;
            }

            if (miner.dataStored > 1f)
            {
                if (chosenWire != null)
                {
                    if(chosenWire.GetComponent<dataWire>().wire.dataStored < 10)
                    {
                        chosenWire.GetComponent<dataWire>().wire.dataStored += 1;

                        miner.dataStored -= 1;
                    }
                }
            }
        }
        else
        {
            miner.powered = true;
        }
    }
}
