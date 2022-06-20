using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uploadStation : MonoBehaviour
{ 
    void Start()
    {
        InvokeRepeating(nameof(TestSurround), 2f, 1.5f); 
    }


    public void TestSurround()
    {

    }
}
