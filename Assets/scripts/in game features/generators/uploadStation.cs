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
        InvokeRepeating(nameof(Sell), 1f, 1f);
    }

    public void Run()
    {
        List<GameObject> wires = WorldManager.getInstance().map.Grid[(int)transform.position.x,0, (int)transform.position.z].GetObject((int)transform.position.x, (int)transform.position.z, "dataWire");
        foreach(GameObject wire in wires)
        {
            if(wire != null)
            {
                wire.GetComponent<dataWire>().wire.SelfPrio = 0;
                wire.GetComponent<dataWire>().GiveValues();
            }
        }
    }

    public void Sell()
    {
        if (station.dataStored > 1)
        {
            station.dataStored--;
            sf.profile.Statistics.Data--;
            sf.profile.Statistics.Money += 5;
        }
    }

}
