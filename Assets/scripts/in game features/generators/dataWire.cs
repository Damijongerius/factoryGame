using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataWire : MonoBehaviour
{
    public Wires wire = new Wires();
    public int random = 1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Run), 2f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (wire.dataStored > 10)
            wire.dataStored--;
    }

    public void Run()
    {
        Debug.Log(wire.dataStored);
        bool[] directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataMiner");
        foreach (bool direction in directions)
        {
            if (direction) 
            {
                //Debug.Log("wire with power");
            }
        }
    }
}
