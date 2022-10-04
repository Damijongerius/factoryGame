using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    //rotation y
    private float y;
    public float updateSpeed;

    private void Update()
    {
        //rotate
        transform.RotateAround(this.transform.position, Vector3.up, 20 * Time.deltaTime);
    }

}
