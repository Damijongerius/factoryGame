using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wirecolor : MonoBehaviour
{

    public Material Powered;
    public Material Unpowered;
    public Renderer material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>();
        material.material = Unpowered;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(this.gameObject.tag + "tag");
        if (this.gameObject.CompareTag("dataWire"))
        {
            bool[] directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "dataWire");
            foreach (bool direction in directions)
            {
                if (direction)
                {
                    //Debug.Log("powered");
                }
            }
        }
    }
}
