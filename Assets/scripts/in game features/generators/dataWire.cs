using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataWire : MonoBehaviour
{
    public Wires wires;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Run), 2f, 1.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run()
    {
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
