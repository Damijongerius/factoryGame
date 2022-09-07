using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uploadStation : MonoBehaviour
{
    public UploadStation station = new UploadStation();
    private SaveFile sf = SaveFile.GetInstance();
    void Start()
    {
        InvokeRepeating(nameof(Run), 10f, 1.5f);
       
    }


    void Update()
    {
    }

    public void Run()
    {
        List<GameObject> wires = gridSys.grid[(int)transform.position.x, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
        foreach(GameObject wire in wires)
        {
            if(wire != null)
            {
                wire.GetComponent<dataWire>().wire.SelfPrio = 0;
                wire.GetComponent<dataWire>().GiveValues();
            }
        }
        if (station.dataStored > 1)
        {
            station.dataStored--;
            sf.profile.Statistics.Money++;
        }
    }

}
