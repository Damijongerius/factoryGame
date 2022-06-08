using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataMiner : MonoBehaviour
{
    //public Cell cell;
    public Miner miner;

    public enum dir { north, east, south, west };
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
                switch (directions)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 8:
                        {
                            break;
                        }
                    case 9:
                        {
                            break;
                        }
                    case 10:
                        {
                            break;
                        }
                    case 11:
                        {
                            break;
                        }
                    case 12:
                        {
                            break;
                        }
                    case 13:
                        {
                            break;
                        }
                    case 14:
                        {
                            break;
                        }
                    case 15:
                        {
                            break;
                        }
                }
            }
        }
    }
}
