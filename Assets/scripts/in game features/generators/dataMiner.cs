using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataMiner : MonoBehaviour
{
    //public Cell cell;
    public ObjInfo objInfo;

    // Start is called before the first frame update
    void Start()
    {
        // get grid pos
        objInfo = new ObjInfo
        {
            powered = true
        };

        InvokeRepeating(nameof(Run), 2f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // ask if powered
        if (objInfo.powered)
        {
        }
    }

    //run the following actions
    public void Run()
    {
        if (objInfo.powered)
        {
            objInfo.powerStored += 1.5f;

            if(objInfo.powerStored > 10f)
            {
                objInfo.powerStored -= 1f;
                objInfo.dataStored += 1;

            }

            if(objInfo.dataStored > 1f )
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
