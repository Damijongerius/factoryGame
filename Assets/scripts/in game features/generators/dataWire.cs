//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class dataWire : MonoBehaviour
//{
//    public Wires wire = new Wires();

//    public int priority;
//    public int[] prioritys;

//    public int dataStored;

//    public List<int> sorted = new List<int>();
//    public List<GameObject> uploadStations = new List<GameObject>();

//    private int StationNr;
//    private int selected; 
//    // Start is called before the first frame update
//    void Start()
//    {
//        InvokeRepeating(nameof(Run), 1f, 1f);
//        InvokeRepeating(nameof(Test), 1f, 1f);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (wire.dataStored > 10)
//            wire.dataStored--;

//        priority = wire.SelfPrio;
//        prioritys = wire.Prio;
//        dataStored = (int)wire.dataStored;
//    }

//    public void Run()
//    {
//        wire.updateSpeed--;
//    }

//    public void Test()
//    {
//        if(wire.dataStored > 0)
//        {
//            GiveData();
//        }
//    }

//    public void GiveValues()
//    {
//        if (wire.updateSpeed <= 0)
//        {
//            wire.updateSpeed = 2;

//            bool[] directions = WorldManager.getInstance().map.Grid[(int)transform.position.x,0, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataWire");
//            List<GameObject> wires = WorldManager.getInstance().map.Grid[(int)transform.position.x,0, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
//            for (int i = 0; i < wires.Count; i++)
//            {
//                if (wires[i] != null)
//                {

//                    if(wires[i].GetComponent<dataWire>().wire.SelfPrio > wire.SelfPrio + 1)
//                    {
//                        wires[i].GetComponent<dataWire>().wire.SelfPrio = wire.SelfPrio + 1;
//                    }else if (wires[i].GetComponent<dataWire>().wire.SelfPrio == -1)
//                    {
//                        wires[i].GetComponent<dataWire>().wire.SelfPrio = wire.SelfPrio + 1;
//                    }

//                    wire.Prio[i] = wires[i].GetComponent<dataWire>().wire.SelfPrio + wire.SelfPrio;
                    
//                    if (wires[i].GetComponent<dataWire>().wire.updateSpeed <= 0)
//                    {
//                        wires[i].GetComponent<dataWire>().GiveValues();               
//                    }
//                }
                
//            }
//            if(wire.SelfPrio == 0 && wire.dataStored > 0)
//            {
//                bool[] directionsU = WorldManager.getInstance().map.Grid[(int)transform.position.x,0, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "uploadStation");
//                List<GameObject> Stations = WorldManager.getInstance().map.Grid[(int)transform.position.x,0, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "uploadStation");
//                List<GameObject> stations = new List<GameObject>();
//                uploadStations = stations;
//                for(int i = 0; i < directionsU.Length; i++)
//                {
//                    if (directionsU[i])
//                    {
//                        stations.Add(Stations[i]);
//                    }
//                }

//            }
//        }
//    }

//    public void GiveData()
//    {
//        sorted.Clear();

//        List<GameObject> objects = WorldManager.getInstance().map.Grid[(int)transform.position.x,0, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
//        for(int i = 0; i < wire.Prio.Length; i++)
//        {
//            if (wire.Prio[i] > 0)
//            {
//                //Debug.Log(wire.Prio[i]);

//                sorted.Add(wire.Prio[i]);
//            }
//        }
//        if(uploadStations.Count > 0)
//        {
//            if (wire.dataStored >= uploadStations.Count)
//            {
//                for (int i = 0; i < uploadStations.Count; i++)
//                {
//                    uploadStations[i].GetComponent<uploadStation>().station.dataStored += 1;
//                    wire.dataStored -= 1;
//                }
//            }
            
//        }
//        else if (sorted != null)
//        {
//            sorted.Sort();

//            if (sorted.Count >= 1)
//            {
                
//                    for (int i = 0; i < wire.Prio.Length; i++)
//                    {
//                        if (sorted[0] == wire.Prio[i])
//                        {
//                            objects[i].GetComponent<dataWire>().wire.dataStored += 1;
//                            wire.dataStored -= 1;
//                        }
                    
//                }
//            }
//        }
//    }    
//}
