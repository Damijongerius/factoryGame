using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataMiner : MonoBehaviour
{
    //public Cell cell;
    public Miner miner;
    public int sides;
    public int nextSide;
    public GameObject chosenWire;

    public enum dir { north, east, south, west };
    // Start is called before the first frame update
    void Start()
    {
        // get grid pos
        miner = new Miner
        {
        powered = true     
        };
        
        nextSide = -1;
        InvokeRepeating(nameof(Run), 2f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // ask if powered
    }

    //run the following actions
    public void Run()
    {
        // looks for data wire in direction

        bool[] directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataWire");
        List<GameObject> wires = gridSys.grid[(int)transform.position.x, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
        sides = -1;
        foreach (bool d in directions)
        {
            if (d)
            {
                sides++;

            }
        }
        if(sides >= 0)
        {
            nextSide++;
        }
        if(nextSide == 0)
        {
            chosenWire = wires[0];
        }
        if (nextSide == 1)
        {
            chosenWire = wires[1];
        }
        if (nextSide == 2)
        {
            chosenWire = wires[2];
        }
        if (nextSide == 3)
        {
            chosenWire = wires[3];
        }
        if(nextSide >= sides)
        {
            nextSide = -1;
        }

        if (miner.powered)
        {
            miner.powerStored += 1.5f;

            if(miner.powerStored > 10f)
            {
                miner.powerStored -= 1.5f;
                miner.dataStored += 1;

            }

            if (miner.dataStored > 1f)
            {
                chosenWire.GetComponent<dataWire>().wire.dataStored += 1;
                miner.dataStored -= 1;

            }
        }
    }
}
