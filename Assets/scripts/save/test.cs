using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Test.save();
        //read();
    }

    public void read()
    {
        Test.read();
    }
}
