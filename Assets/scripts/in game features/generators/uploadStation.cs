using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uploadStation : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating(nameof(Run), 2f, 1.5f);
       
    }


    void Update()
    {
    }

    public void Run()
    {
        List<GameObject> wires = gridSys.grid[(int)transform.position.x, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
        foreach(GameObject wire in wires)
        {
            wire.GetComponent<dataWire>().wire.SelfPrio = 0;
            Debug.Log("done");
            wire.GetComponent<dataWire>().GiveValues();
        }

    }
}
