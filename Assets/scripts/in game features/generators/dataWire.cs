using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataWire : MonoBehaviour
{
    public Wires wire = new Wires();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Run), 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (wire.dataStored > 10)
            wire.dataStored--;
    }

    public void Run()
    {
        Debug.Log(wire.SelfPrio);
        bool[] directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataMiner");
        foreach (bool direction in directions)
        {
            if (direction) 
            {
                //Debug.Log("wire with power");
            }
        }
    }

    public void GiveValues()
    {
        wire.updateSpeed--;
        if (wire.updateSpeed <= 0)
        {
            Debug.Log("run datawire");
            wire.updateSpeed = 20;


            List<GameObject> wires = gridSys.grid[(int)transform.position.x, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
            for (int i = 0; i < wires.Count; i++)
            {
                wire.Prio[i] = wires[i].GetComponent<dataWire>().wire.SelfPrio;
                //wires[i].GetComponent<dataWire>().wire.prio
            }
        }
    }

}
