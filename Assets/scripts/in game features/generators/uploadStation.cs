using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uploadStation : MonoBehaviour
{
    
    void Start()
    {
        InvokeRepeating(nameof(TestSurround), 2f, 1.5f);
       
    }


    void Update()
    {
    }

    public void TestSurround()
    {
        bool[] directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "DataWire");
        //GameObject[] Wires = gridSys.grid[(int)transform.position.x, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "DataWire");
        // Wires wire = Wires[0].GetComponent<dataWire>().wire;
        //float power = wire.powerStored;
    }
}
