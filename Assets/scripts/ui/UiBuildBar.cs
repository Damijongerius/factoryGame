using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBuildBar : MonoBehaviour
{
    public GameObject[] buttons;
    public float width;
    public float begin;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("run", 0.5f, 0f);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void run()
    {
        int length = buttons.Length;
        if(length % 2 == 0)
        {
            begin = (length * width) / -2;
        }
        else
        {
            begin = ((length - 1) / -2) * width;
        }

        for(int i = 0; i < length; i++)
        {
            buttons[i].transform.position = new Vector3(begin + (width * i) + (Screen.width / 2),transform.position.y,transform.position.z);
        }
    }
}
