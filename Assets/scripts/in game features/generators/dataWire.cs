using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataWire : MonoBehaviour
{
    public Wires wire = new Wires();

    public int priority;
    public int[] prioritys;

    public int dataStored;

    public List<int> sorted = new List<int>();
    public List<GameObject> sortedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Run), 1f, 1f);
        InvokeRepeating(nameof(Test), 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (wire.dataStored > 10)
            wire.dataStored--;

        priority = wire.SelfPrio;
        prioritys = wire.Prio;
        dataStored = wire.dataStored;
    }

    public void Run()
    {
        wire.updateSpeed--;

        bool[] directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataMiner");
        foreach (bool direction in directions)
        {
            if (direction) 
            {
                //Debug.Log("wire with power");
            }
        }
    }

    public void Test()
    {
        if(wire.dataStored > 0)
        {
            GiveData();
        }
    }

    public void GiveValues()
    {
        if (wire.updateSpeed <= 0)
        {
            wire.updateSpeed = 2;

            bool[] directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataWire");
            List<GameObject> wires = gridSys.grid[(int)transform.position.x, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
            for (int i = 0; i < wires.Count; i++)
            {
                if (wires[i] != null)
                {
                    if (wires[i].GetComponent<dataWire>().wire.updateSpeed <= 0)
                        wires[i].GetComponent<dataWire>().wire.SelfPrio = wire.SelfPrio + 1;
                    wire.Prio[i] = wires[i].GetComponent<dataWire>().wire.SelfPrio + wire.SelfPrio;
                    if (wires[i].GetComponent<dataWire>().wire.updateSpeed <= 0)
                    {
                        wires[i].GetComponent<dataWire>().GiveValues();
                    }

                }
            }
        }
    }

    public void GiveData()
    {
        

        List<GameObject> objects = gridSys.grid[(int)transform.position.x, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
        for(int i = 0; i < wire.Prio.Length; i++)
        {
            if (wire.Prio[i] > 0)
            {
                //Debug.Log(wire.Prio[i]);

                sorted.Add(wire.Prio[i]);
                sortedObjects.Add(objects[i]);
            }
        }
        //if(sorted != null)
        //{
        //    sorted.Sort();

        //    if(sorted[0] == sorted[1]) {
        //        for (int i = 0; i < sortedObjects.Count; i++)
        //        {
        //            if (sorted[0] == wire.Prio[i])
        //            {
        //                sortedObjects[i].GetComponent<dataWire>().wire.dataStored += 1;
        //                wire.dataStored -= 1;

        //            }
        //        }
        //    }
        //}
    }    
}
