using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataMiner : MonoBehaviour
{
    public Cell cell;
    public ObjInfo objInfo;

    // Start is called before the first frame update
    void Start()
    {
        // get grid pos
        Cell cell = gridSys.grid[(int)transform.position.x, (int)transform.position.z];
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
               //int direction = ObjOnCell.seek((int)transform.position.x, (int)transform.position.z, "dataWire");
                //Debug.Log(direction);
                int x = (int)transform.position.x;
                int z = (int)transform.position.z;
                Cell cell = gridSys.grid[x++, z++];
                Debug.Log(cell.obj + "||" + x++ + "||" + z++ +" ||| " + x +"||"+ z );
               // if (direction != -1)
                //  {
                //    Debug.Log("ejow");
                //}
            }
        }
    }
}
