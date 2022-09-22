using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    //rotation y
    private float y;
    public float updateSpeed;
    void Start()
    {
        //call function on speed(time)
        InvokeRepeating(nameof(Rotate), updateSpeed, updateSpeed);
    }

    private void Rotate()
    {
        //rotate
        transform.RotateAround(this.transform.position, Vector3.up, 20 * Time.deltaTime);
    }

}
