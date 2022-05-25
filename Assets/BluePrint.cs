using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{

    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out RaycastHit hit, 50000.0f))
        {
            transform.position = hit.point + new Vector3(0,0.19f,0);
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
}
