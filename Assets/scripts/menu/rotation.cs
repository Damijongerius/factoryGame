using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    public int y =10;
    public float speed;
    void Start()
    {
        InvokeRepeating("Rotate", speed, speed); 
    }

    private void Rotate()
    {
        
        transform.Rotate(0, Mathf.Lerp(transform.rotation.y, y,0.1f), 0);
        
    }

}
