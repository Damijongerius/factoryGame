using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataMiner : MonoBehaviour
{
    //public Cell cell;
    public Miner miner;

    // Start is called before the first frame update
    void Start()
    {
        // get grid pos
        miner = new Miner
        {
        powered = true     
        };

        InvokeRepeating(nameof(Run), 2f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // ask if powered
        if (miner.powered)
        {
        }
    }

    //run the following actions
    public void Run()
    {
        if (miner.powered)
        {
            miner.powerStored += 1.5f;

            if(miner.powerStored > 10f)
            {
                miner.powerStored -= 1f;
                miner.dataStored += 1;

            }

            if(miner.dataStored > 1f )
            {
                // looks for data wire in direction
                int directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataWire");
                //Debug.Log(direction);
               if(directions != 0)
                {
                    Debug.Log("I have some one that can take my power: " + directions);
                }
            }
        }
    }
}
