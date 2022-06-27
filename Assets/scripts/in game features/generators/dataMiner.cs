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

    public bool[] directions;
    public List<GameObject> wires;
    public List<GameObject> notNull;

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

        directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataWire");
        wires = gridSys.grid[(int)transform.position.x, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
        notNull = null;
        sides = -1;
        for(int i = 0;i < directions.Length; i++)
        {
            if (directions[i])
            {
                sides++;
                notNull.Add(wires[i]);
                //Debug.Log(wires[i]);
            }
                
        }
        if(sides > nextSide)
        {
            nextSide++;
        }
        if(nextSide == 0)
        {
            chosenWire = notNull[0];
            Debug.Log("summon0");
        }
        if (nextSide == 1)
        {
            chosenWire = notNull[1];
            Debug.Log("summon1");
        }
        if (nextSide == 2)
        {
            chosenWire = notNull[2];
            Debug.Log("summon2");
        }
        if (nextSide == 3)
        {
            chosenWire = notNull[3];
            Debug.Log("summon3");
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
                if (chosenWire)
                {
                    chosenWire.GetComponent<dataWire>().wire.dataStored += 1;
                    Debug.Log("gived");
                }
                miner.dataStored -= 1;

            }
        }
    }
}
